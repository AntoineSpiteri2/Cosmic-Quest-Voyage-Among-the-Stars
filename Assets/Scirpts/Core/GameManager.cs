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

    private void Start()
    {
        GameData.Objective = "Mercery";
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameData.PlayerHealth = 100;

        switch (scene.name)
        {
            case "Mercury":
                if (GameData.Difficultysetter.ToString() == "Easy")
                {
                    GameData.SpawnRate = 3;
                    GameData.Astrodmg = 5;
                    GameData.MaxSpeedAstro = 750f;
                    GameData.MinSpeedAstro = 500f;

                }
                else if (GameData.DifficultyLevel.ToString() == "Medium")
                {
                    GameData.SpawnRate = 1.5f;
                    GameData.Astrodmg = 10;
                    GameData.MaxSpeedAstro = 1000;
                    GameData.MinSpeedAstro = 750f;

                }
                else
                {
                    GameData.SpawnRate = 0.1f;
                    GameData.Astrodmg = 20;
                    GameData.MaxSpeedAstro = 1000f;
                    GameData.MinSpeedAstro = 1250f;


                }
                break;
        }
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
