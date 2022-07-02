using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public enum MoveType
{
    Start,
    Ongoing
}

public class match_manager : MonoBehaviour
{
    [SerializeField] private GameObject mainDeck;
    main_deck _mainDeckEnv;

    internal GameObject[] bots;
    internal player_manager mainPlayer;
    internal int playerIndexMove;
    

    display_deck displayDeck;
    public int HowManyCards;

    public GameObject BotPrefab;
    public int howManyBots;
    Transform[] _botsPositions;

    

   internal CardType currentCardType;
   internal MoveType currentMoveType;
   internal int howManyCardsThrowedAtCurrentMove;
   internal GameObject previousPlayer;

   private moveDeckManager mdm;
   private bool isGameOver = false;
   private GameObject _cardTypeField;

    void Awake()
    {
        mdm = GameObject.FindGameObjectWithTag("MoveDeck").GetComponent<moveDeckManager>();
        _mainDeckEnv = mainDeck.GetComponent<main_deck>();
        bots = new GameObject[howManyBots];
        _botsPositions = new Transform[howManyBots];
        displayDeck = this.GetComponent<display_deck>();
        mainPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<player_manager>();
        _cardTypeField = GameObject.FindGameObjectWithTag("Canvas").transform.GetChild(0).Find("Offered_Card_Type").gameObject;
    }

    void Start()
    {
        currentMoveType = MoveType.Start;
        instantiateBots();
        HandOutCardsToPlayers();
        displayDeck.FitCards();
        playerIndexMove = 0; //Random.Range(0, howManyBots); // + 1 - + player
        onCardTypeButtonPressed.CardTypeButtonPressed += endMove;
    }

    void Update()
    {
        if (isGameOver)
        {
            // end of the game
            // congrats for the winner
            Debug.Log("Game is over!");
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

        //if (previousPlayer != null && previousPlayer.name == "Player") Debug.Log(previousPlayer.GetComponent<player_manager>().howManyCardsThrowed);
        //else if (previousPlayer != null) Debug.Log(previousPlayer.GetComponent<bot_manager>().howManyThrowed);
    }


    void passMoveToPlayer()
    {
        //playerIndexMove = Random.Range(0, howManyBots + 1); // + 1 - + player // CHECK IT
        if (playerIndexMove == 0)
        {
            mainPlayer.isMyTurn = true;
            mainPlayer.turnIndicator.color = Color.green;
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
        if (previousPlayer)
        {
            bool isPreviousPlayerBot = !previousPlayer.CompareTag("Player");
            if (isPreviousPlayerBot)
            {
                if (previousPlayer.GetComponent<bot_manager>().PlayersDeck.Count == 0)
                    return true;
            }
            else if(previousPlayer.GetComponent<player_manager>().PlayersDeck.Count == 0)
                return true;
        }

        return false;
    }

    public void endMove(bool isLastMoveLie = false, bool cardsWereChecked = false)
    {
        if(isGameOver = endGameCheck()) return;

        if (cardsWereChecked)
        {
            bool isPreviousPlayerBot = previousPlayer.tag != "Player";
            bool isCurrentPlayerBot = playerIndexMove != 0;

            if (isLastMoveLie && !isPreviousPlayerBot)
            {
                mainPlayer._preparedForMove = false;
                _cardTypeField.GetComponent<CardTypeChange>().ResetCardType();
            }
            
            if (!isLastMoveLie)
            {
                if (!isCurrentPlayerBot) mainPlayer.turnIndicator.color = Color.red;
                _cardTypeField.GetComponent<CardTypeChange>().waitForPlayersInput();
                ChangeIndexMove();
            }

            currentMoveType = MoveType.Start;

            if (playerIndexMove == 0)
            {
                _cardTypeField.GetComponent<CardTypeChange>().ResetCardType();
            }
            else
            {
                _cardTypeField.GetComponent<CardTypeChange>().waitForPlayersInput();
            }

            mdm.ResetAddedCardsCount();
        }
        else
        {
            ChangeIndexMove();
        }

    }

    public void ChangeIndexMove()
    {
        if (playerIndexMove == 0)
        {
            mainPlayer.isMyTurn = false;
            mainPlayer.turnIndicator.color = Color.red;
            previousPlayer = mainPlayer.gameObject;
        }
        else
        {
            bots[playerIndexMove - 1].GetComponent<bot_manager>().isMyTurn = false;
            previousPlayer = bots[playerIndexMove - 1];
        }

        if (playerIndexMove + 1 < howManyBots + 1)
            playerIndexMove++;
        else playerIndexMove = 0;

        if (currentMoveType == MoveType.Start)
            currentMoveType = MoveType.Ongoing;

        if (playerIndexMove == 0)
        {
            mainPlayer._preparedForMove = false;
        }
    }

    internal void IncreaseThrowedCardsAtCurrentMove()
    {
        howManyCardsThrowedAtCurrentMove++;
    }

    void ResetThrowedCardsAtMove()
    {
        howManyCardsThrowedAtCurrentMove = 0;
    }
}
