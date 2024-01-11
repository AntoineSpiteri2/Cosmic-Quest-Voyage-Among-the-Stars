using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10; // Adjust the damage value as needed
    public Rigidbody rb;
    public float lifetime = 5f; // How long the bullet exists before being destroyed

    private Vector3 direction; // Field to store the direction of the bullet



    void Start()
    {
        damage = GameData.MarsDmg;

        Destroy(gameObject, lifetime);

    }

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized; // Ensure the direction is normalized
    }

    void Update()
    {
        // Move the bullet in the set direction
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.gameObject.tag == "Player")
        {
            hitInfo.gameObject.GetComponent<AudioSource>().Play();

            GameManager.Instance.ReducePlayer(damage);
            Destroy(gameObject); // Destroy the bullet after it hits the player
        }
        else
        {
            Destroy(gameObject); // Destroy the bullet after hitting something else
        }
    }
}
