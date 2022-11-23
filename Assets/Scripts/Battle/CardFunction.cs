using Photon.Pun;
using UnityEngine;

public class CardFunction : MonoBehaviour, Photon.Pun.IPunObservable
{
    [SerializeField] PhotonView photonView;
    [SerializeField] GameObject cardManagerObject;

    CardManager cardManager;
    Controller controller;


    bool tempBool;
    int tempInt;
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
        tempInt = id;
        tempBool = controller.returnPointHit(id);
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
                stream.SendNext(controller.sendAttack());
                stream.SendNext(tempBool);
            }
            if (stream.IsReading)
            {
                print("received");
                receiveAttack((int)stream.ReceiveNext());
                controller.isEnemyDown(tempInt,(bool)stream.ReceiveNext());
            }
        }
    }
}
