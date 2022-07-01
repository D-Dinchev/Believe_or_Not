using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bot_manager : MonoBehaviour
{
    public List<GameObject> PlayersDeck;
    internal bool isMyTurn;
    private GameObject Canvas;
    private GameObject botPanelPrefab;
    private GameObject botPanel;
    private moveDeckManager mdm;
    private match_manager mm;

    private Coroutine turnRunningCoroutine;
    private bool stopTurnCoroutine = false;

    internal int howManyThrowed;

    void Awake()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        botPanelPrefab = Resources.Load("Prefabs/Bot_Panel") as GameObject;
        mdm = GameObject.FindGameObjectWithTag("MoveDeck").GetComponent<moveDeckManager>();
        mm = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<match_manager>();
    }

    void Start()
    {
        botPanel = Instantiate(botPanelPrefab, transform.position, Quaternion.identity);
        botPanel.transform.SetParent(Canvas.transform, false);
        botPanel.transform.position = transform.position;

        botPanel.transform.Find("Bot_Name").gameObject.GetComponent<TextMeshProUGUI>().text = name;
        botPanel.transform.Find("Cards_Count").gameObject.GetComponent<TextMeshProUGUI>().text = PlayersDeck.Count.ToString();
    }

    void Update()
    {
        if (isMyTurn && turnRunningCoroutine == null)
        {
            botPanel.transform.Find("TurnIndicator").GetComponent<Image>().color = Color.green;
            turnRunningCoroutine = StartCoroutine(startMove());
        }
        else if (stopTurnCoroutine)
        {
            StopCoroutine(startMove());
            turnRunningCoroutine = null;
            botPanel.transform.Find("TurnIndicator").GetComponent<Image>().color = Color.red;
            stopTurnCoroutine = false;
        }
    }
    IEnumerator startMove()
    {
        yield return new WaitForSeconds(Random.Range(3, 5));

        howManyThrowed =  Random.Range(1, 4);
        if (howManyThrowed > PlayersDeck.Count) howManyThrowed = Random.Range(1, PlayersDeck.Count + 1);
        botPanel.transform.Find("Cards_Count").gameObject.GetComponent<TextMeshProUGUI>().text = (PlayersDeck.Count - howManyThrowed).ToString();

        for (int i = 0; i < howManyThrowed; i++)
        {
            GameObject card = PlayersDeck[Random.Range(0, PlayersDeck.Count - 1)];
            if (card != null)
            {
                mdm.moveDeck.Add(card);
                PlayersDeck.Remove(card);
                card.transform.position = transform.position;
                card.transform.parent = mdm.gameObject.transform;
                mdm.cardAdded();
                yield return new WaitForSeconds(0.1f);
                mm.IncreaseThrowedCardsAtCurrentMove();
            }
        }

        mm.endMove();
        stopTurnCoroutine = true;
    }

    void ongoingMove()
    {

    }

    internal void IncreaseCardsCount()
    {
        botPanel.transform.Find("Cards_Count").gameObject.GetComponent<TextMeshProUGUI>().text = PlayersDeck.Count.ToString();
    }
}
