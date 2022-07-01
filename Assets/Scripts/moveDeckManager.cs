using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveDeckManager : MonoBehaviour
{
    public List<GameObject> moveDeck = new List<GameObject>();
    private int howManyAdded = 0;
    private float angleGap = 9f;

    void Start()
    {
        onCardClick.onCardAdded += cardAdded;
    }


    internal void cardAdded()
    {
        GameObject addedCard = moveDeck[moveDeck.Count - 1];
        addedCard.GetComponent<SpriteRenderer>().sortingLayerName = "MoveDeckCard";
        addedCard.GetComponent<SpriteRenderer>().sortingOrder = moveDeck.IndexOf(addedCard);
        addedCard.GetComponent<CardManager>().cardAddedToMoveDeck();
        StartCoroutine(LerpCardPosition(transform.position, 0.5f, addedCard.transform, () =>
        {
            addedCard.transform.Rotate(0f, 0f, howManyAdded * angleGap);
            howManyAdded++;
        }));

        addedCard.GetComponent<OnMouseOverCard>().enabled = !addedCard.GetComponent<OnMouseOverCard>().enabled;
        addedCard.GetComponent<onCardClick>().enabled = !addedCard.GetComponent<onCardClick>();

    }

    IEnumerator LerpCardPosition(Vector2 endValue, float duration, Transform valueToLerp, Action whenDone)
    {
        float time = 0f;
        Vector2 startValue = valueToLerp.position;

        while (time < duration)
        {
            valueToLerp.position = Vector2.Lerp(startValue, endValue, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        valueToLerp.position = endValue;
        whenDone();
    }

    internal void ResetAddedCardsCount()
    {
        howManyAdded = 0;
    }
}
