using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardManager : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
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
    //nhung card cua enemy
    Card[] enemySlotCard = new Card[5];

    public Transform[] cardSlots;
    //vi tri cua card enemy
    public Transform[] enemyCardSlots;
    public bool[] availableCardSlots;
    public int cardNum;

    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardPileSizeText;

    public PanelOpener cardPanel;

    bool activeDrawButton;
    bool drawedCard;
    Controller controller;
    [SerializeField] GameObject cardFunctionObject;
    NetworkStarter cardFunction;
    private void Start()
    {
        cardFunction = cardFunctionObject.GetComponent<NetworkStarter>();
        activeDrawButton = true;
        drawedCard = false;
        allCard = deck;
        for (int i = 0; i<13; i++)
        {
            allCardStill[i] = true;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        while (!controller)
            controller = FindObjectOfType<Controller>();
    }
    void Update()
    {
        deckSizeText.text = deck.Count.ToString();
        discardPileSizeText.text = discardPile.Count.ToString();
    }
    public void DrawCard()
    {
        if (!controller.isEnemyTurn()&&  !drawedCard)
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
                            drawedCard=true;
                            notInDeck.Add(randCard);
                            tempCard = randCard;
                            deck.Remove(randCard);
                            photonView.RPC("RPC_enemyDrawCard", RpcTarget.Others, tempCard);
                            return;
                        }
                    }
                }
            }
        }

    }
    [PunRPC]
    void RPC_enemyDrawCard(Card card)
    {
        deck.Remove(card);
        enemyCard.Add(card.id);
        enemySlotCard[enemySlotCard.Length] = card;
        notInDeck.Add(card);
        allCardStill[card.id] = false;
        drawedCard = false;
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
            photonView.RPC("RPC_enemyShufflle", RpcTarget.Others);
        }
    }
    void RPC_enemyShufflle()
    {
        Shufflle();
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
    /// <summary>
    /// authored by Booster
    /// </summary>
    /// <returns></returns>
    
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
        if (enemyCard.Count >= 1)
        {
            int randNum = Random.Range(0, enemyCard.Count);
            foreach (Card card in notInDeck)
            {
                if (card.name.Equals(enemyCard[randNum].ToString()))
                {
                    card.gameObject.SetActive(false);
                }
            }

        }
        drawedCard = true;
        return true;
    }
    public bool card003(int idCard)
    {
        for (int i = 0; i < availableCardSlots.Length; i++)
        {
            if (availableCardSlots[i] == true)
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
            }
        }
        return false;
    }
    /// <summary>
    /// function for card
    /// authored by Booster
    /// </summary>
    /// <returns></returns>
    public bool card101()
    {
        return controller.card101();
    }
    public bool card102()
    {
        return controller.card102();
    }
    public bool card103()
    {
        photonView.RPC("RPC_card103()", RpcTarget.Others);
        return controller.card103();
    }
    [PunRPC]
    void RPC_card103()
    {
        controller.card103_receive();
    }
    public bool card104()
    {
        photonView.RPC("RPC_card104()", RpcTarget.Others);
        return controller.card104();
    }
    void RPC_card104()
    {
        controller.card104_receive();
    }
    public bool card201()
    {
        drawedCard = false;
        return controller.card201();
    }
    public bool card202()
    {
        photonView.RPC("RPC_card202_send()", RpcTarget.Others);
        return true;
    }
    void RPC_card202_send()
    {
        photonView.RPC("RPC_card202_receive()", RpcTarget.Others, controller.card202_receive()); 
    }
    void RPC_card202_receive(int id)
    {
         controller.card202(id);
    }
    public bool card203()
    {
        return controller.card203(); ;
    }
}