using Photon.Pun;
using UnityEngine;

public class CardFunction : MonoBehaviour
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
                controller.toggleEnemyTurn();
            }
            else
            {
                setFirstTurn();
            }
        }

    }
    public void setFirstTurn()
    {
        photonView.RPC("RPC_setFirstTurn", RpcTarget.Others);
    }
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
    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (!controller.isEnemyTurn())
        {

            if (stream.IsWriting)
            {
                stream.SendNext(controller.sendAttack());
            }
            if (stream.IsReading)
            {
                controller.displayAtack(tempInt);
            }
        }
        if (controller.isEnemyTurn())
        {
            if (stream.IsReading)
            {
                receiveAttack((int)stream.ReceiveNext());
            }
            if (stream.IsWriting)
            {
                stream.SendNext(tempBool);
            }

        }
    }
    void receiveAttack(int id)
    {
        tempInt = id;
        tempBool = controller.returnPointHit(id);
        setNextTurn();
    }
    //void displayPointHit(bool)
}
