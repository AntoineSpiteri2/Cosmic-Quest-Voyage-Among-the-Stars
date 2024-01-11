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

    private void Update()
    {
        if (Time.timeScale != 0) // Assuming you have a way to check if the game is paused
        {
            GameData.TotalPlayTime += Time.deltaTime;
        }

        // Rest of your Update code...
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

        switch (scene.name)
        {
            case "Mercury":
                if (GameData.Difficultysetter.ToString() == "Easy")
                {
                    GameData.PlayerHealth = 100;
                    GameData.SpawnRate = 3;
                    GameData.Astrodmg = 5;
                    GameData.MaxSpeedAstro = 750f;
                    GameData.MinSpeedAstro = 500f;

                }
                else if (GameData.Difficultysetter.ToString() == "Medium")
                {
                    GameData.PlayerHealth = 100;
                    GameData.SpawnRate = 1.5f;
                    GameData.Astrodmg = 10;
                    GameData.MaxSpeedAstro = 1000;
                    GameData.MinSpeedAstro = 750f;

                }
                else
                {
                    GameData.PlayerHealth = 100;
                    GameData.SpawnRate = 0.1f;
                    GameData.Astrodmg = 20;
                    GameData.MaxSpeedAstro = 1000f;
                    GameData.MinSpeedAstro = 1250f;


                }
                break;
            case "Venus":
                if (GameData.Difficultysetter.ToString() == "Easy")
                {
                    GameData.TimerSeconds = 400;

                }
                else if (GameData.Difficultysetter.ToString() == "Medium")
                {
                    GameData.TimerSeconds = 200;


                }
                else
                {

                    GameData.TimerSeconds = 100;


                }
                break;
            case "Earth":
                if (GameData.Difficultysetter.ToString() == "Easy")
                {
                    GameData.TimerSeconds = 400;

                }
                else if (GameData.Difficultysetter.ToString() == "Medium")
                {
                    GameData.TimerSeconds = 200;


                }
                else
                {

                    GameData.TimerSeconds = 100;


                }
                break;
            case "Mars":
                if (GameData.Difficultysetter.ToString() == "Easy")
                {
                    GameData.ammountofDustStorms = 500;
                }
                else if (GameData.Difficultysetter.ToString() == "Medium")
                {
                    GameData.ammountofDustStorms = 1000;
                }
                else
                {
                    GameData.ammountofDustStorms = 1500;
                }
                break;
        }
    }
        public void ReducePlayer(int Dmg)
    {
        GameData.PlayerHealth -= Dmg;

        Debug.Log("Player damaged current health: " + GameData.PlayerHealth);
        if (SceneManager.GetActiveScene().name == "Mercury" || SceneManager.GetActiveScene().name == "Mars") {

            HealthBar.Instance.SetHealth(GameData.PlayerHealth, 100);


        }
        if (GameData.PlayerHealth <= 0)
        {
            GameData.NumberOfRetries++;
            SceneManager.LoadScene("Retry");

        }
    }

}
