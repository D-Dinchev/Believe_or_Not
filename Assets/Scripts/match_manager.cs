using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class match_manager : MonoBehaviour
{
    [SerializeField] private GameObject mainDeck;
    main_deck _mainDeckEnv;

    GameObject[] bots;
    player_manager mainPlayer;
    internal int playerIndexMove;
    

    display_deck displayDeck;
    public int HowManyCards;

    public GameObject BotPrefab;
    public int howManyBots;
    Transform[] _botsPositions;

    

   internal CardType currentCardType;

    void Awake()
    {
        _mainDeckEnv = mainDeck.GetComponent<main_deck>();
        bots = new GameObject[howManyBots];
        _botsPositions = new Transform[howManyBots];
        displayDeck = this.GetComponent<display_deck>();
        mainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player_manager>();
    }

    void Start()
    {
        instantiateBots();
        HandOutCardsToPlayers();
        displayDeck.FitCards();
        playerIndexMove = 0; //Random.Range(0, howManyBots); // + 1 - + player
        onCardTypeButtonPressed.CardTypeButtonPressed += endMove;
    }

    void Update()
    {
        if (endGameCheck())
        {
            // end of the game
            // congrats for the winner
        }
        else
        {
            /*
             * moveResult = passMoveToPlayer(playerID, startMove\ongoingMove, ResultOfTurn)
             *
             * endOfMoveHandler(moveResult);
             * endOfMoveHandler() {
             *     if (win) { dropCards/returnToPreviousPlayer; setMoveToThisPlayer;}
             *     if (lose) { sendCardsToThisPlayer'sDeck; setMoveToNextPlayer; } 
             * }
             *
             */
            passMoveToPlayer();
            
        }
    }

    public class ResultOfTurn
    {
        public bool isWin;
        public bool dropCards;
    }

    void passMoveToPlayer()
    {
        playerIndexMove = Random.Range(0, howManyBots + 1); // + 1 - + player // CHECK IT
        if (playerIndexMove == 0)
        {
            mainPlayer.isMyTurn = true;
        }
        else
        {
            bots[playerIndexMove - 1].GetComponent<bot_manager>().isMyTurn = true;
        }
    }

    void HandOutCardsToPlayers()
    {
        mainPlayer.PlayersDeck = _mainDeckEnv.returnRandomDeck(HowManyCards); // hand out to player
        foreach (var bot in bots)
        {
            bot.GetComponent<bot_manager>().PlayersDeck = _mainDeckEnv.returnRandomDeck(HowManyCards); // hand out to bots
        }
    }

    void instantiateBots()
    {
        GameObject bp = GameObject.FindGameObjectWithTag("Positions");


        if (howManyBots == 1)
        {
            _botsPositions[howManyBots - 1] = bp.transform.GetChild(howManyBots - 1);
        }
        else
        {
            GameObject objectWithPositions =  bp.transform.GetChild(howManyBots - 1).gameObject;
            for (int i = 0; i < objectWithPositions.transform.childCount; i++)
            {
                _botsPositions[i] = objectWithPositions.transform.GetChild(i);
            }
        }

        for (int i = 0; i < howManyBots; i++)
        {
            bots[i] = Instantiate(BotPrefab, _botsPositions[i].position, Quaternion.identity);
            bots[i].name = "Bot " + (i + 1);
        }
    }

    bool endGameCheck()
    {
        if (mainPlayer.PlayersDeck.Count == 0)
        {
            return true;
        }

        foreach (var bot in bots)
        {
            if (bot.GetComponent<bot_manager>().PlayersDeck.Count == 0)
            {
                return true;
            }
        }

        return false;
    }

    internal void endMove() // mb put result in it
    {
        if (playerIndexMove == 0)
        {
            mainPlayer.isMyTurn = false;
        }
        else
        {
            bots[playerIndexMove - 1].GetComponent<bot_manager>().isMyTurn = false;
        }

        if (playerIndexMove + 1 < howManyBots)
            playerIndexMove++;
        else playerIndexMove = 0;
    }
}
