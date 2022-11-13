using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.UI;

public class CardManager : MonoBehaviour
{
    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    
    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardPileSizeText;

    public PanelOpener cardPanel;
    public void DrawCard()
    {
        Debug.Log("drawcard");
        if (cardPanel.isActive == false)
        {
            cardPanel.isActive = true;
            cardPanel.panel.SetActive(cardPanel.isActive);
        }
        //if (deck.Count >= 1)
        //{
        //    Card randCard = deck[Random.Range(0, deck.Count)];
        //    for (int i = 0; i < availableCardSlots.Length; i++)
        //    {
        //        if (availableCardSlots[i] == true)
        //        {
        //            randCard.gameObject.SetActive(true);
        //            randCard.handIndex = i;

        //            randCard.transform.position = cardSlots[i].position;
        //            randCard.hasBeenPlayed = false;

        //            availableCardSlots[i] = false;
        //            deck.Remove(randCard);
        //            return;
        //        }
        //    }
        //}
    }

    public void Shufflle()
    {
        if(discardPile.Count == 10)
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
}
