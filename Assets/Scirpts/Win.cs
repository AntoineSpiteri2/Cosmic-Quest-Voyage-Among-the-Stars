using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Win : MonoBehaviour
{

    public GameObject Highscore;
    // Start is called before the first frame update
    void Start()
    {
        if (Highscore != null)
        {
            int highScore = LoadHighScore();
            Highscore.GetComponent<TextMeshProUGUI>().text = highScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}
