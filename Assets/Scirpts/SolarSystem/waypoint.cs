using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class waypoint : MonoBehaviour
{

    public RawImage img;
    public Transform target;
    public CinemachineVirtualCamera cinemachineCamera;


    public GoToScene sceneLoader;

    public GameObject textMsg;
    public GameObject Objective;
    public GameObject textmsgpanel;





    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {



            // Get the Camera that Cinemachine is sending its output to
            Camera camera = CinemachineCore.Instance.GetActiveBrain(0).OutputCamera;


            // Determine if the target is behind the camera
            Vector3 toTarget = (target.position - camera.transform.position).normalized;
            bool isBehind = Vector3.Dot(toTarget, camera.transform.forward) < 0;

            if (isBehind)
            {
                // Place the image in a corner of the screen
                img.transform.position = new Vector2(50, 50); // Adjust these values as needed
            }
            else if (Time.timeScale != 0)
            {
                // Normal behavior when the target is in front
                Vector2 pos = camera.WorldToScreenPoint(target.position);
                pos.x = Mathf.Clamp(pos.x, img.GetPixelAdjustedRect().width / 2, Screen.width - img.GetPixelAdjustedRect().width / 2);
                pos.y = Mathf.Clamp(pos.y, img.GetPixelAdjustedRect().height / 2, Screen.height - img.GetPixelAdjustedRect().height / 2);
                img.transform.position = pos;
            }
        }
    }


    void OnMouseDown()
    {

        if (Time.timeScale != 0)
        {
            string clickedName = gameObject.name;
            string objectiveText = Objective.GetComponent<TextMeshProUGUI>().text;

            // Special case for "Atmosphere" clicked
            if (clickedName == "Atmosphere")
            {
                clickedName = "Earth";
            }

            // Check if the clicked planet is the current objective
            if (clickedName != objectiveText)
            {
                SetMessage($"Go Complete {objectiveText} first");
            }
            else
            {
                LoadNextScene(clickedName);
            }
        }
    }

    void LoadNextScene(string currentScene)
    {
        SceneManager.LoadScene(currentScene);
        GameData.LastScene = currentScene;
        UpdateObjective(currentScene);
    }

    void UpdateObjective(string completedScene)
    {
        string nextObjective = "";

        switch (completedScene)
        {
            case "Mercury":
                nextObjective = "Venus";
                GameData.Objective = nextObjective;

                break;
            case "Venus":
                nextObjective = "Earth";
                GameData.Objective = nextObjective;

                break;
            case "Earth":
                nextObjective = "Mars";
                GameData.Objective = nextObjective;

                break;
            case "Mars":
                nextObjective = "Complete";
                GameData.Objective = nextObjective;

                break;
        }

        if (!string.IsNullOrEmpty(nextObjective))
        {
            Objective.GetComponent<TextMeshProUGUI>().text = nextObjective;
        }
    }

    // Sets the message and activates the message panel.
    void SetMessage(string message)
    {
        textMsg.GetComponent<TextMeshProUGUI>().text = message;
        textmsgpanel.SetActive(true);
        StartCoroutine(ThreeSecondTimer());
    }

    private IEnumerator ThreeSecondTimer()
    {
        yield return new WaitForSeconds(3f);
        textmsgpanel.SetActive(false);
    }
}
