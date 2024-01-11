using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class rock : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameData.rocksCollected += 1;
            GameObject text = GameObject.Find("Rocks");

            text.GetComponent<TextMeshProUGUI>().text = GameData.rocksCollected.ToString();
            Destroy(gameObject);
        }
    }

}
