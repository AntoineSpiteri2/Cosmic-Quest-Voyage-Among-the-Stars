using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the GameManager persistent across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures only one instance of GameManager exists
        }
    }


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

   

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameData.PlayerHealth = 100;
    }
        public void ReducePlayer(int Dmg)
    {
        GameData.PlayerHealth -= Dmg;

        Debug.Log("Player damaged current health: " + GameData.PlayerHealth);
        if (GameData.PlayerHealth <= 0)
        {

        }
    }

}
