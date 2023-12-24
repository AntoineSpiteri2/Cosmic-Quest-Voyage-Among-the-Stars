using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CombinationManager : MonoBehaviour
{
    public enum ElementType {
        Carbon_Dioxide,
        Sulfur_Dioxide,
        Nitrogen,
        Water_Vapor,
        Argon,

    }


    public ElementType elementType;

    public ElementType elementTypeAccpeted;


    public SpawnElementsRandomly Waypaoints;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == elementTypeAccpeted.ToString())
        {
            Destroy(other.gameObject);
        } else
        {
            Destroy(other.gameObject);
            Debug.Log("wrong combintion");
            Waypaoints.SpawnElements();
        }
    }


}
