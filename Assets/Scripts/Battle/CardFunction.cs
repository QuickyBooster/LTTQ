using Photon.Pun;
using UnityEngine;

public class CardFunction : MonoBehaviour, Photon.Pun.IPunObservable
{
    [SerializeField] PhotonView photonView;
    [SerializeField] GameObject cardManagerObject;

    CardManager cardManager;
    Controller controller;


    bool tempBool,needInfoAttack,needInfoDefend;
    int tempIntNext,tempIntPrev;
    private void Awake()
    {
        needInfoAttack =needInfoDefend= false;
        cardManager = cardManagerObject.GetComponent<CardManager>();
        controller = FindObjectOfType<Controller>();
        if (PhotonNetwork.IsMasterClient)
        {
            bool rand = (1==Random.Range(0, 1));
            if (rand)
            {
                print("true roi ha");
                controller.toggleEnemyTurn();
                setNeedInfoAttack();
            }
            else
            {
                print("false roi nha");
                setFirstTurn();
                needInfoAttack = true;
            }
        }

    }
    public void setNeedInfoAttack()
    {
        photonView.RPC("RPC_setFirstReceive", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_setFirstReceive()
    {
        needInfoAttack = true;
    }
    public void setFirstTurn()
    {
        photonView.RPC("RPC_setFirstTurn", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_setFirstTurn()
    {
        controller.toggleEnemyTurn();
    }
    public void setNextTurn()
    {
        photonView.RPC("RPC_setTurn", RpcTarget.All);
    }
    [PunRPC]
    void RPC_setTurn()
    {
        controller.toggleEnemyTurn();
    }


    void receiveAttack(int id)
    {
        if (id ==-1) return;
        print(id);
        if (tempIntNext == tempIntPrev)
        {
            needInfoAttack = true;
        }

        tempIntNext = tempIntPrev = id;
        tempBool = controller.returnPointHit(id);
        setNextTurn();
    }

    void sendAttack()
    {

    }

    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (true)
        {

            if (stream.IsWriting)
            {
                print("sendign");
                if (controller.isThisANewAttack())
                {
                    print("sending attack");
                    stream.SendNext(controller.sendAttack());
                    needInfoDefend = true;
                }
                ///
                if (!needInfoAttack)
                {
                    print("sengding info");
                    stream.SendNext(tempBool);
                    needInfoAttack= true;
                }
            }
            if (stream.IsReading)
            {
                print("received");
                if (needInfoAttack)
                {
                    print("received attack");
                    receiveAttack((int)stream.ReceiveNext());
                    needInfoAttack= false;
                }
                if (needInfoDefend)
                {
                    print("received defend");
                    controller.isEnemyDown(tempIntNext,(bool)stream.ReceiveNext());
                }
            }
        }
    }
}
