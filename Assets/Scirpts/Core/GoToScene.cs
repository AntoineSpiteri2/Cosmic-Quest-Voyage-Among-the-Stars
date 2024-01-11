using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GoToScene : MonoBehaviour
{
    public enum SceneName
    {
        Earth,
        Mars,
        Mercury,
        SolarSystem,
        SpaceCenter,
        Venus,
        Main,
        Quit,
        Retry
    }


    public SceneName SceneToLoad;



    public void LoadScene()
    {
        if (SceneToLoad.ToString() == "Quit")
        {
            Application.Quit();
        }

        Cursor.lockState = CursorLockMode.None;

        if (SceneToLoad.ToString() == "Retry")
        {
            SceneManager.LoadScene(GameData.LastScene.ToString());

        } else
        {

            SceneManager.LoadScene(SceneToLoad.ToString());

        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LoadScene();
        }
    }
}
