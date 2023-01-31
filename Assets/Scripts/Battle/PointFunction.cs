using Photon.Pun;
using UnityEngine;

public class PointFunction : MonoBehaviour
{
    [SerializeField] PhotonView view;


    Controller controller;
    int tempID;
    private void Awake()
    {
        controller = FindObjectOfType<Controller>();
        tempID = -1;
    }

    //The following lines are about attack the enemy points
    public void attackPoint(int id)
    {
        tempID = id;
        view.RPC("RPC_attackPoint", RpcTarget.Others, id);
    }

    [PunRPC]
    void RPC_attackPoint(int id)
    {
        feedbackToEnemy(id);
    }

    // The following lines are about receive an attack from enemy and feedback about that attack

    void feedbackToEnemy(int id)
    {
        view.RPC("RPC_receiveFromEnemy", RpcTarget.Others, controller.returnPointHit(id));
    }

    [PunRPC]
    void RPC_receiveFromEnemy(bool status)
    {
        controller.isEnemyDown(tempID, status);
    }

    // The following lines are about when pause the battle
    // then continue after someone lose a ship
    public void pauseBattle()
    {
        view.RPC("RPC_pauseBattle", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_pauseBattle()
    {
        controller.deleteEnemyPoints();
        controller.textForWaitingEnemy();
    }

    public void resumeBattle()
    {
        view.RPC("RPC_resumeBattle", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_resumeBattle()
    {
        controller.createTableEnemy();

    }
    //special
    public void torpedoExplodeOnEnemy(int id, bool result)
    {
        view.RPC("RPC_torpedoExplodeOnEnemy", RpcTarget.Others, id, result);
    }
    [PunRPC]
    void RPC_torpedoExplodeOnEnemy(int id, bool result)
    {
        controller.isEnemyDown(id, result);
    }
}