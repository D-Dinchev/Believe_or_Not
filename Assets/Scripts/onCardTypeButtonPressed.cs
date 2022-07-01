using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public enum CardType
{
    Two = 2,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine,
    Ten,
    Jack,
    Queen,
    King,
    Ace,
    Joker
}

public class onCardTypeButtonPressed : MonoBehaviour
{
    public static event Action<bool, bool> CardTypeButtonPressed;
    private match_manager mm;

    void Awake()
    {
        mm = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<match_manager>();
    }

    public void onButtonPressed(cardDesicionComponent cdc)
    {
        mm.currentCardType = cdc.cardType;
        CardTypeButtonPressed?.Invoke(false, false);
    }
}
