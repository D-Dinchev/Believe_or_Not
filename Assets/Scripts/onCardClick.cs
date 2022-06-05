using System;
using UnityEngine;

public class onCardClick : MonoBehaviour
{
    public static event Action onCardAdded;
    private moveDeckManager mdm;

    void Awake()
    {
        mdm = GameObject.FindGameObjectWithTag("MoveDeck").GetComponent<moveDeckManager>();
    }

    void Start()
    {
        
    }

    void OnMouseDown()
    {
        if (enabled)
        {
            mdm.moveDeck.Add(gameObject);
            onCardAdded?.Invoke();
        }
    }
}
