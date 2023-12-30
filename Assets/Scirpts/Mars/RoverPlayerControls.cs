using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoverPlayerControls : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public float speed = 5.0f;
    public float turnSpeed = 200.0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        // Disable NavMeshAgent control if you want the player to have full control
        //navMeshAgent.enabled = false;
    }

    void Update()
    {
        // Check for player input and move the rover accordingly
        float horizontal = Input.GetAxis("Horizontal"); // For turning
        float vertical = Input.GetAxis("Vertical"); // For forward/backward movement

        // Adjust the position and rotation based on input
        transform.Translate(Vector3.forward * vertical * speed * Time.deltaTime);
        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);
    }

    // Call this method to toggle control between AI and player
    public void SetControl(bool isPlayerControlled)
    {
        navMeshAgent.enabled = !isPlayerControlled;
    }
}
