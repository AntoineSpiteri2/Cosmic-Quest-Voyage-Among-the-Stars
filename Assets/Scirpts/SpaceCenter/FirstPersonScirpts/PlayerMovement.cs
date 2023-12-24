using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    public float moveSpeed;


    public float groundDrag;

    [Header("Ground check")]
    public float playerHeight;
    public LayerMask whatisGround;
    public bool grounded;


    public Transform orirntation;

    float HorizontalInput;
    float VerticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(rb.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);
        MyInput();

        if (grounded)
        {
            rb.drag = groundDrag;
        } else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");


    }




    private void MovePlayer()
    {
        moveDirection = orirntation.forward * VerticalInput + orirntation.right * HorizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }


}
