using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Card : MonoBehaviour
{

    public enum CardType
    {
        PolarBear,
        Lion,
        Fish,
        Eagle,
        Monkey,
        Camel,
        Penguin,
        Elephant
    }
    public CardType cardType;

    public List<Texture2D> Images;

    public static bool isChecking = false;


    public bool isClicked = false;

    public bool isaPair = false;

    public bool beingclicked = false;


    public Material material;

    public GameObject[] moves;
    private TextMeshProUGUI movesText;



    private void Start()
    {
        this.gameObject.name = cardType.ToString();
        moves =  GameObject.FindGameObjectsWithTag("Respawn");
        movesText = moves[0].GetComponent<TextMeshProUGUI>();
    }


    public void SetCardImage()
    {

        Renderer renderer = GetComponent<Renderer>();

        switch (cardType)
        {
            case CardType.PolarBear:
                renderer.material.mainTexture = Images[0];
                break;
            case CardType.Lion:
                renderer.material.mainTexture = Images[1];
                break;
            case CardType.Fish:
                renderer.material.mainTexture = Images[2];
                break;
            case CardType.Eagle:
                renderer.material.mainTexture = Images[3];
                break;
            case CardType.Monkey:
                renderer.material.mainTexture = Images[4];
                break;
            case CardType.Camel:
                renderer.material.mainTexture = Images[5];
                break;
            case CardType.Penguin:
                renderer.material.mainTexture = Images[6];
                break;
            case CardType.Elephant:
                renderer.material.mainTexture = Images[7];
                break;


        }

    }




    void OnMouseDown()
    {
        if (!isClicked && !beingclicked && !Card.isChecking)
        {
            beingclicked = true;
            SetCardImage();
            isClicked = true;

            // Check the number of cards that are clicked and not part of a pair
            var clickedCards = FindObjectsOfType<Card>().Count(c => c.isClicked && !c.isaPair);

            if (clickedCards == 1)
            {
                // Only one card is clicked, so allow clicking another card
                Card.isChecking = false;
            }
            else if (clickedCards == 2)
            {
                // Two cards are clicked, begin checking for a match
                Card.isChecking = true;
                StartCoroutine(CheckForMatch());
            }
        }
    }
    IEnumerator CheckForMatch()
    {
        yield return new WaitForSeconds(1); // Wait for a short duration to allow the player to see the second card

        bool matchFound = false;
        Card otherCard = null;


        // Check for matching card
        foreach (Transform child in gameObject.transform.parent.transform)
        {
            if (child.gameObject.name == gameObject.name && child.gameObject != gameObject)
            {
                otherCard = child.gameObject.GetComponent<Card>();
                if (otherCard != null && otherCard.isClicked)
                {
                    matchFound = true;
                    Debug.Log("A match");


                    // Inside CheckForMatch method, wherever MOVES is incremented
                    GameData.Moves += 1;
                    Debug.Log("Moves incremented to: " + GameData.Moves);
                    movesText.text = GameData.Moves.ToString();
                    GameData.matches += 1; 

                    isaPair = true;
                    isClicked = false;
                    beingclicked = false;
                    otherCard.isaPair = true;
                    otherCard.isClicked = false;
                    otherCard.beingclicked = false;
                    Card.isChecking = false;

                    if (GameData.matches == 8)
                    {
                        OnLevelComplete();  

                    }
                    break;
                }
            }
        }

        // If no match found, reset both card states
        if (!matchFound)
        {
            // Inside CheckForMatch method, wherever MOVES is incremented
            GameData.Moves += 1;
            Debug.Log("Moves incremented to: " + GameData.Moves);
            movesText.text = GameData.Moves.ToString();

            Debug.Log("Not a match");
            Debug.Log(gameObject.name);


            StartCoroutine(ResetCard()); // reset the last clicked card if so 

            foreach (Transform child in gameObject.transform.parent.transform)
            {


                if (child.GetComponent<Card>().isClicked == true && child.GetComponent<Card>().beingclicked == true && child.GetComponent<Card>().isaPair == false)
                {
                    Debug.Log("Triggerd");
                    Debug.Log(child.name);
                    child.GetComponent<Card>().ResetC(); // reset the  first clicked card
                }
            }



            // Reset the checking flag after the coroutine for resetting cards is started
            yield return new WaitForSeconds(2); // Wait until the reset is likely done
            Card.isChecking = false;
        }


      


      











    }


    public void ResetC() // tried calling the coroutine directly but this wont work so this kind of method to fix the issue
    {
        StartCoroutine(ResetCard());
    }

    public IEnumerator ResetCard()
    {
        yield return new WaitForSeconds(2); // Wait for a short duration before resetting

        isClicked = false;
        beingclicked = false;
        GetComponent<Renderer>().material.mainTexture = null;

        // Only reset the isChecking flag if this card is not a pair
        if (!isaPair)
        {
            isChecking = false;
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
