using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Controller_Rover_system : System.Object
{




    [Header("------------WHEELS BASE SETUP------------")]

    [Tooltip("If  true - Use Torque in this wheels")]
    public bool ThisMotor;


    [Tooltip("If  true - reverse this wheels")]
    public bool Reverse;

    [Tooltip("If  true - this wheel's can steering")]
    public bool ThisSteering;



    [Tooltip("If _ThisSteereng == true - reverse steering angle")]
    public bool SteeringRevers;


    [Header("------------  WHEELS COLLIDER  ------------")]
    [Tooltip("Left Wheel Collider")]
    public WheelCollider Left_Wheel_Collider;
    [Tooltip("Right Wheel Collider")]
    public WheelCollider Right_Wheel_Collider;
    [Header("------------     WHEELS MESH      ------------")]
    [Tooltip("Left Wheel mesh")]
    public GameObject Left_Wheel_Mesh;
    [Tooltip("Right Wheel Mesh")]
    public GameObject Right_Wheel_Mesh;

}

public class Controller_Rover : MonoBehaviour
{



    [Header("WHEELS SETUP")]
    public List<Controller_Rover_system> Wheel_Settings;

    [Header("ENGINE POWER SETTINGS")]

    [Tooltip("Maximum engine power. If more - faster acceleration and torque.If less - slower and weaker. For power per speed control - use torque curve")]
    [Range(16.0f, 8192.0f)]
    public float MaxPower = 750f;

    [Tooltip("Maximum speed in KPH. For power per speed control - use torque curve")]
    [Range(10.0f, 220.0f)]
    public float MaxSpeed = 45f;
    [Tooltip("Torque curve - needed for power per speed control. to example - big power in small speed  - helpful for hillclimbing,  big power in hight speed - good for sportcars")]
    public AnimationCurve Torque_Curve;

    [Header("STEER SETTINGS")]

    [Range(0.1f, 55f)]



    [Tooltip("Wheels steering - maximum angle in Low speed")]
    public float MaxAngle = 45f;
    [Range(0.1f, 40.0f)]


    [Tooltip("Wheels steering - maximum angle in hight speed")]
    public float MaxAngle_InSpeed = 8.0f;
    [Range(-1.2f, 1.2f)]

    [Tooltip("Differential balance")]
    public float Differential_Power = 0.1f;

    [Header("BODY SETTINGS")]
    public Vector3 CenterOfMass_offset;

    public Rigidbody RoverRigidBody;
    private bool Sleep_Rigidbody_Debug;


    [Header("VEHICLE DRIVING DATA")]

    [Tooltip("Current torque")]
    public float FinalPower;
    [Tooltip("Current speed in unity engine units")]
    public float CurrentSpeed;

    [Tooltip("Current speed in KPH")]
    public float CurrentSpeed_KPH;

    [Tooltip("Current speed in MPH")]
    public float CurrentSpeed_MPH;
    [Tooltip("RPM counter")]
    public float WheelRPM;

    [Tooltip("Wheel for RPM counter")]
    public WheelCollider RPM_counter_collider;



    private float Torque_Curve_Lerp;
    private float SleepDelay = 3f;
    private float SleepDelay_current;
    private float TorqueCalc;

    private float speedFraction;
    private float motor2;

    private NavMeshAgent navMeshAgent;

    public Transform destinationWaypoint; // Assign this in the Inspector or via code


    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestinationToWaypoint();

    }

    void SetDestinationToWaypoint()
    {
        if (destinationWaypoint != null)
        {
            navMeshAgent.SetDestination(destinationWaypoint.position);

        }
    }

    public void Wheel_Render(Controller_Rover_system Wheel_proces)
    {

        Vector3 Wheel_position;
        Quaternion Wheel_rotation;

         

        Wheel_proces.Left_Wheel_Collider.GetWorldPose(out Wheel_position, out Wheel_rotation);
        Wheel_proces.Left_Wheel_Mesh.transform.position = Wheel_position;
        Wheel_proces.Left_Wheel_Mesh.transform.rotation = Wheel_rotation;

       


        Wheel_proces.Right_Wheel_Collider.GetWorldPose(out Wheel_position, out Wheel_rotation);
        Wheel_proces.Right_Wheel_Mesh.transform.position = Wheel_position;
        Wheel_proces.Right_Wheel_Mesh.transform.rotation = Wheel_rotation;
        
    }









    public void FixedUpdate()
    {
        // Calculate speed fraction based on NavMesh Agent's velocity
        speedFraction = navMeshAgent.velocity.magnitude / navMeshAgent.speed;
        motor2 = MaxPower * speedFraction;

        // Update current speed and wheel RPM
        CurrentSpeed = RoverRigidBody.velocity.magnitude;
        WheelRPM = (CurrentSpeed < 0.09f && Mathf.Abs(speedFraction) < 0.15f) ? 0.0f : RPM_counter_collider.rpm;
        CurrentSpeed_KPH = CurrentSpeed * 3.6f;
        CurrentSpeed_MPH = CurrentSpeed * 2.23f;

        // Handle sleep state for efficiency
        if (CurrentSpeed < 0.09f && Mathf.Abs(speedFraction) < 0.15f)
        {
            SleepDelay_current += Time.fixedDeltaTime;
            if (SleepDelay_current > SleepDelay)
            {
                RoverRigidBody.Sleep();
                CurrentSpeed_KPH = 0.0f;
                CurrentSpeed_MPH = 0.0f;
            }
        }
        else
        {
            SleepDelay_current = 0.0f;
        }

        Sleep_Rigidbody_Debug = RoverRigidBody.IsSleeping();

        // Calculate torque curve lerp
        Torque_Curve_Lerp = Mathf.Clamp01(CurrentSpeed_KPH / MaxSpeed);
        TorqueCalc = (CurrentSpeed_KPH < MaxSpeed) ? MaxPower * -1 * speedFraction * Torque_Curve.Evaluate(Torque_Curve_Lerp) : 0.0f;

        // Apply calculated motor torque
        foreach (Controller_Rover_system Wheel_proces in Wheel_Settings)
        {
            // Motor torque
            if (Wheel_proces.ThisMotor)
            {
                Wheel_proces.Left_Wheel_Collider.motorTorque = Wheel_proces.Right_Wheel_Collider.motorTorque = motor2;
            }

            // Braking
            float brakePower = Mathf.Abs(Input.GetAxis("Jump"));
            brakePower = (brakePower > 0.0015f) ? MaxPower : 0;
            Wheel_proces.Left_Wheel_Collider.brakeTorque = Wheel_proces.Right_Wheel_Collider.brakeTorque = brakePower;

            // Update wheel visuals
            Wheel_Render(Wheel_proces);
        }

        FinalPower = TorqueCalc;
    }






}





