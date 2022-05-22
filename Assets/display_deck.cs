using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class display_deck : MonoBehaviour
{
    [SerializeField] player_manager playerManager;
    public Transform HandDeck;
    private int howManyAdded = 0;
    public int gapBetweenCards = 10;

    public void FitCards()
    {
        if (playerManager.PlayersDeck.Count == 0) return;

        foreach (var cardTexture in playerManager.PlayersDeck)
        {
            GameObject card = new GameObject();
            Sprite cardSprite = Sprite.Create(cardTexture, new Rect(0f, 0f, cardTexture.width, cardTexture.height), new Vector2(0.5f, 0.5f), 100);
            SpriteRenderer sr = card.AddComponent<SpriteRenderer>() as SpriteRenderer;
            card.transform.position = HandDeck.transform.position + new Vector3(howManyAdded * gapBetweenCards, 0, 0);
            card.transform.SetParent(HandDeck);
            sr.sprite = cardSprite;
            card.name = cardTexture.name;

            howManyAdded++;
        }
    }
}
