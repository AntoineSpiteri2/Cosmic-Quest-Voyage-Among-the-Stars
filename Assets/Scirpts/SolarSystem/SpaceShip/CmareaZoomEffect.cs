using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmareaZoomEffect : MonoBehaviour
{

    public CinemachineVirtualCamera virtualCamera;
    public GameObject targetObject; // The object whose speed we're tracking

    public float minFOV = 70f; // Minimum FOV
    public float maxFOV = 120f; // Maximum FOV
    public float maxSpeed = 50f; // Speed at which max FOV is reached

    public float smoothTime = 0.3f; // Time to smooth FOV transition

    private Vector3 lastPosition;
    private float currentSpeed;

    void Start()
    {
        lastPosition = targetObject.transform.position;
    }

    void Update()
    {
        // Calculate speed
        float distance = Vector3.Distance(targetObject.transform.position, lastPosition);
        float instantSpeed = distance / Time.deltaTime;
        currentSpeed = Mathf.Lerp(currentSpeed, instantSpeed, Time.deltaTime / smoothTime); // Smooth the speed value
        lastPosition = targetObject.transform.position;

        // Adjust FOV based on speed
        if (virtualCamera != null)
        {
            float targetFOV = Mathf.Lerp(minFOV, maxFOV, currentSpeed / maxSpeed);
            targetFOV = Mathf.Clamp(targetFOV, minFOV, maxFOV); // Ensure FOV is within bounds




            virtualCamera.m_Lens.FieldOfView = targetFOV;
        }
    }
}
