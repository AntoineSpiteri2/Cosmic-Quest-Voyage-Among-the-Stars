using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 velocity;
    public float maxSpeed = 5f;
    public float maxForce = 0.5f;

    // Boids Settings
    public float neighborRadius = 3.0f;
    public float separationDistance = 1.0f;

    GameObject[] allBoids;
    // ... (Other variables and functions)

    private void Start()
    {
        allBoids = GameObject.FindGameObjectsWithTag("Boid");
    }

    void Update()
    {
        // Calculate the three rules
        Vector3 separation = CalculateSeparation();
        Vector3 alignment = CalculateAlignment();
        Vector3 cohesion = CalculateCohesion();

        // Apply the rules to the velocity
        velocity += separation + alignment + cohesion;

        // Apply a max speed check
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // Update position and rotation
        transform.position += velocity * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(velocity);
    }

    private Vector3 CalculateSeparation()
    {
        Vector3 steer = Vector3.zero;
        int count = 0;

        // Check each boid in the flock
        foreach (var other in allBoids)
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance > 0 && distance < separationDistance)
            {
                Vector3 diff = transform.position - other.transform.position;
                diff.Normalize();
                diff /= distance;  // Weight by distance
                steer += diff;
                count++;
            }
        }

        if (count > 0)
        {
            steer /= count;
        }

        if (steer.magnitude > 0)
        {
            steer.Normalize();
            steer *= maxSpeed;
            steer -= velocity;
            steer = Vector3.ClampMagnitude(steer, maxForce);
        }

        return steer;
    }

    private Vector3 CalculateAlignment()
    {
        Vector3 sum = Vector3.zero;
        int count = 0;

        foreach (var other in allBoids)
        {
            Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
            if (otherRigidbody != null)
            {
                float distance = Vector3.Distance(transform.position, other.transform.position);
                if (distance > 0 && distance < neighborRadius)
                {
                    sum += otherRigidbody.velocity;
                    count++;
                }
            }
        }

        if (count > 0)
        {
            sum /= count;
            sum.Normalize();
            sum *= maxSpeed;
            Vector3 steer = sum - velocity;
            steer = Vector3.ClampMagnitude(steer, maxForce);
            return steer;
        }
        else
        {
            return Vector3.zero;
        }
    }


    private Vector3 CalculateCohesion()
    {
        Vector3 sum = Vector3.zero;
        int count = 0;

        foreach (var other in allBoids)
        {
            float distance = Vector3.Distance(transform.position, other.transform.position);
            if (distance > 0 && distance < neighborRadius)
            {
                sum += other.transform.position;
                count++;
            }
        }

        if (count > 0)
        {
            sum /= count;
            return Seek(sum);
        }
        else
        {
            return Vector3.zero;
        }
    }

    private Vector3 Seek(Vector3 target)
    {
        Vector3 desired = target - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        Vector3 steer = desired - velocity;
        steer = Vector3.ClampMagnitude(steer, maxForce);
        return steer;
    }


}
