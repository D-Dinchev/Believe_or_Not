using System;
using UnityEngine;

public class onCardClick : MonoBehaviour
{
    public static event Action onCardAdded;
    private moveDeckManager mdm;
    private player_manager pm;
    internal static bool threwedMoreThanTwo = false;
    private match_manager mm;

    void Awake()
    {
        mdm = GameObject.FindGameObjectWithTag("MoveDeck").GetComponent<moveDeckManager>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<player_manager>();
        mm = GameObject.FindGameObjectWithTag("MatchManager").GetComponent<match_manager>();
    }


    void Update()
    {
        if (pm.howManyCardsThrowed > 2)
            threwedMoreThanTwo = true;
    }

    void OnMouseDown()
    {

        if (enabled && !threwedMoreThanTwo && pm.isMyTurn)
        {
            mdm.moveDeck.Add(gameObject);
            gameObject.transform.parent = mdm.transform;
            pm.PlayersDeck.Remove(gameObject);
            onCardAdded?.Invoke();
        }
    }
}
