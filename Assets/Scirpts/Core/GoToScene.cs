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
        Quit
    }


    public SceneName SceneToLoad;



    public void LoadScene()
    {
        if (SceneToLoad.ToString() == "Quit")
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }

        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene(SceneToLoad.ToString());

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            LoadScene();
        }
    }
}
