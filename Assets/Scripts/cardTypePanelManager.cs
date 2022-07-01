using UnityEngine;
using UnityEngine.UI;

public class cardTypePanelManager : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup;

    private match_manager mm;

    void Awake()
    {
        mm = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<match_manager>();
    }

    void Start()
    {
        panelCanvasGroup.interactable = false; // while player doesn't throw at least 1 card
        onCardClick.onCardAdded += switchPanelInteractToTrue;
        onCardTypeButtonPressed.CardTypeButtonPressed += switchPanelInteractToFalse;
    }

    void Update()
    {

    }


    void switchPanelInteractToTrue()
    {
        if (mm.currentMoveType == MoveType.Start && mm.mainPlayer.isMyTurn)
            panelCanvasGroup.interactable = true;
    }

    void switchPanelInteractToFalse(bool _ = false, bool __ = false)
    {
        panelCanvasGroup.interactable = false;
    }
}
