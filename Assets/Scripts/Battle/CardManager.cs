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
    List<Card> allCard;
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

    //vi tri cua card 
    public Transform[] myCardPositions;
    public Transform[] enemyCardPositions;
    public bool[] availableCardSlots;
    public bool[] enemyAvailableCardSlots;
    public int cardNum;

    public TextMeshProUGUI deckSizeText;
    public TextMeshProUGUI discardPileSizeText;

    public PanelOpener cardPanel;

    bool activeDrawButton { get; set; }
    public bool drawedCard { get; set; }
    Controller controller;
    [SerializeField] GameObject cardFunctionObject;
    NetworkStarter cardFunction;
    private void Start()
    {
        cardFunction = cardFunctionObject.GetComponent<NetworkStarter>();
        activeDrawButton = true;
        drawedCard = false;
        allCard = new List<Card>(deck);
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
                            photonView.RPC("RPC_enemyDrawCard", RpcTarget.Others, (tempCard.id-1));
                            return;
                        }
                    }
                }
            }
        }

    }
    [PunRPC]
    void RPC_enemyDrawCard(int id)
    {
        deck.Remove(allCard[id]);
        enemyCard.Add(id);
        int i = 0;
        foreach (bool a in enemyAvailableCardSlots)
        {
            if (!a) i++;
            else break;
        }
        enemySlotCard[i] = allCard[id];
        notInDeck.Add(allCard[id]);
        allCardStill[id] = false;
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
    [PunRPC]
    void RPC_enemyShufflle()
    {
        Shufflle();
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
            tempCard.transform.position = myCardPositions[i].position;
            availableCardSlots[i] = false;
            slotCard[i] = tempCard;
        }
    }
    public void UseCard(int handIdex)
    {
        if (handIdex==-1)
        {
            return;
        }else
        {
            print(handIdex);
            cardPanel.SetHidePannel(true);
            cardPanel.SetShowPannel(false);

            if (cardPanel.isCardInPanelNow())
            {

                deck.Remove(tempCard);
            }
            cardPanel.setCardInPanel(false);
            photonView.RPC("RPC_enemyUseCard", RpcTarget.Others,handIdex);
            discardPile.Add(slotCard[handIdex]);
            slotCard[handIdex].useCard();
            availableCardSlots[handIdex]= true;
        }
    }
    [PunRPC]
    void RPC_enemyUseCard(int index)
    {
        enemyAvailableCardSlots[index] = true;
        discardPile.Add(enemySlotCard[index]);
        notInDeck.Remove(enemySlotCard[index]);
        allCardStill[enemySlotCard[index].id] =true;
        enemyCard.Remove(enemySlotCard[index].id);
        //enemySlotCard[index].useCard();
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