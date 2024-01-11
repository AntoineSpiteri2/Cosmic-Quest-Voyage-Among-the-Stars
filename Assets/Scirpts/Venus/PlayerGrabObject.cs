using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrabObject : MonoBehaviour
{
    public Camera camera;
    public Transform PlayerTrans;
    public float range = 3f;
    public float go = 100f;

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

            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            }
        }
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

