using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrabObject : MonoBehaviour
{
    public Camera camera;
    public Transform PlayerTrans;
    public float range = 3f;
    public float go = 100f;

    public GameObject panel;

    public GameObject hinttext;

    public GameObject heldObject;
    public bool isObjectHeld = false;

    public LayerMask pickableLayer; // Define the layer mask for pickable objects

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartPickup();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Drop();
        }

        if (isObjectHeld)
        {
            Vector3 newPosition = camera.transform.position + camera.transform.forward * go;
            // Prevent the object from going below the player's feet
            newPosition.y = Mathf.Max(newPosition.y, PlayerTrans.position.y);
            if (heldObject == null)
            {
                isObjectHeld = false;
            }

            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.MovePosition(newPosition);
            }
        }
    }


    private void StartPickup()
    {
        if (isObjectHeld) return;

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range, pickableLayer))
        {
            heldObject = hit.transform.gameObject;
            isObjectHeld = true;


                SetHintText(heldObject);
            

            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
        }
    }

    private void SetHintText(GameObject element)
    {
        string hint = "";
        switch (element.name)
        {
            case "Carb dioxode(Clone)":
                hint = "This gas is like a warm blanket and likes being around water. Find the water container for this one! Or, it can also go where the air is mostly made of a super quiet gas."; // Replace with actual hint
                break;
            case "Nitrogen(Clone)":
                hint = "Nitrogen is all around us in the air but doesn't make much noise. It can visit the container that has a gas which is very quiet and noble!"; // Replace with actual hint
                break;
            case "Sulfer(Clone)":
                hint = "This gas can be stinky like rotten eggs. It should go to the container that's looking for a gas that's like a warm blanket for our planet!"; // Replace with actual hint
                break;
            case "water(Clone)":
                hint = "Water vapor is what you see when water gets heated up and turns into clouds. It goes into the container that loves smelly gases from volcanoes!"; // Replace with actual hint
                break;

        }

        hinttext.GetComponent<TextMeshProUGUI>().text = hint;
    }

    private void Drop()
    {
        if (!isObjectHeld) return;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None;
        }

        heldObject = null;
        isObjectHeld = false;
    }
}

