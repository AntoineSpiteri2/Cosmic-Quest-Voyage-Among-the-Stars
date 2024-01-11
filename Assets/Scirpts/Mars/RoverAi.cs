using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class RoverAi : MonoBehaviour
{
    public Transform destination;
    private NavMeshAgent navMeshAgent;
    private Rigidbody rigidbody;
    public float rotationSpeed = 2f;
    private bool playerControlEnabled = true;
    private float playerSteeringSensitivity = 10f;
    private bool isPlayerControlling = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rigidbody = GetComponentInParent<Rigidbody>();

        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;

        SetDestination();
    }

    void SetDestination()
    {
        if (destination != null)
        {
            navMeshAgent.SetDestination(destination.position);
        }
    }

    void Update()
    {
        if (playerControlEnabled && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            isPlayerControlling = true;
            navMeshAgent.isStopped = true;
            AdjustDestinationBasedOnPlayerInput();
        }
        else
        {
            if (isPlayerControlling)
            {
                isPlayerControlling = false;
                navMeshAgent.isStopped = false;
                navMeshAgent.Warp(rigidbody.position); // Update NavMeshAgent position to current position
                SetDestination();
            }

            if (navMeshAgent.hasPath && !navMeshAgent.pathPending)
            {
                Vector3 nextPosition = navMeshAgent.nextPosition;
                rigidbody.MovePosition(nextPosition);
                RotateParentTowardsMovementDirection();
            }

        }


        if (!isPlayerControlling && !navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
            {
                // Rover has reached the destination
                OnLevelComplete();
            }
        }
    }

    void AdjustDestinationBasedOnPlayerInput()
    {
        Vector3 direction = new Vector3(-Input.GetAxis("Horizontal"), 0, -Input.GetAxis("Vertical"));
        Vector3 movement = transform.TransformDirection(direction * playerSteeringSensitivity * Time.deltaTime);
        rigidbody.MovePosition(rigidbody.position + movement);
    }
    void RotateParentTowardsMovementDirection()
    {
        if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            Quaternion lookRotation = Quaternion.LookRotation(-navMeshAgent.velocity.normalized);
            // Apply rotation to the parent object
            if (transform.parent != null)
            {
                transform.parent.rotation = Quaternion.Slerp(transform.parent.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
            else
            {
                // If there is no parent, apply rotation to this object
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }



    public void OnLevelComplete()
    {
        GameData.LevelsCompleted++;
        GameData.CurrentLevel++; 
        GameData.NumberOfRetries = 0;
        SceneManager.LoadScene("Win");

    }

}



