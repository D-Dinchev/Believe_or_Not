using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_manager : MonoBehaviour
{
    public List<GameObject> PlayersDeck;
    internal bool isMyTurn;
    internal int howManyCardsThrowed;

    void Start()
    {
        onCardClick.onCardAdded += increaseThrowedCards;
    }

    void Update()
    {
        if (isMyTurn)
        {
            
        }
        
    }

    void startMove()
    {

    }

    void ongoingMove()
    {

    }

    void increaseThrowedCards()
    {
        howManyCardsThrowed++;
    }
}
