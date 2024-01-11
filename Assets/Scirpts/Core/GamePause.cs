using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pasuemenu;
    public CinemachineVirtualCamera cinemachineCamera;
    public CinemachineBrain cinemachineBrain;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    // Call this method to toggle pause
    public void TogglePause()
    {
        isPaused = !isPaused; // Toggle the state

        if (isPaused)
        {
            PauseGame();
            pasuemenu.SetActive(true);
        }
        else
        {
            ResumeGame();
            pasuemenu.SetActive(false);

        }
    }

    // Call this method to pause the game
    public void PauseGame()
    {
        if (SceneManager.GetActiveScene().name == "SpaceCenter" || SceneManager.GetActiveScene().name == "Venus" || SceneManager.GetActiveScene().name == "Win")
        {
            Cursor.lockState = CursorLockMode.None;
        }


        Time.timeScale = 0;


        // Optionally, you can also pause animations or sounds if necessary
        // AudioListener.pause = true;
        isPaused = true;
    }

    // Call this method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1;

        // Optionally, resume animations or sounds if they were paused
        // AudioListener.pause = false;
        isPaused = false;
    }

    public void GoBackGame()
    {
        if (SceneManager.GetActiveScene().name == "SpaceCenter"  || SceneManager.GetActiveScene().name == "Venus" || SceneManager.GetActiveScene().name == "Win")
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
            TogglePause();
    }

    public void GoBackMenu()
    {
        TogglePause();
        SceneManager.LoadScene("Start");

    }

    public void Quit()
    {
        TogglePause();
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }


    public void Reset()
    {

        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;


        GameData.CurrentLevel = 0;
       GameData.LevelsCompleted = 0;
        GameData.CurrentScore = 0;
         GameData.PlayerHealth = 100;
         GameData.TotalPlayTime = 0;
         GameData.NumberOfRetries = 0;
        GameData.LastScene = "";

        SceneManager.LoadScene("Start");

    }
}
