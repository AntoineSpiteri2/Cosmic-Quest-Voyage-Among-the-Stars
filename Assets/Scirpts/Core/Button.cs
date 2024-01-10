using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;

public class Button : MonoBehaviour
{

    private TextMeshPro _textMeshPro;
    public enum ActionType
    {
        Start,
        Quit,
        retry,
        Diffuctly
    }

    private void Start()
    {
        GameData.Difficultysetter = GameData.Diffuctly.Easy;
        _textMeshPro = gameObject.GetComponentInChildren<TextMeshPro>();
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
            case ActionType.Diffuctly:
                if (GameData.Diffuctly.Easy.ToString() == _textMeshPro.text)
                {
                    GameData.Difficultysetter = GameData.Diffuctly.Medium;
                    _textMeshPro.text = GameData.Difficultysetter.ToString();
                }
                else if (GameData.Diffuctly.Medium.ToString() == _textMeshPro.text)
                {
                    GameData.Difficultysetter = GameData.Diffuctly.Hard;
                    _textMeshPro.text = GameData.Difficultysetter.ToString();


                }
                else if (GameData.Diffuctly.Hard.ToString() == _textMeshPro.text)
                {
                    GameData.Difficultysetter = GameData.Diffuctly.Easy;
                    _textMeshPro.text = GameData.Difficultysetter.ToString();


                }
                break;

        }
    }




}
