using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class match_manager : MonoBehaviour
{
    [SerializeField] private GameObject mainDeck;
    main_deck _mainDeckEnv;
    GameObject[] bots;
    [SerializeField] player_manager mainPlayer;
    private display_deck displayDeck;
    public int HowManyCards;

    void Awake()
    {
        _mainDeckEnv = mainDeck.GetComponent<main_deck>();
        bots = GameObject.FindGameObjectsWithTag("Bot");
        displayDeck = this.GetComponent<display_deck>();
    }

    void Start()
    {
        HandOutCardsToPlayers();
        displayDeck.FitCards();
    }

    void HandOutCardsToPlayers()
    {
        mainPlayer.PlayersDeck = _mainDeckEnv.returnRandomDeck(HowManyCards); // hand out to player
        foreach (var bot in bots)
        {
            bot.GetComponent<bot_manager>().PlayersDeck = _mainDeckEnv.returnRandomDeck(HowManyCards); // hand out to bots
        }
    }

}
