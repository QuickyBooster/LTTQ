using Photon.Pun;
using UnityEngine;

public class PointFunction : MonoBehaviour
{
    [SerializeField] PhotonView view;
    [SerializeField] GameObject cardManagerObject;

    CardManager cardManager;
    Controller controller;
    int tempID;
    private void Awake()
    {
        cardManager = cardManagerObject.GetComponent<CardManager>();
        controller = FindObjectOfType<Controller>();
        tempID = -1;
    }


    public bool managerAllPoint(int id)
    {
        tempID = id;
        attackPoint(id);
        return true;
    }
    [PunRPC]
    void receiveTrueFromEnemy()
    {
        controller.isEnemyDown(tempID, true);
    }
    [PunRPC]
    void receiveFalseFromEnemy()
    {
        controller.isEnemyDown(tempID, false);
    }
    //testing parameter
    [PunRPC]
    void receiveFromEnemy(bool status)
    {
        controller.isEnemyDown(tempID, status);
    }
    //end
    void feedbackToEnemy(int id)
    {
        view.RPC("receiveFromEnemy", RpcTarget.Others, controller.returnPointHit(id));
    }
    public void attackPoint(int id)
    {
        view.RPC("RPC_attackPoint", RpcTarget.Others, id);
    }
    [PunRPC]
    void RPC_attackPoint(int id)
    {
        feedbackToEnemy(id);
    }
}