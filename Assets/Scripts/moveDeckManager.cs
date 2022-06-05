using System.Collections.Generic;
using UnityEngine;

public class moveDeckManager : MonoBehaviour
{
    internal List<GameObject> moveDeck = new List<GameObject>();
    private int howManyAdded = 0;
    private float angleGap = 9f;

    void Start()
    {
        onCardClick.onCardAdded += cardAdded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void cardAdded()
    {
        GameObject addedCard = moveDeck[moveDeck.Count - 1];
        addedCard.GetComponent<SpriteRenderer>().sortingLayerName = "MoveDeckCard";
        addedCard.GetComponent<SpriteRenderer>().sortingOrder = moveDeck.IndexOf(addedCard);
        addedCard.GetComponent<CardManager>().cardAddedToMoveDeck();
        addedCard.transform.position = transform.position;
        addedCard.transform.Rotate(0f, 0f, howManyAdded * angleGap);

        addedCard.GetComponent<OnMouseOverCard>().enabled = !addedCard.GetComponent<OnMouseOverCard>().enabled;
        addedCard.GetComponent<onCardClick>().enabled = !addedCard.GetComponent<onCardClick>();

        howManyAdded++;
    }

    
}
