using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class AestroSpawner : MonoBehaviour
{
    public float spawnRate;
    public GameObject AestroidGameObject;
    public GameObject Spawner;

    private List<GameObject> listWaypoints;
    private float timeSinceLastSpawn; // Timer to track time since last asteroid was spawned


 


    private void Start()
    {
        spawnRate = GameData.SpawnRate;

        listWaypoints = new List<GameObject>();

        int numberofChildGameObjects = Spawner.transform.childCount;

        for (int i = 0; i < numberofChildGameObjects; i++)
        {
            listWaypoints.Add(Spawner.transform.GetChild(i).gameObject);
        }
        timeSinceLastSpawn = 0; // Initialize the timer

    }



    // Update is called once per frame
    void Update()
    {

        timeSinceLastSpawn += Time.deltaTime; // Update the timer every frame

        // Check if enough time has passed since the last spawn
        if (timeSinceLastSpawn >= spawnRate)
        {
            // Reset the timer
            timeSinceLastSpawn = 0;

            // Spawn an asteroid
            int waypointIndex = Random.Range(0, listWaypoints.Count);
            Vector3 spawnPosition = listWaypoints[waypointIndex].transform.position;

            // Randomize the X position
            float randomXOffset = Random.Range(-5f, 5f); // Adjust the range as needed
            spawnPosition.x += randomXOffset;

            Instantiate(AestroidGameObject, spawnPosition, Quaternion.identity);
        }

    }
}
