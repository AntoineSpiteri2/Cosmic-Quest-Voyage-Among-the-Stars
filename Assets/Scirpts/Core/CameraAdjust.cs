using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjust : MonoBehaviour
{
    private float targetAspectRatio = 16f / 9f; // Target aspect ratio, change if needed

    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
        AdjustCamera();
    }

    void AdjustCamera()
    {
        // Calculate the current aspect ratio
        float windowAspect = (float)Screen.width / (float)Screen.height;

        // Scale the camera's field of view based on the current aspect ratio
        if (windowAspect < targetAspectRatio)
        {
            // If the screen is taller, adjust field of view to fit target aspect ratio
            float scaleHeight = windowAspect / targetAspectRatio;
            _camera.rect = new Rect(0, (1 - scaleHeight) / 2, 1, scaleHeight);
        }
        else
        {
            // If the screen is wider, adjust field of view to fit target aspect ratio
            float scaleWidth = targetAspectRatio / windowAspect;
            _camera.rect = new Rect((1 - scaleWidth) / 2, 0, scaleWidth, 1);
        }
    }

    void Update()
    {
        // Optional: Adjust camera during runtime if the window size changes
        AdjustCamera();
    }
}
