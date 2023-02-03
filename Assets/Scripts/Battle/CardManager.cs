using Photon.Pun;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class CardManager : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    // danh sach the
    List<Card> allCard;
    // id nhung card ma player dang giu
    List<int> myCardID = new List<int>();
    //id nhung card ma enemy dang giu
    List<int> enemyCardID = new List<int>();
    // nhung card khong co trong deck + discard pile
    List<Card> cardInPlayerHand = new List<Card>();
    // nhung card tren hang doi, co the draw
    public List<Card> deck = new List<Card>();
    // nhung card da bi su dung, khi deck =0 thi co the shuffle
    public List<Card> discardPile = new List<Card>();
    // luu lai card vua moi rut
    public Card tempCard;
    Card[] mySlotCard = new Card[5];
    //nhung card cua enemy
    Card[] enemySlotCard = new Card[5];

    //vi tri cua card 
    public Transform[] myCardPositions;
    public Transform[] enemyCardPositions;
    public bool[] myAvailableCardSlots;
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
        controller = FindObjectOfType<Controller>().GetComponent<Controller>();
        cardFunction = cardFunctionObject.GetComponent<NetworkStarter>();
        activeDrawButton = true;
        drawedCard = false;
        allCard = new List<Card>(deck);
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
                    for (int i = 0; i < myAvailableCardSlots.Length; i++)
                    {
                        if (myAvailableCardSlots[i] == true)
                        {
                            print(75);
                            if (cardPanel.isActive() == false)
                            {
                                cardPanel.toggleActive();
                            }
                            randCard.gameObject.SetActive(true);
                            randCard.picked();
                            randCard.transform.position = cardPanel.cardTransform.position;
                            myCardID.Add(randCard.id-1);
                            cardPanel.setCardInPanel(true);
                            drawedCard=true;
                            cardInPlayerHand.Add(randCard);
                            tempCard  = randCard;
                            photonView.RPC("RPC_enemyDrawCard", RpcTarget.Others, (randCard.id-1));
                            deck.Remove(randCard);
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
        allCard[id].gameObject.SetActive(true);
        enemyCardID.Add(id);
        int i = 0;
        foreach (bool a in enemyAvailableCardSlots)
        {
            if (!a) i++;
            else break;
        }
        enemyAvailableCardSlots[i] = false;
        enemySlotCard[i] = allCard[id];
        cardInPlayerHand.Add(allCard[id]);
        allCard[id].GetComponent<Card>().hideFrontSide();
        allCard[id].transform.position = enemyCardPositions[i].position;
        drawedCard = false;
    }

    public void Shuffle()
    {
        if (deck.Count == 0)
        {
            foreach (Card card in discardPile)
                deck.Add(card);
            discardPile.Clear();
            activeDrawButton = true;
            photonView.RPC("RPC_enemyShuffle", RpcTarget.Others);
        }
    }
    void Shuffle_recieve()
    {
            foreach (Card card in discardPile)
            {
                deck.Add(card);
            }
            discardPile.Clear();
            activeDrawButton = true;
    }
    [PunRPC]
    void RPC_enemyShuffle()
    {
        Shuffle_recieve();
    }

    public void GetCard()
    {
        cardPanel.SetHidePannel(true);
        cardPanel.SetShowPannel(false);
        int i = 0;
        foreach (bool a in myAvailableCardSlots)
        {
            if (!a) i++;
            else break;
        }
        if (cardPanel.isCardInPanelNow())
        {
            tempCard.handIndex =i;
            cardPanel.setCardInPanel(false);
            tempCard.transform.position = myCardPositions[i].position;
            myAvailableCardSlots[i] = false;
            mySlotCard[i] = tempCard;
        }
    }
    public void UseCard(int handIdex)
    {
        photonView.RPC("RPC_enemyUseCard", RpcTarget.Others, handIdex);
        discardPile.Add(mySlotCard[handIdex]);
        mySlotCard[handIdex].useCard();
        myAvailableCardSlots[handIdex]= true;
        cardInPlayerHand.Remove(mySlotCard[handIdex]);
        myCardID.Remove(mySlotCard[handIdex].id);
        mySlotCard[handIdex] = null;
    }
    [PunRPC]
    void RPC_enemyUseCard(int index)
    {
        //false
        print("168"+index);
        discardPile.Add(enemySlotCard[index]);
        enemySlotCard[index].useCard();
        enemyAvailableCardSlots[index] = true;
        cardInPlayerHand.Remove(enemySlotCard[index]);
        enemyCardID.Remove(enemySlotCard[index].id);
        enemySlotCard[index]= null;
    }
    public void toggleActiveDrawButton()
    {
        activeDrawButton = !activeDrawButton;
    }
    /// <summary>
    /// card002
    /// </summary>
    public void card002()
    {
        controller.card002_active();
    }
    public void card002_send(int id)
    {
        photonView.RPC("RPC_card002_receive", RpcTarget.Others, id);
    }
    [PunRPC]
    void RPC_card002_receive(int id)
    {
        controller.card002_receive(id);
    }

    /// <summary>
    /// card004
    /// </summary>
    /// <returns></returns>
    public bool card004()
    {
        return controller.card004();
    }
    public bool card005()
    {
        return controller.card005();
    }
    public bool card006()
    {
        photonView.RPC("RPC_card006", RpcTarget.Others);
        return controller.card006();
    }
    [PunRPC]
    void RPC_card006()
    {
        controller.card006_receive();
    }
    public bool card008()
    {
        drawedCard = false;
        return controller.card008();
    }
    public bool card009()
    {
        photonView.RPC("RPC_card009_send", RpcTarget.Others);
        return true;
    }
    [PunRPC]
    void RPC_card009_send()
    {
        photonView.RPC("RPC_card009_receive", RpcTarget.Others, controller.card009_receive());
    }
    [PunRPC]
    void RPC_card009_receive(int id)
    {
        controller.card009(id);
    }
    public bool card001()
    {
        return controller.card001(); ;
    }
}