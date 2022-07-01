using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private static Sprite facedownTexture;
    private Sprite faceupTexture;


    void Awake()
    {
        faceupTexture = GetComponent<SpriteRenderer>().sprite;
        facedownTexture = Resources.Load<Sprite>("Prefabs/facedown_card");
    }

    internal void cardAddedToMoveDeck()
    {
        GetComponent<SpriteRenderer>().sprite = facedownTexture;
    }

    internal void flipCardToFace()
    {
        GetComponent<SpriteRenderer>().sprite = faceupTexture;
    }

    internal void flipCardToDown()
    {
        GetComponent<SpriteRenderer>().sprite = facedownTexture;
    }
}
