using Photon.Pun;
using UnityEngine;

public class CardFunction : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    [SerializeField] GameObject cardManagerObject;
    [SerializeField] UIManagerBattle UIManager;

    CardManager cardManager;
    Controller controller;

    private void Awake()
    {
        cardManager = cardManagerObject.GetComponent<CardManager>();
        controller = FindObjectOfType<Controller>();
        UIManager = FindObjectOfType<UIManagerBattle>();
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

    public void endMatch()
    {
        photonView.RPC("RPC_endMatch", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_endMatch()
    {
        UIManager.showResult(true);
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



}
