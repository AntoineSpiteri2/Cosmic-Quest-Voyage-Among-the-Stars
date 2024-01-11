using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Win : MonoBehaviour
{

    public GameObject score;
    // Start is called before the first frame update
    void Start()
    {
        if (score != null)
        {

            int currentscore = GameData.CurrentScore;

            score.GetComponent<TextMeshProUGUI>().text = currentscore.ToString();
        }
    }


}
