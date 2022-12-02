using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // kiem tra xem card da bi lay ra chua  true la chua, false la roi
    bool[] allCardStill = new bool[14];
    List<Card> allCard = new List<Card>();
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
    // luu lai card vua moi rut
    public Card tempCard;
    Card[] slotCard = new Card[5];

    public Transform[] cardSlots;
    public bool[] availableCardSlots;
    public int cardNum;

    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardPileSizeText;

    public PanelOpener cardPanel;

    bool activeDrawButton;
    bool drawedCard;
    Controller controller;
    [SerializeField] GameObject cardFunctionObject;
    CardFunction cardFunction;
    private void Start()
    {
        cardFunction = cardFunctionObject.GetComponent<CardFunction>();
        activeDrawButton = true;
        drawedCard = false;
        allCard = deck;
        for (int i = 0; i<13; i++)
        {
            allCardStill[i] = true;
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
        if (/*!controller.isEnemyTurn()&& */ !drawedCard)
        {
            if (activeDrawButton)
            {
                if (deck.Count >= 1)
                {
                    cardNum = Random.Range(0, deck.Count);
                    Card randCard = deck[cardNum];
                    for (int i = 0; i < availableCardSlots.Length; i++)
                    {
                        if (availableCardSlots[i] == true)
                        {
                            if (cardPanel.isActive() == false)
                            {
                                cardPanel.toggleActive();
                            }
                            int cardID;
                            int.TryParse(randCard.name, out cardID);
                            allCardStill[cardID] = false;
                            randCard.gameObject.SetActive(true);
                            //randCard.handIndex = i;
                            randCard.picked();
                            randCard.transform.position = cardPanel.cardTransform.position;
                            playerCard.Add(cardID);
                            cardPanel.setCardInPanel(true);
                            //drawedCard=true;
                            notInDeck.Add(randCard);
                            tempCard = randCard;
                            //deck.Remove(randCard);
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
                allCardStill[idCard] = false;
            }
            discardPile.Clear();
            activeDrawButton = true;
        }
    }
    public void enemyDrawCard(int idCard)
    {

        allCardStill[idCard] = false;
        foreach (Card card in deck)
        {
            if (card.name.Equals(idCard.ToString()))
            {
                notInDeck.Add(card);
                deck.Remove(card);
                enemyCard.Add(idCard);
                return;
            }
        }

    }
    public void enemyUsedCard(int idCard)
    {

        allCardStill[idCard] = true;
        foreach (Card card in notInDeck)
        {
            if (card.name.Equals(idCard.ToString()))
            {
                discardPile.Add(card);
                notInDeck.Remove(card);
                enemyCard.Remove(idCard);
                return;
            }
        }
    }
    public void GetCard()
    {
        cardPanel.SetHidePannel(true);
        cardPanel.SetShowPannel(false);
        int i = 0;
        foreach (bool a in availableCardSlots)
        {
            if (!a) i++;
            else break;
        }
        if (cardPanel.isCardInPanelNow())
        {
            tempCard.handIndex =i;
            cardPanel.setCardInPanel(false);
            tempCard.transform.position = cardSlots[tempCard.handIndex].position;
            availableCardSlots[tempCard.handIndex] = false;
            slotCard[tempCard.handIndex] = tempCard;
        }
        deck.Remove(tempCard);
    }

    public void buttonUseCard()
    {
        UseCard(-2);
    }
    public void UseCard(int handIdex)
    {
        if (handIdex==-1)
        {
            return;
        }
        if (handIdex == -2)
        {
            deck.Remove(tempCard);
            cardPanel.setCardInPanel(false);
            tempCard.MoveToDiscardPile();
            cardPanel.SetHidePannel(true);
            cardPanel.SetShowPannel(false);
            return;
        }
        else
        {
            print(handIdex);
            cardPanel.SetHidePannel(true);
            cardPanel.SetShowPannel(false);

            if (cardPanel.isCardInPanelNow())
            {

                deck.Remove(tempCard);
            }
            cardPanel.setCardInPanel(false);
            slotCard[handIdex].MoveToDiscardPile();
            availableCardSlots[handIdex]= true;
        }
    }
    public void toggleActiveDrawButton()
    {
        activeDrawButton = !activeDrawButton;
    }
    // benefits from card
    public bool usingCard003()
    {
        int randomCard = Random.Range(0, enemyCard.Count);
        foreach (Card card in notInDeck)
        {
            if (card.name.Equals(enemyCard[randomCard].ToString()))
            {
                playerCard.Add(randomCard);
                enemyCard.Remove(randomCard);
                // send enemy card003();
                return true;
            }
        }
        return false;
    }
    // enemy attack
    public bool card002()
    {
        drawedCard = true;
        return true;
    }
    public bool card003(int idCard)
    {
        foreach (Card card in notInDeck)
        {
            if (card.name.Equals(playerCard[idCard].ToString()))
            {
                playerCard.Remove(idCard);
                enemyCard.Add(idCard);
                return true;
            }
        }
        return false;
    }
}