using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bot_manager : MonoBehaviour
{
    public List<GameObject> PlayersDeck;
    internal bool isMyTurn;

    void Update()
    {
        if (isMyTurn)
        {
        }
    }
    void startMove()
    {
        // pick 1 - 3 cards
        // click the endMove button
        Debug.Log(this.name + " turn!");

    }

    void ongoingMove()
    {

    }
}
