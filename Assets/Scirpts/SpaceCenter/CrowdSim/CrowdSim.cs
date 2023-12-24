using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrowdSim : MonoBehaviour
{
    public GameObject waypointParent;
    private List<Transform> waypoints;
    private NavMeshAgent agent;
    private float waypointChangeInterval = 2.0f; // Interval to force waypoint change
    private float stuckCheckInterval = 1.0f; // Interval to check if the agent is stuck
    private float lastWaypointChangeTime;
    private float lastStuckCheckTime;
    private Vector3 lastPosition;
    private float movementThreshold = 0.1f; // Threshold to consider if the agent has moved

    private Animator animator; // Reference to the Animator component
    private Vector3 previousPosition;

    private float currentSpeed; // Current speed value for the Animator
    public float speedSmoothTime = 0.1f; // Time taken to smooth the speed change



    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        waypoints = new List<Transform>();

        foreach (Transform child in waypointParent.transform)
        {
            waypoints.Add(child);
        }

        GotoRandomWaypoint();
        lastWaypointChangeTime = Time.time;
        lastStuckCheckTime = Time.time;
        lastPosition = transform.position;


        animator = GetComponent<Animator>(); // Get the Animator component
        previousPosition = transform.position;
        currentSpeed = 0f;

    }

    void GotoRandomWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        agent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);
    }

    void Update()
    {
        if (!agent.pathPending && !agent.hasPath && Vector3.Distance(transform.position, agent.destination) < 1.0f)
        {
            GotoRandomWaypoint();
        }

        // Force waypoint change at intervals
        if (Time.time - lastWaypointChangeTime > waypointChangeInterval)
        {
            GotoRandomWaypoint();
            lastWaypointChangeTime = Time.time;
        }

        // Check if the agent is stuck
        if (Time.time - lastStuckCheckTime > stuckCheckInterval)
        {
            if (Vector3.Distance(transform.position, lastPosition) < movementThreshold)
            {
                GotoRandomWaypoint(); // Change waypoint if the agent is stuck
            }
            lastPosition = transform.position;
            lastStuckCheckTime = Time.time;
        }

        // Calculate the speed
        float speed = (transform.position - previousPosition).magnitude / Time.deltaTime;
        previousPosition = transform.position;

        // Normalize or scale the speed as needed for the Animator
        float normalizedSpeed = Mathf.Clamp(speed, 0, 2); // Assuming 1 is the max speed for walking

        currentSpeed = Mathf.Lerp(currentSpeed, normalizedSpeed, speedSmoothTime * Time.deltaTime);

        // Update the Animator parameter
        animator.SetFloat("Blend", currentSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {

        // Change waypoint on collision
        if (other.gameObject.CompareTag("Agent"))
        {
            GotoRandomWaypoint();
        }
    }


}
