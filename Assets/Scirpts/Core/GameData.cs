using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{

    // Player Progress

    // Score and Points
    public static int CurrentScore { get; set; }

    // Health and Lives
    public static int PlayerHealth { get; set; }


    // Player Statistics

    public static int CurrentLevel = 1; // Assuming level starts from 1
    public static int LevelsCompleted = 0;
    public static float TotalPlayTime = 0f;
    public static int NumberOfRetries = 0;


    public static string LastScene { get; set; }

    public enum Diffuctly
    {
        Easy,
        Medium,
        Hard
    }
    private static string _objective = "Mercury";


    public static string Objective
    {
        get
        {
            return _objective;
        }
        set
        {
            _objective = value;

            // for debugging pruposes as this really helped 
            var stackTrace = new System.Diagnostics.StackTrace();
            Debug.Log("Objective set to: " + value + "\nStack Trace: " + stackTrace);
        }
    }
    public static Diffuctly Difficultysetter { get; set; }



    //Mercury Level

    public static float SpawnRate { get; set; }
    public static int Astrodmg { get; set; }

    public static float MaxSpeedAstro { get; set; }
    public static float MinSpeedAstro { get; set; }

    // end of it Mercury Level


    //Venus

    public static int TimerSeconds { get; set; }

    public static int TimerSecondsCurrent { get; set; }


    public static int combinationSuccess { get; set; }

    //earth
    public static int Moves { get; set; }

    public static int TimerSecondsCurrentEarth { get; set; }

    public static int matches { get; set; }



    //mars 

    public static int ammountofDustStorms { get; set; }
    public static int rocksCollected { get; set; }



    public static void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.Save(); // Save PlayerPrefs changes
    }


    public static int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0); // 0 is the default value if no high score is set
    }


}
