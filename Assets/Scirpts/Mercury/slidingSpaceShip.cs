using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class slidingSpaceShip : MonoBehaviour
{

    public float speed = 1f;
    public float MinX = 50;
    public float MaxX = 150;
    public float forwardSpeed = 5.0f;


    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame

    void Update()
    {
        // Get the current position
        Vector3 position = gameObject.transform.position;

        // Clamp the X position
        position.x = Mathf.Clamp(position.x, MinX, MaxX);

        // Move forward in the Z-axis
        position.z += forwardSpeed * Time.deltaTime;

        // Apply the clamped and updated position back to the gameObject
        gameObject.transform.position = position;

        // move sliding ship horizontally
        float h = Input.GetAxisRaw("Horizontal");
        Vector2 movement = new Vector2(h * speed, rb.velocity.y);
        rb.velocity = Vector3.Lerp(rb.velocity, movement, 2f * Time.deltaTime);

        if (gameObject.transform.position.z >= 70)
        {
            Debug.Log("going to solarsystem");
            OnLevelComplete();

        }
    }
    public void OnLevelComplete()
    {
        GameData.LevelsCompleted++;
        GameData.CurrentLevel++; // Assuming next level is the current level after completion
        GameData.NumberOfRetries = 0;
        SceneManager.LoadScene("SolarSystem");

        // Load next level or handle completion...
    }


}
