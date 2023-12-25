using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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


    Card THISCARD = null;




    private void Start()
    {
        this.gameObject.name = cardType.ToString();
        THISCARD = gameObject.GetComponent<Card>();
    }


    public void SetCardImage() {

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
                isaPair = true;
                otherCard.isaPair = true;
                break;
            }
        }
    }

    // If no match found, reset both card states
    if (!matchFound)
    {
        Debug.Log("Not a match");
        StartCoroutine(ResetCard());
        if (otherCard != null)
        {
            StartCoroutine(otherCard.ResetCard());
        }
    }

    // Reset the checking flag after the coroutine for resetting cards is started
    yield return new WaitForSeconds(2); // Wait until the reset is likely done
    Card.isChecking = false; 
}

IEnumerator ResetCard()
{
    yield return new WaitForSeconds(2); // Wait for a short duration before resetting

    isClicked = false;
        Debug.Log(gameObject.name);
    beingclicked = false;
    GetComponent<Renderer>().material.mainTexture = null;

    // Only reset the isChecking flag if this card is not a pair
    if (!isaPair)
    {
        isChecking = false; 
    }
}











}
