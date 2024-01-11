using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ObjectiveManager : MonoBehaviour
{
    public GameObject objectiveText;



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

            setObjective();

    }



    public void setObjective()
    {
        objectiveText.GetComponent<TextMeshProUGUI>().text = GameData.Objective;
    }
}

