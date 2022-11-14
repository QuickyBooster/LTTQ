using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] Sprite frontSide;
    [SerializeField] Sprite backSide;    

    public bool hasBeenPlayed;

    public int handIndex;

    private CardManager gm;

    private void Start()
    {
        gm = FindObjectOfType<CardManager>();
        GetComponent<SpriteRenderer>().sprite = backSide;
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
