using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class display_deck : MonoBehaviour
{
    player_manager playerManager;
    Transform HandDeck;
    private int howManyAdded = 0;
    public int gapBetweenCards = 3;

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerManager = player.GetComponent<player_manager>();
        HandDeck = player.transform.GetChild(0); // 0 - Hand Deck
    }

    public void FitCards()
    {
        if (playerManager.PlayersDeck.Count == 0) return;

        foreach (var card in playerManager.PlayersDeck)
        {
            card.transform.eulerAngles = new Vector3(0, 0, 0);
            card.GetComponent<CardManager>().flipCardToFace();
            card.transform.position = HandDeck.transform.position + new Vector3(howManyAdded * gapBetweenCards, 0, 0);
            card.transform.SetParent(HandDeck);

            card.GetComponent<SpriteRenderer>().sortingLayerName = "Card";
            card.GetComponent<SpriteRenderer>().sortingOrder = playerManager.PlayersDeck.IndexOf(card);

            howManyAdded++;
        }
    }

    internal void ResetAddedCardsCount()
    {
        howManyAdded = 0;
    }
}
