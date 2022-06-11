using System;
using UnityEngine;

public class onCardClick : MonoBehaviour
{
    public static event Action onCardAdded;
    private moveDeckManager mdm;
    private player_manager pm;
    private bool threwedMoreThanTwo = false;

    void Awake()
    {
        mdm = GameObject.FindGameObjectWithTag("MoveDeck").GetComponent<moveDeckManager>();
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<player_manager>();
    }

    void Start()
    {
        
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
            onCardAdded?.Invoke();
        }
    }
}
