using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public int initialTimeInSeconds = 120; // User can set this in the editor
    private float remainingTime;
    private bool timerRunning = false;

    public TextMeshProUGUI timerText; // Assign this in the editor

    void Start()
    {
        GameData.TimerSecondsCurrent = 0;
        initialTimeInSeconds = GameData.TimerSeconds;
        StartTimer(initialTimeInSeconds);
    }

    void Update()
    {
        if (!timerRunning)
            return;

        // Update remaining time
        remainingTime -= Time.deltaTime;

        // Update timer display
        UpdateTimerDisplay();

        GameData.TimerSecondsCurrent = (int)remainingTime;


        // Check if the timer has run out
        if (remainingTime <= 0)
        {
            timerRunning = false;
            GameOver();
        }
    }

    public void StartTimer(int seconds)
    {
        remainingTime = seconds;
        timerRunning = true;
    }

    private void UpdateTimerDisplay()
    {
        // Calculate minutes and seconds from remaining time
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        // Update the text component
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void GameOver()
    {
        GameManager.Instance.ReducePlayer(100);
        Cursor.lockState = CursorLockMode.None;

        Debug.Log("Game Over!");
        // Implement your game over logic here
    }
}
