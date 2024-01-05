using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                // Optionally, you can create a new GameObject with GameManager if none exists
                GameObject gameObject = new GameObject("GameManager");
                instance = gameObject.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);  // Ensures that there aren't multiple GameManager instances
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject); // Makes the GameManager persist across different scenes
    }



    public void ReducePlayer(int Dmg)
    {
        GameData.PlayerHealth -= Dmg;

        if (GameData.PlayerHealth <= 0)
        {

        }
    }

}
