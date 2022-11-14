using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEditor.Rendering.LookDev;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public int cardNum;
    
    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardPileSizeText;

    public PanelOpener cardPanel;
    public void DrawCard()
    {
        Debug.Log("drawcard");
        if (cardPanel.isActive() == false)
        {
            cardPanel.toggleActive();
        }
        if (deck.Count >= 1)
        {
            cardNum = Random.Range(0, deck.Count);
            Card randCard = deck[cardNum];
            for (int i = 0; i < availableCardSlots.Length; i++)
            {
                if (availableCardSlots[i] == true)
                {
                    randCard.gameObject.SetActive(true);
                    randCard.handIndex = i;

                    randCard.transform.position = cardPanel.cardTransform.position;
                    cardPanel.SetCardStatus(true);

                    //deck.Remove(randCard);
                    return;
                }
            }
        }
    }

    public void Shufflle()
    {
        if(deck.Count == 0)
        {
            foreach (Card card in discardPile)
                deck.Add(card);
            discardPile.Clear();
        }
    }
    void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardPileSizeText.text = discardPile.Count.ToString();
    }

    public void GetCard()
    {
        cardPanel.SetHidePannel(true);
        cardPanel.SetShowPannel(false);

        if(cardPanel.GetCardStatus())
        {
            cardPanel.SetCardStatus(false);
            Card card = deck[cardNum];
            card.transform.position = cardSlots[card.handIndex].position;
            availableCardSlots[card.handIndex] = false;
            deck.Remove(card);
        }
    }

    public void UseCard()
    {
        cardPanel.SetHidePannel(true);
        cardPanel.SetShowPannel(false);

        if (cardPanel.GetCardStatus())
        {
            cardPanel.SetCardStatus(false);
            Card card = deck[cardNum];
            card.Invoke("MoveToDiscardPile", 0.1f);
            deck.Remove(card);
        }
    }
}
