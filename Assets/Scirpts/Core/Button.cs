using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Button : MonoBehaviour
{
    public enum ActionType
    {
        Start,
        Quit,
        retry
    }

    public ActionType actionType;

    private void OnMouseDown()
    {
        switch (actionType)
        {
            case ActionType.Start:
                // Load your scene here. Replace "YourSceneName" with the name of your scene
                SceneManager.LoadScene("SpaceCenter");
                break;
            case ActionType.retry:
                // Load your scene here. Replace "YourSceneName" with the name of your scene
                SceneManager.LoadScene(GameData.LastScene);
                break;
            case ActionType.Quit:
                // Quit the game

                UnityEditor.EditorApplication.isPlaying = false;

                Application.Quit();
                break;
        }
    }




}
