using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public GameObject cardPrefab; // Assign your Card prefab in the Inspector
    public GameObject cardContainer; // Assign your card container GameObject in the Inspector

    public int gridWidth = 4;
    public int gridHeight = 4;
    public float cardSpacing = 1f;

    private void Start()
    {
        SpawnAndShuffleCards();
    }

    void SpawnAndShuffleCards()
    {
        List<Card.CardType> deck = GenerateDeck();
        ShuffleDeck(deck);

        for (int i = 0; i < gridWidth * gridHeight; i++)
        {
            Vector3 position = CalculateCardPosition(i);
            GameObject newCardObj = Instantiate(cardPrefab, position, Quaternion.identity, cardContainer.transform); // Set the card's parent to cardContainer
            Card newCard = newCardObj.GetComponent<Card>();
            newCard.cardType = deck[i];
        }
    }

    List<Card.CardType> GenerateDeck()
    {
        List<Card.CardType> newDeck = new List<Card.CardType>();

        foreach (Card.CardType type in System.Enum.GetValues(typeof(Card.CardType)))
        {
            newDeck.Add(type);
            newDeck.Add(type); // Add each type twice for pairs
        }

        return newDeck;
    }

    void ShuffleDeck(List<Card.CardType> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            Card.CardType temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    Vector3 CalculateCardPosition(int index)
    {
        // Calculate the position based on the index, grid width and height
        // Adjust these values as necessary to fit your game's layout
        float x = (index % gridWidth) * cardSpacing;
        float z = (index / gridWidth) * cardSpacing;
        return new Vector3(x, 0, z);
    }
}
