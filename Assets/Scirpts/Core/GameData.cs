using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{

    // Player Progress
    public static int CurrentLevel { get; set; }
    public static int LevelsCompleted { get; set; }

    // Score and Points
    public static int CurrentScore { get; set; }

    // Health and Lives
    public static int PlayerHealth { get; set; }
    public static int PlayerLives { get; set; }

    // Collectibles and Items
    public static int CollectedItemsCount { get; set; } // or use a more complex structure

    // Player Statistics
    public static float TotalPlayTime { get; set; }
    public static int NumberOfRetries { get; set; }


    // Educational Content Progress
    public static int EducationalContentUnlocked { get; set; }

    // Difficulty Adjustment Data (for GDA)
    public static float DifficultyLevel { get; set; } // Can be a more complex structure depending on GDA implementation
}
