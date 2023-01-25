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
    public int handIndex { get; set; }
    //bool isPicked;

    private CardManager cardManager;
    Controller controller;

    public int id;
    private void Start()
    {
        handIndex = -1;
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
    public void hideFrontSide()
    {
        GetComponent<SpriteRenderer>().sprite = backSide;
    }

    private void OnMouseEnter()
    {
        if(hasBeenPlayed == false &&handIndex!=-1)
        {
           // transform.position += Vector3.up;  
        }
    }

    private void OnMouseExit()
    {
        if (handIndex!=-1)
        {

        //transform.position -= Vector3.up;
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
            controller.toggleUsingCard(id);
                        
            switch (id)
            {
                case 1:
                    {
                        cardManager.card001();
                        break;
                    }
                case 2:
                    {
                        cardManager.card002();
                        break;
                    }
                case 3:
                    {
                        break;
                    }
                case 4:
                    {
                        cardManager.card004();
                        break;
                    }
                case 5:
                    {
                        cardManager.card005();
                        break;
                    }
                case 6:
                    {
                        cardManager.card006();
                        break;
                    }
                case 8:
                    {
                        cardManager.card008();
                        break;
                    }
                case 9:
                    {
                        cardManager.card009();
                        break;
                    }
            }
        }
        else
        {
            print("fail");
        }
    }
    public void useCard()
    {
        this.gameObject.SetActive(false); 
    }

}
