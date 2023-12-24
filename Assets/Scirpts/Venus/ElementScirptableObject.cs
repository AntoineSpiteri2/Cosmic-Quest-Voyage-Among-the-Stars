using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Element", menuName = "Element")]

public class ElementScirptableObject : ScriptableObject
{
    public enum Elements
    {
        Carbon_Dioxide,
        Sulfur_Dioxide,
        Nitrogen,
        Water_Vapor,
        Argon,
    }


    public Elements ElementType;


    public GameObject ElementGameObject;

    public GameObject ParticleWaypoint;

    public string description;




}
