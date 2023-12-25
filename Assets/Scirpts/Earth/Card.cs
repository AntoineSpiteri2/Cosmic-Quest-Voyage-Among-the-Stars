using System.Collections;
using System.Collections.Generic;
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


    }
