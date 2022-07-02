using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CardTypeChange : MonoBehaviour
{
    private match_manager mm;

    void Awake()
    {
        mm = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<match_manager>();
    }
    void Start()
    {
        onCardTypeButtonPressed.CardTypeButtonPressed += ChangeCardType;
    }

    internal void ChangeCardType(bool _ = false, bool __ = false)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = mm.currentCardType.ToString();
    }

    internal void ResetCardType()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "";
    }

    internal void waitForPlayersInput()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Waiting for player...";
    }
}
