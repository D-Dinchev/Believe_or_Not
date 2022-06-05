using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class main_deck : MonoBehaviour
{
    public List<GameObject> Cards;

    void Awake()
    {
        Texture2D[] textures;
        textures = Resources.LoadAll<Texture2D>("Cards");
        foreach (var texture in textures)
        {
            GameObject card = new GameObject();
            Sprite cardSprite = Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
            SpriteRenderer sr = card.AddComponent<SpriteRenderer>() as SpriteRenderer;

            sr.sprite = cardSprite;
            sr.sprite.name = card.name = texture.name;
            card.tag = "Card";

            card.transform.position = transform.position;

            card.AddComponent<CardManager>();
            card.AddComponent<OnMouseOverCard>();
            card.AddComponent<onCardClick>();
            BoxCollider2D bc = card.AddComponent<BoxCollider2D>();

            bc.size = new Vector2(bc.size.x / 2, bc.size.y);
            bc.offset = new Vector2(-1, bc.offset.y);

            Cards.Add(card);
            card.transform.parent = transform;
        }
    }

    public List<GameObject> returnRandomDeck(int howManyCards)
    {
        List<GameObject> randomDeck = new List<GameObject>();
        for (int i = 0; i < howManyCards; i++)
        {
            int randomIndex = Random.Range(0, Cards.Count);
            randomDeck.Add(Cards[randomIndex]);
            Cards.RemoveAt(randomIndex);

        }

        return randomDeck;
    }

}

