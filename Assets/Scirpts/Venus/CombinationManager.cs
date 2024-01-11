using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            GameData.combinationSuccess += 1;
            Debug.Log("Good combintion");
            Destroy(other.gameObject);

            if (GameData.combinationSuccess == 5)
            {
                OnLevelComplete();
            }
        } else
        {
            Destroy(other.gameObject);
            Debug.Log("wrong combintion");
            Waypaoints.SpawnElements();
        }
    }


    public void OnLevelComplete()
    {
        GameData.LevelsCompleted++;
        GameData.CurrentLevel++; // Assuming next level is the current level after completion
        GameData.NumberOfRetries = 0;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene("SolarSystem");

    }

}
