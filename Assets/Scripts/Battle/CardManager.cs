using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.UI;
using UnityEditor.Rendering.LookDev;
using UnityEditor;

public class CardManager : MonoBehaviour
{
    // kiem tra xem card da bi lay ra chua  true la chua, false la roi
     bool[] allCard = new bool[13];
    // id nhung card ma player dang giu
     List<int> playerCard = new List<int>();
    //id nhung card ma enemy dang giu
     List<int> enemyCard = new List<int>();
    // nhung card khong co trong deck + discard pile
    List<Card> notInDeck = new List<Card>();
    // nhung card tren hang doi, co the draw
    public List<Card> deck = new List<Card>();
    // nhung card da bi su dung, khi deck =0 thi co the shuffle
    public List<Card> discardPile = new List<Card>();

    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public int cardNum;

    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardPileSizeText;

    public PanelOpener cardPanel;

    bool activeDrawButton;
    bool drawedCard;
    Controller controller;
    private void Start()
    {
        activeDrawButton = true;
        drawedCard = false;
        for (int i = 0; i<13; i++)
        {
            allCard[i] = true;
        }
    }
    void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardPileSizeText.text = discardPile.Count.ToString();
        if (!controller)
        {
            controller = FindObjectOfType<Controller>();
        }
    }
    public void DrawCard()
    {
        if (!controller.isEnemyTurn() && !drawedCard)
        {
            if (activeDrawButton)
            {
                if (deck.Count >= 1)
                {
                    if (cardPanel.isActive() == false)
                    {
                        cardPanel.toggleActive();
                    }
                    cardNum = Random.Range(0, deck.Count);
                    Card randCard = deck[cardNum];
                    for (int i = 0; i < availableCardSlots.Length; i++)
                    {
                        if (availableCardSlots[i] == true)
                        {
                            int cardID;
                            int.TryParse(randCard.name, out cardID);
                            allCard[cardID] = false;
                            randCard.gameObject.SetActive(true);
                            randCard.handIndex = i;
                            randCard.picked();
                            randCard.transform.position = cardPanel.cardTransform.position;
                            cardPanel.SetCardStatus(true);
                            drawedCard=true;
                            notInDeck.Add(randCard);
                            deck.Remove(randCard);
                            return;
                        }
                    }
                }
            }
        }
        
    }

    public void Shufflle()
    {
        if (deck.Count == 0)
        {
            foreach (Card card in discardPile)
            {
                deck.Add(card);
                int idCard;
                int.TryParse(card.name, out idCard);
                allCard[idCard] = false;
            }
            discardPile.Clear();
            activeDrawButton = true;
        }
    }
    public void enemyDrawCard(int idCard)
    {
        if (drawedCard)
        {
            allCard[idCard] = false;
            foreach (Card card in deck)
            {
                if (card.name.Equals(idCard.ToString()))
                {
                    notInDeck.Add(card);
                    deck.Remove(card);
                    return;
                }
            }
        }
    }
    public void enemyUsedCard(int idCard)
    {
        if (drawedCard)
        {
            allCard[idCard] = true;
            foreach (Card card in notInDeck)
            {
                if (card.name.Equals(idCard.ToString()))
                {
                    discardPile.Add(card);
                    notInDeck.Remove(card);
                    return;
                }
            }
        }
    }
    public void GetCard()
    {
        cardPanel.SetHidePannel(true);
        cardPanel.SetShowPannel(false);

        if (cardPanel.GetCardStatus())
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
    public void toggleActiveDrawButton()
    {
        activeDrawButton = !activeDrawButton;
    }
    // enemy attack
    public bool card002()
    {
        drawedCard = true;
        return true;
    }
    public bool card003()
    {
        int randomCard = Random.Range(0, availableCardSlots.Length);
        Card pick = deck[randomCard];
        pick.gameObject.SetActive(false);
        pick.handIndex = -1;
        pick.picked();
        return true;
    }
}