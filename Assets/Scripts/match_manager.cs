using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class match_manager : MonoBehaviour
{
    [SerializeField] private GameObject mainDeck;
    main_deck _mainDeckEnv;
    GameObject[] players;
    public int HowManyCards;

    void Awake()
    {
        _mainDeckEnv = mainDeck.GetComponent<main_deck>();
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    void Start()
    {
        HandOutCardsToPlayers();
    }

    void HandOutCardsToPlayers()
    {
        foreach (var player in players)
        {
            player.GetComponent<player_manager>().PlayersDeck = _mainDeckEnv.returnRandomDeck(HowManyCards);
        }
    }

}
