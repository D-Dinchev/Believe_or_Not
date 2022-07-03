using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_manager : MonoBehaviour
{
    internal List<GameObject> PlayersDeck;

    internal Image turnIndicator;
    internal bool isMyTurn;
    internal int howManyCardsThrowed;
    private match_manager mm;

    internal bool _preparedForMove = false;

    void Awake()
    {
        turnIndicator = GameObject.FindGameObjectWithTag("PlayerZonePanel").transform.GetChild(0).GetComponent<Image>();
        mm = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<match_manager>();
    }

    void Start()
    {
        onCardClick.onCardAdded += increaseThrowedCards;
    }

    void Update()
    {
        if (!_preparedForMove)
        {
            if (isMyTurn && mm.currentMoveType == MoveType.Start)
            {
                prepareForStartMove();
                _preparedForMove = true;
            }
            else if (isMyTurn && mm.currentMoveType == MoveType.Ongoing)
            {
                prepareForOngoingMove();
                _preparedForMove = true;
            }
        }
    }

    void prepareForStartMove()
    {
        foreach (var card in PlayersDeck)
        {
            card.GetComponent<OnMouseOverCard>().enabled = true;
            card.GetComponent<onCardClick>().enabled = true;
        }

        onCardClick.threwedMoreThanTwo = false;
        howManyCardsThrowed = 0;
    }

    void prepareForOngoingMove()
    {
        foreach (var card in PlayersDeck)
        {
            card.GetComponent<OnMouseOverCard>().enabled = true;
            card.GetComponent<onCardClick>().enabled = true;
        }

        onCardClick.threwedMoreThanTwo = false;
        howManyCardsThrowed = 0;
    }

    void increaseThrowedCards()
    {
        howManyCardsThrowed++;
    }
}
