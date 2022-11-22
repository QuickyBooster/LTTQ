using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CardFunction : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    [SerializeField] GameObject cardManagerObject;
    [SerializeField] GameObject controllerObject;

    CardManager cardManager;
    Controller controller;


    bool tempBool;
    int tempInt;
    private void Awake()
    {
        cardManager = cardManagerObject.GetComponent<CardManager>();
        controller = controllerObject.GetComponent<Controller>();
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
    }
    //void displayPointHit(bool)
}
