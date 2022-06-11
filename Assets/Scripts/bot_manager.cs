using UnityEngine.UI;
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

    void Awake()
    {
        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        botPanelPrefab = Resources.Load("Prefabs/Bot_Panel") as GameObject;
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
}
