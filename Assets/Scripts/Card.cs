using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool hasBeenPlayed;

    public int handIndex;

    private CardManager gm;

    private void Start()
    {
        gm = FindObjectOfType<CardManager>();
    }

    private void OnMouseEnter()
    {
        if(hasBeenPlayed == false)
        {
            transform.position += Vector3.up;  
        }
    }

    private void OnMouseExit()
    {
        transform.position -= Vector3.up;
    }

    private void OnMouseDown()
    {
        if(hasBeenPlayed == false)
        {
            hasBeenPlayed = true;
            gm.availableCardSlots[handIndex] = true;
            Invoke("MoveToDiscardPile", 0.1f);
        }
    }

    void MoveToDiscardPile()
    {
        gm.discardPile.Add(this);
        gameObject.SetActive(false); 
    }
}
