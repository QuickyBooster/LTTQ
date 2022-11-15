using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] Sprite frontSide;
    [SerializeField] Sprite backSide;    

    public bool hasBeenPlayed;
    public int handIndex;
    bool isPicked;

    private CardManager cardManager;
    Controller controller;

    int id;
    private void Start()
    {
        int.TryParse(this.name,out id);
        isPicked = false;   
        cardManager = FindObjectOfType<CardManager>().GetComponent<CardManager>();   
        GetComponent<SpriteRenderer>().sprite = backSide;
        controller = FindObjectOfType<Controller>();
    }
    public void picked()
    {
        isPicked = true;
        GetComponent<SpriteRenderer>().sprite = frontSide;  
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
        //if(hasBeenPlayed == false)
        //{
        //    hasBeenPlayed = true;
        //    gm.availableCardSlots[handIndex] = true;
        //    Invoke("MoveToDiscardPile", 0.1f);
        //}
        usingCard();
    }
    public void usingCard()
    {
        if (cardManager)
        {
            cardManager.UseCard();
            switch (id)
            {
                case 1:
                    {
                        controller.toggleUsingCard(id);
                        cardManager.toggleActiveDrawButton();
                        break;
                    }
                case 2:
                    {
                        //send to enemy cardManger: cardManager.card002();

                        break;
                    }
                case 3:
                    {

                        //send to enemy cardManger: cardManager.card003();
                        break;
                    }

            }
        }
        else
        {
            print("fail");
        }
    }
    void MoveToDiscardPile()
    {
        cardManager.discardPile.Add(this);
        gameObject.SetActive(false); 
    }

}
