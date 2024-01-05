using Cinemachine;
using System.Collections;
using System.Collections.Generic;
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




    private void Update()
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
        else
        {
            // Normal behavior when the target is in front
            Vector2 pos = camera.WorldToScreenPoint(target.position);
            pos.x = Mathf.Clamp(pos.x, img.GetPixelAdjustedRect().width / 2, Screen.width - img.GetPixelAdjustedRect().width / 2);
            pos.y = Mathf.Clamp(pos.y, img.GetPixelAdjustedRect().height / 2, Screen.height - img.GetPixelAdjustedRect().height / 2);
            img.transform.position = pos;
        }
    }


    void OnMouseDown()
    {
        string name = gameObject.name;
        switch (name)
        {
            case "Mercury":
                SceneManager.LoadScene("Mercury");
                break;
            case "Mars":
                SceneManager.LoadScene("Mars");
                break;
            case "Venus":
                SceneManager.LoadScene("Venus");
                break;
            case "Atmosphere":
                SceneManager.LoadScene("Earth");
                break;
            default:
                Debug.Log("not implemented");
                break;
        }
    }
}
