using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    public float ShipSpeed = 0.5f;
    [SerializeField]
    private Camera MainCamera;
    private Quaternion targetRotation;
    public float rotationSmoothness = 0.1f;
    private float rotSpeed = 660f;
    private Rigidbody rb;
    public float boostMultiplier = 2f;

    public float maxXRotation = 45f; // Maximum rotation angle along the X-axis
    public float minXRotation = -45f; // Minimum rotation angle along the X-axis
    public float maxZRotation = 45f; // Maximum rotation angle along the Z-axis
    public float minZRotation = -45f; // Minimum rotation angle along the Z-axis



    void Start()
    {
        targetRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        ProcessMovement();
        ProcessRotation();
    }

    void ProcessMovement()
    {
        Vector3 moveDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) moveDirection += MainCamera.transform.forward;
        if (Input.GetKey(KeyCode.S)) moveDirection -= MainCamera.transform.forward;
        if (Input.GetKey(KeyCode.A)) moveDirection -= MainCamera.transform.right;
        if (Input.GetKey(KeyCode.D)) moveDirection += MainCamera.transform.right;

        if (moveDirection.magnitude > 0) moveDirection.Normalize();

        float currentSpeed = ShipSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            currentSpeed *= boostMultiplier;
        }

        rb.velocity = moveDirection * currentSpeed * Time.deltaTime;
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            float rotx = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float roty = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

            targetRotation *= Quaternion.Euler(roty, rotx, 0);
            targetRotation = ClampRotationAroundXAndZAxis(targetRotation);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            targetRotation = Quaternion.identity;
        }
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothness * Time.deltaTime);
    }

    Quaternion ClampRotationAroundXAndZAxis(Quaternion q) //basically lets the limits that player can move in the x y and z axis in terms of rotation as to make it less funky
    {
        q.eulerAngles = new Vector3(ClampAngle(q.eulerAngles.x, minXRotation, maxXRotation),
                                    q.eulerAngles.y,
                                    ClampAngle(q.eulerAngles.z, minZRotation, maxZRotation));
        return q;
    }

    float ClampAngle(float angle, float min, float max)
    {
        angle = NormalizeAngle(angle);
        return Mathf.Clamp(angle, min, max);
    }

    float NormalizeAngle(float angle)
    {
        while (angle > 180) angle -= 360;
        while (angle < -180) angle += 360;
        return angle;
    }

}