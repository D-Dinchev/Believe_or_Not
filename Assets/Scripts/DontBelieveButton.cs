using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DontBelieveButton : MonoBehaviour
{
    public Button DButton;

    private match_manager mm;
    private List<GameObject> _moveDeck;
    private Transform _mainDeck;
    private display_deck _displayDeck;

    void Awake()
    {
        _displayDeck = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<display_deck>();
        mm = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<match_manager>();
        _moveDeck = GameObject.FindGameObjectWithTag("MoveDeck").GetComponent<moveDeckManager>().moveDeck;
        _mainDeck = GameObject.FindGameObjectWithTag("MainDeck").transform;
    }

    void Start()
    {
        DButton.interactable = false;
    }

    void Update()
    {
        if (mm.mainPlayer.isMyTurn && mm.currentMoveType == MoveType.Ongoing &&  mm.mainPlayer.howManyCardsThrowed == 0)
        {
            DButton.interactable = true;
        }
        else DButton.interactable = false;
    }

    public void DontBelieve()
    {
        DButton.interactable = false;
        StartCoroutine(DontBelieveCoroutine());
    }

    internal IEnumerator DontBelieveCoroutine(Action whenDone = null)
    {
        int cardsThrownInPreviousMove = 0;
        bool isLastMoveLie = false;
        bool isPreviousPlayerBot = mm.previousPlayer.name != "Player";
        bool isCurrentPlayerBot = mm.playerIndexMove != 0;
        var currentCardType = mm.currentCardType;
        List<GameObject> previousPlayerDeck = null;

        if (isPreviousPlayerBot && mm.previousPlayer)                                                       // getting previous player's deck
        {
            previousPlayerDeck = mm.previousPlayer.GetComponent<bot_manager>().PlayersDeck;
        }
        else if (mm.previousPlayer)
        {
            previousPlayerDeck = mm.previousPlayer.GetComponent<player_manager>().PlayersDeck;
        }

        // how many cards previous player throw
        if (mm.previousPlayer && !isPreviousPlayerBot) cardsThrownInPreviousMove = mm.previousPlayer.GetComponent<player_manager>().howManyCardsThrowed;
        else if (mm.previousPlayer) cardsThrownInPreviousMove = mm.previousPlayer.GetComponent<bot_manager>().howManyThrowed;

        GameObject[] lastPlayersThrownCards = new GameObject[cardsThrownInPreviousMove];

        for (int i = 0; i < cardsThrownInPreviousMove; i++)
        {
            lastPlayersThrownCards[i] = _moveDeck[_moveDeck.Count - i - 1]; // get last thrown cards
        }

        // check if offered card fits with prev player's throw cards
        foreach (var card in lastPlayersThrownCards)
        {
            if (card.name.Contains(currentCardType.ToString().ToLower()) || card.name.Contains( ( (int)currentCardType).ToString() ) ) // for number cards or king, joker etc.
            {
                // that's fine :)))
            }
            else
            {
                isLastMoveLie = true;
                break;
            }
        }

        if (isCurrentPlayerBot) yield return new WaitForSeconds(2);
        ShowLastCards(lastPlayersThrownCards);
        Debug.Log(isLastMoveLie ? "Lie !" : "Oops..");
        yield return new WaitForSeconds(3.5f);
        HideLastCards(lastPlayersThrownCards);


        if (isLastMoveLie)
        {
            foreach (var card in _moveDeck)
            {
                previousPlayerDeck.Add(card);

                if (isPreviousPlayerBot)
                {
                    card.transform.parent = _mainDeck;
                    card.transform.position = _mainDeck.position;
                    mm.previousPlayer.GetComponent<bot_manager>().IncreaseCardsCount();
                }
                else
                {
                    card.GetComponent<OnMouseOverCard>().enabled = true;
                    card.GetComponent<onCardClick>().enabled = true;
                }
            }

            if (!isPreviousPlayerBot)
            {
                _displayDeck.ResetAddedCardsCount();
                _displayDeck.FitCards();
            }
            _moveDeck.Clear();
            mm.endMove(true, true);
        }
        else
        {
            List<GameObject> currentPlayerDeck;
            if (mm.playerIndexMove == 0) currentPlayerDeck = mm.mainPlayer.PlayersDeck;
            else currentPlayerDeck = mm.bots[mm.playerIndexMove - 1].GetComponent<bot_manager>().PlayersDeck;

            foreach (var card in _moveDeck)
            {
                currentPlayerDeck.Add(card);
                if (mm.playerIndexMove != 0)
                {
                    card.transform.parent = _mainDeck;
                    card.transform.position = _mainDeck.position;
                    mm.bots[mm.playerIndexMove - 1].GetComponent<bot_manager>().IncreaseCardsCount();
                }
            }
            if (!isPreviousPlayerBot)
            {
                _displayDeck.ResetAddedCardsCount();
                _displayDeck.FitCards();
            }

            _moveDeck.Clear();
            mm.endMove(false, true);
        }

        whenDone?.Invoke();
    }

    private void ShowLastCards(GameObject[] cards)
    {
        float gap = cards[0].GetComponent<BoxCollider2D>().bounds.extents.x * 2;

        for (int i = 0; i < cards.Length; i++)
        {
            GameObject card = cards[i];
            if (card != null)
            {
                card.transform.eulerAngles = new Vector3(0, 0, 0);
                card.GetComponent<CardManager>().flipCardToFace();
                card.transform.position =  new Vector3(i * gap, 0, 0);
            }
        }
    }

    private void HideLastCards(GameObject[] cards)
    {
        foreach (var card in cards)
        {
            card.GetComponent<CardManager>().flipCardToDown();
        }
    }
}
