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

    void Start()
    {
    }

    internal void cardAddedToMoveDeck()
    {
        GetComponent<SpriteRenderer>().sprite = facedownTexture;
    }
}
