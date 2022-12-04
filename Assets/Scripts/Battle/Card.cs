using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] Sprite frontSide;
    [SerializeField] Sprite backSide;    

    public bool hasBeenPlayed;
    public int handIndex=-1;
    //bool isPicked;

    private CardManager cardManager;
    Controller controller;

    int id;
    private void Start()
    {
        int.TryParse(this.name,out id);
       // isPicked = false;   
        cardManager = FindObjectOfType<CardManager>().GetComponent<CardManager>();   
        GetComponent<SpriteRenderer>().sprite = backSide;
        controller = FindObjectOfType<Controller>();
        handIndex =-1;
    }
    private void Awake()
    {
        handIndex =-1;
    }
    public void picked()
    {
        //isPicked = true;
        GetComponent<SpriteRenderer>().sprite = frontSide;

    }

    private void OnMouseEnter()
    {
        if(hasBeenPlayed == false &&handIndex!=-1)
        {
            transform.position += Vector3.up;  
        }
    }

    private void OnMouseExit()
    {
        if (handIndex!=-1)
        {

        transform.position -= Vector3.up;
        }
    }

    private void OnMouseDown()
    {
        //if (hasBeenPlayed == false)
        //{
        //    hasBeenPlayed = true;
        //    cardManager.availableCardSlots[handIndex] = true;
        //    Invoke("MoveToDiscardPile", 0.1f);
        //}
        if (handIndex!=-1)
        {
            usingCard();

        }
    }
    public void usingCard()
    {
        if (cardManager)
        {
            int idCard;
            int.TryParse(this.name, out idCard);
            cardManager.UseCard(handIndex);
            handIndex =-1;
            switch (id)
            {
                case 1:
                    {
                        //controller.toggleUsingCard(id);
                        //cardManager.toggleActiveDrawButton();
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
    public void MoveToDiscardPile()
    {
        cardManager.discardPile.Add(this);
        this.gameObject.SetActive(false); 
    }

}
