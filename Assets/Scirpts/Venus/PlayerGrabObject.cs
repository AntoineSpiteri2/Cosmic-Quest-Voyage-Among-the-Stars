using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrabObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;

    public Transform PlayerTrans;
    public float range = 3f;
    public float go = 100f;

    public GameObject heldObject;
    public bool isObjectHeld = false;



    // Update is called once per frame
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
            // Calculate the new position in front of the player
            Vector3 newPosition = camera.transform.position + camera.transform.forward * go;

            // Move the held object using Rigidbody
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.MovePosition(newPosition);
            }
        }
    }



    private void StartPickup()
    {
        if (isObjectHeld) return; // Skip if already holding an object

        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            if (hit.transform.CompareTag("Pickable"))
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
    }


    private void Drop()
    {
        if (!isObjectHeld) return; // Skip if no object is held

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.constraints = RigidbodyConstraints.None; // Remove constraints
        }

        heldObject = null;
        isObjectHeld = false;
    }







}


