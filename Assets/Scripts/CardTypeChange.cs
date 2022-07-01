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

    void ChangeCardType(bool _ = false, bool __ = false)
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = mm.currentCardType.ToString();
    }
}
