using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class main_deck : MonoBehaviour
{
    public List<Texture2D> Cards;

    void Awake()
    {
        Cards = Resources.LoadAll<Texture2D>("Cards").ToList();
    }

    public List<Texture2D> returnRandomDeck(int howManyCards)
    {
        List<Texture2D> randomDeck = new List<Texture2D>();
        for (int i = 0; i < howManyCards; i++)
        {
            int randomIndex = Random.Range(0, Cards.Count);
            randomDeck.Add(Cards[randomIndex]);
            Cards.RemoveAt(randomIndex);

        }

        return randomDeck;
    }

}

