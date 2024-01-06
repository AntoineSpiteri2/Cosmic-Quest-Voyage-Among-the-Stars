using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class astroid : MonoBehaviour
{
    public Vector3 size;

    public float MaxSpeed = 5f;
    public float MinSpeed = 10f;
    public float speed;

    public int Dmg = 10;




    void Start()
    {
        // Randomly scale the asteroid
        Vector3 newScale = new Vector3(Random.Range(50, 100), Random.Range(50, 100), Random.Range(50, 100));
        gameObject.transform.localScale = newScale;

        // Update size to reflect the new scale
        size = newScale; // This line is added to update the size variable

        // Randomly set the speed
        speed = Random.Range(MaxSpeed, MinSpeed);

        ApplyRandomTorque();

    }

    private void ApplyRandomTorque()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 randomTorque = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(-10f, 10f));
            rb.AddTorque(randomTorque);
        }
    }



    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -speed * Time.deltaTime);

        if (gameObject.transform.position.z < -100 )
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.Instance.ReducePlayer(Dmg);
            Destroy(gameObject);
        }
    }
}
