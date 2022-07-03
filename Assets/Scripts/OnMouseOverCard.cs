using UnityEngine;

public class OnMouseOverCard : MonoBehaviour
{
    float howHigh = 1.5f;
    internal bool isAlreadyUp = false;

    void Update(){}
    void OnMouseOver()
    {
        if (!isAlreadyUp && enabled)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + howHigh);
            isAlreadyUp = true;
        }
    }

    void OnMouseExit()
    {
        if (enabled)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - howHigh);
            isAlreadyUp = false;
        }
    }
}
