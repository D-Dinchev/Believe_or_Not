using UnityEngine;
using UnityEngine.UI;

public class MoreButton : MonoBehaviour
{
    public Button MButton;

    private match_manager mm;

    void Awake()
    {
        mm = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<match_manager>();
    }

    void Start()
    {
        MButton.interactable = false;
        onCardClick.onCardAdded += switchButtonInteractToTrue;
    }

    void switchButtonInteractToTrue()
    {
        if (mm.currentMoveType == MoveType.Ongoing)
            MButton.interactable = true;
    }

    public void switchPanelInteractToFalse()
    {
        MButton.interactable = false;
    }


}
