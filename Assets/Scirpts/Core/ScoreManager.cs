using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class ScoreManager : MonoBehaviour
{

    public GameObject ScoreText;


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

        setScore();



    }



    public void setScore()
    {
        float score = 0;

        switch (GameData.LastScene.ToString())
        {
            case "Mercury":
                score = CalculateScore(GameData.PlayerHealth, 100, GetDifficultyMultiplier(GameData.Difficultysetter));
                break;
            case "Venus":
                score = CalculateTimeBasedScore(GameData.TimerSecondsCurrent, GetDifficultyMultiplier(GameData.Difficultysetter));
                break;
            case "Earth":
                score = CalculateEarthLevelScore(GameData.TimerSecondsCurrent, GameData.Moves, GetDifficultyMultiplier(GameData.Difficultysetter));
                break;
            case "Mars":
                score = CalculateCollectionBasedScore(GameData.rocksCollected, GetDifficultyMultiplier(GameData.Difficultysetter));
                break;
        }

        GameData.CurrentScore += (int)score;
        ScoreText.GetComponent<TextMeshProUGUI>().text = GameData.CurrentScore.ToString();
    }

    float CalculateTimeBasedScore(int timerSeconds, float difficultyMultiplier)
    {
        float score;

        // If scoring based on time remaining (faster completion is better)
         score = (GameData.TimerSeconds - timerSeconds) * difficultyMultiplier;


        return Mathf.Clamp(score, 0, 1000); // Adjust the max score as needed
    }

    float CalculateCollectionBasedScore(int rocksCollected, float difficultyMultiplier)
    {
        float score = rocksCollected * 5 * difficultyMultiplier; // Assuming each rock collected is worth 10 points
        return Mathf.Clamp(score, 0, 1000); // Adjust the max score as needed
    }


    float CalculateScore(float playerHealth, float maxHealth, float difficultyMultiplier)
    {
        if (playerHealth <= 0) return 0; // Game over, score is 0

        float healthPercentage = playerHealth / maxHealth;
        float score = healthPercentage * 100; // Scale the health percentage to the max score of 100
        score *= difficultyMultiplier; // Adjust the score by the difficulty multiplier

        return Mathf.Clamp(score, 0, 100); // Ensure score does not exceed max score of 100
    }

    float CalculateEarthLevelScore(int timerSeconds, int moves, float difficultyMultiplier)
    {
        float baseScore = 1000; // Base score for perfect performance
        float scorePenaltyPerMove = 10; // Penalty for each additional move
        float scorePenaltyPerSecond = 5; // Penalty for each second taken

        // Calculate move and time penalties
        float movePenalty = moves * scorePenaltyPerMove;
        float timePenalty = (GameData.TimerSeconds - timerSeconds) * scorePenaltyPerSecond;

        // Calculate final score
        float score = baseScore - movePenalty - timePenalty;
        score *= difficultyMultiplier;

        return Mathf.Clamp(score, 0, baseScore); // Ensure score does not fall below 0 or exceed base score
    }



    float GetDifficultyMultiplier(GameData.Diffuctly difficulty)
    {
        switch (difficulty)
        {
            case GameData.Diffuctly.Easy:
                return 1.0f;
            case GameData.Diffuctly.Medium:
                return 1.5f;
            case GameData.Diffuctly.Hard:
                return 2.0f;
            default:
                return 1.0f; // Default to easy if undefined
        }
    }


}
