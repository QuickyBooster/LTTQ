using Photon.Pun;
using UnityEngine;

public class CardFunction : MonoBehaviour/*, Photon.Pun.IPunObservable*/
{
    [SerializeField] PhotonView photonView;
    [SerializeField] GameObject cardManagerObject;

    CardManager cardManager;
    Controller controller;


    int tempIntNext, tempIntPrev;
    private void Awake()
    {
        cardManager = cardManagerObject.GetComponent<CardManager>();
        controller = FindObjectOfType<Controller>();
        if (PhotonNetwork.IsMasterClient)
        {
            bool rand = (1==Random.Range(0, 1));
            if (rand)
            {
                print("true roi ha");
                controller.toggleEnemyTurn();
            }
            else
            {
                print("false roi nha");
                setFirstTurn();
            }
        }

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
        }

        tempIntNext = tempIntPrev = id;
        setNextTurn();
    }

    void sendAttack()
    {

    }

    //void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{

    //    if (stream.IsWriting)
    //    {
    //        print("sendign");
    //        if (controller.isThisANewAttack()&& !needInfoDefend)
    //        {
    //            print("1");
    //            stream.SendNext(controller.sendAttack());
    //            needInfoDefend = true;
    //        }
    //        if (!needInfoAttack)
    //        {
    //            print("2");
    //            stream.SendNext(tempBool);
    //            needInfoAttack= true;
    //        }
    //    }
    //    else
    //    {
    //        print("received");
    //        if (needInfoAttack)
    //        {
    //            print("3");
    //            receiveAttack((int)stream.ReceiveNext());
    //            needInfoAttack= false;
    //        }
    //        if (needInfoDefend)
    //        {
    //            print("4");
    //            controller.isEnemyDown(tempIntNext, (bool)stream.ReceiveNext());
    //        }
    //    }
    //}
}
