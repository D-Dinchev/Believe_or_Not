using UnityEngine;
using UnityEngine.UI;

public class cardTypePanelManager : MonoBehaviour
{
    public CanvasGroup panelCanvasGroup;

    void Start()
    {
        panelCanvasGroup.interactable = false; // while player doesn't throw at least 1 card
        onCardClick.onCardAdded += switchPanelInteractToTrue;
        onCardTypeButtonPressed.CardTypeButtonPressed += switchPanelInteractToFalse;
    }


    void switchPanelInteractToTrue()
    {
        panelCanvasGroup.interactable = true;
    }

    void switchPanelInteractToFalse()
    {
        panelCanvasGroup.interactable = false;
    }
}
