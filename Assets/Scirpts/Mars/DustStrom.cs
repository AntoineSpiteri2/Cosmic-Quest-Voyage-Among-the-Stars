using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustStrom : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector3 moveDirection;
    public int dmg;
    public Terrain terrain; // Reference to the Terrain
    public float changeDirectionInterval = 5f; // Time in seconds to change direction
    public float heightAboveTerrain = 1f; // Height above the terrain

    private float timeSinceLastDirectionChange;
    private Vector3 terrainSize;

    void Start()
    {
        dmg = GameData.MarsDmg;
        if (terrain != null)
        {
            terrainSize = terrain.terrainData.size;
        }
        else
        {
            Debug.LogError("Terrain not assigned to DustStorm script.");
            return;
        }

        SetInitialPositionAndDirection();
    }

    void Update()
    {
        // Move the dust storm
        Vector3 newPosition = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        newPosition.y = terrain.SampleHeight(newPosition) - 0.1f; // Adjust Y position based on terrain height

        transform.position = newPosition;

        // Change direction at intervals or if it reaches the terrain bounds
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange >= changeDirectionInterval || IsOutOfBounds())
        {
            SetRandomDirection();
            timeSinceLastDirectionChange = 0;
        }
    }

    private bool IsOutOfBounds()
    {
        return transform.position.x < 0 || transform.position.x > terrainSize.x
            || transform.position.z < 0 || transform.position.z > terrainSize.z;
    }

    private void SetRandomDirection()
    {
        moveDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    private void SetInitialPositionAndDirection()
    {
        float randomX = Random.Range(0, terrainSize.x);
        float randomZ = Random.Range(0, terrainSize.z);
        float y = terrain.SampleHeight(new Vector3(randomX, 0, randomZ)) + heightAboveTerrain;

        transform.position = new Vector3(randomX, y , randomZ);

        SetRandomDirection();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<AudioSource>().Play();
            GameManager.Instance.ReducePlayer(dmg);

        }
    }

}
