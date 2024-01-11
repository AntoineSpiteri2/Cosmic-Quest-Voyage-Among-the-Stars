using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRocks : MonoBehaviour
{

     public int NumberoFRocks = 5000;
    public int NumberofDuststroms = 100;


    public GameObject rock;

    public GameObject DustStorm;
    // Start is called before the first frame update
    void Start()
    {

        NumberofDuststroms = GameData.ammountofDustStorms;

        Terrain terrain = GetComponent<Terrain>();

        


        PlaceRocks(terrain, rock, NumberoFRocks);

        PlaceDustStrom(terrain, DustStorm, NumberofDuststroms);

    }


    //reason for this scirpt as painting the rocks wont allow the player to collect the rocks while this method does allow me

    //I think just the limitation of the terrain desginer 

    private void PlaceRocks(Terrain terrain, GameObject rock, int numberofrocks)
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;


        for (int i = 0; i < numberofrocks; i++)
        {
            float x = Random.Range(0, terrainSize.x);
            float z = Random.Range(0, terrainSize.z);
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.transform.position.y;

            Vector3 rockPosition = new Vector3(x, y, z) + terrain.transform.position;
            Instantiate(rock, rockPosition, Quaternion.identity);
        }
    }



    private void PlaceDustStrom(Terrain terrain, GameObject rock, int numberofrocks)
    {
        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainSize = terrainData.size;
        rock.GetComponent<DustStrom>().terrain = terrain;

        for (int i = 0; i < numberofrocks; i++)
        {
            float x = Random.Range(0, terrainSize.x);
            float z = Random.Range(0, terrainSize.z);
            float y = terrain.SampleHeight(new Vector3(x, 0, z)) + terrain.transform.position.y;

            Vector3 rockPosition = new Vector3(x, y, z) + terrain.transform.position;
            Instantiate(rock, rockPosition, Quaternion.identity);
        }
    }

}
