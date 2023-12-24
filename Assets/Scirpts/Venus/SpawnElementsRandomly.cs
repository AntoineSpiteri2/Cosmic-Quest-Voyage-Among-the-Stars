using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnElementsRandomly : MonoBehaviour
{

    // Change this to a list of your ScriptableObjects
    public List<ElementScirptableObject> Elements;
    private List<Transform> ChildLocations;

    void Start()
    {
        ChildLocations = new List<Transform>();

        foreach (Transform child in transform)
        {
            ChildLocations.Add(child);
        }

        SpawnElements();
    }


    public void SpawnElements()
    {
        int index = 0;
        for (int i = 0; i < ChildLocations.Count && index < Elements.Count; i++)
        {
            ElementScirptableObject elementSO = Elements[index];

            // Instantiate the GameObject and then set its tag
            GameObject instantiatedElement = Instantiate(elementSO.ElementGameObject, ChildLocations[i].position, Quaternion.identity);
            instantiatedElement.tag = elementSO.ElementType.ToString();

            // Instantiate the ParticleWaypoint (if applicable)
            Instantiate(elementSO.ParticleWaypoint, ChildLocations[i].position, Quaternion.identity);

            index++;
        }
    }



}


