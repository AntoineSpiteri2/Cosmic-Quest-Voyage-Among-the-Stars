using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MartianDrone : MonoBehaviour
{
    public Transform playerTransform; // Assign the player's transform in the Inspector
    public NavMeshAgent agent;

    private float detectionRange = 15f;
    private float attackRange = 10f;
    private float patrolSpeed = 3.5f;
    private float chaseSpeed = 5.5f;

    public float waypointThreshold = 2f;
    private bool isPlayerDetected = false;

    public GameObject projectilePrefab;
    private float shootingCooldown = 2.0f;
    private float lastShotTime = 0f;

    public List<Transform> waypoints;
    private int currentWaypointIndex = 0;
    public Transform weaponPosition; // Assign the position from where the bullet is shot


    public float projectileSpeed;

    void Start()
    {
        currentWaypointIndex = Random.Range(0, waypoints.Count);
        agent = GetComponent<NavMeshAgent>();
        detectionRange = GameData.detectionRange;
        attackRange = GameData.attackRange;
        patrolSpeed = GameData.patrolSpeed;
        chaseSpeed = GameData.chaseSpeed;
        shootingCooldown = GameData.shootingCooldown;




    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer < detectionRange)
        {
            isPlayerDetected = true;
            ChasePlayer();
        }
        else
        {
            isPlayerDetected = false;
            Patrol();
        }

        if (isPlayerDetected && distanceToPlayer < attackRange && Time.time > lastShotTime + shootingCooldown)
        {
            Vector3 shootDirection = (playerTransform.position - transform.position).normalized;
            AimTowards(shootDirection);
            ShootBullet(shootDirection);
            lastShotTime = Time.time;
        }
    }

    void Patrol()
    {
        agent.speed = patrolSpeed;

        if (!agent.pathPending && (!agent.hasPath || agent.remainingDistance < waypointThreshold))
        {
            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = Random.Range(0, waypoints.Count);
            }

            agent.SetDestination(waypoints[currentWaypointIndex].position);
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }


    void ChasePlayer()
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(playerTransform.position);
    }

    void AimTowards(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f); // Adjust the rotation speed if necessary
    }

    void ShootBullet(Vector3 direction)
    {
        // Calculate the rotation to align the bullet's forward direction with the shooting direction
        Quaternion bulletRotation = Quaternion.LookRotation(direction);

        // Instantiate the bullet with the calculated rotation
        GameObject bullet = Instantiate(projectilePrefab, weaponPosition.position, Quaternion.identity);

        // Set the bullet's direction
        bullet.GetComponent<Bullet>().SetDirection(direction);
    }





}
