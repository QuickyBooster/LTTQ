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
        switch (id)
        {
            case 0:
                {
                    attackPoint00();
                    break;
                }
            case 1:
                {
                    attackPoint01();
                    break;
                }
            case 2:
                {
                    attackPoint02();
                    break;
                }
            case 3:
                {
                    attackPoint03();
                    break;
                }
            case 4:
                {
                    attackPoint04();
                    break;
                }
            case 5:
                {
                    attackPoint05();
                    break;
                }
            case 6:
                {
                    attackPoint06();
                    break;
                }
            case 7:
                {
                    attackPoint07();
                    break;
                }
            case 8:
                {
                    attackPoint08();
                    break;
                }
            case 9:
                {
                    attackPoint09();
                    break;
                }
            case 10:
                {
                    attackPoint10();
                    break;
                }
            case 11:
                {
                    attackPoint11();
                    break;
                }
            case 12:
                {
                    attackPoint12();
                    break;
                }
            case 13:
                {
                    attackPoint13();
                    break;
                }
            case 14:
                {
                    attackPoint14();
                    break;
                }
            case 15:
                {
                    attackPoint15();
                    break;
                }
            case 16:
                {
                    attackPoint16();
                    break;
                }
            case 17:
                {
                    attackPoint17();
                    break;
                }
            case 18:
                {
                    attackPoint18();
                    break;
                }
            case 19:
                {
                    attackPoint19();
                    break;
                }
            case 20:
                {
                    attackPoint20();
                    break;
                }
            case 21:
                {
                    attackPoint21();
                    break;
                }
            case 22:
                {
                    attackPoint22();
                    break;
                }
            case 23:
                {
                    attackPoint23();
                    break;
                }
            case 24:
                {
                    attackPoint24();
                    break;
                }
            default:
                {
                    print("no case to attack || error at point function 151");
                    break;
                }

        }
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
    void feedbackToEnemy(int id)
    {
        if (controller.returnPointHit(id))
        {
            view.RPC("receiveTrueFromEnemy", RpcTarget.Others);
        }
        else
        {
            view.RPC("receiveFalseFromEnemy", RpcTarget.Others);
        }
    }
    public void attackPoint00()
    {
        view.RPC("RPC_attackPoint00", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint00()
    {
        feedbackToEnemy(0);


    }
    public void attackPoint01()
    {
        view.RPC("RPC_attackPoint01", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint01()
    {
        feedbackToEnemy(1);
    }
    public void attackPoint02()
    {
        view.RPC("RPC_attackPoint02", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint02()
    {
        feedbackToEnemy(2);
    }
    public void attackPoint03()
    {
        view.RPC("RPC_attackPoint03", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint03()
    {
        feedbackToEnemy(3);
    }
    public void attackPoint04()
    {
        view.RPC("RPC_attackPoint04", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint04()
    {
        feedbackToEnemy(4);
    }
    public void attackPoint05()
    {
        view.RPC("RPC_attackPoint05", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint05()
    {
        feedbackToEnemy(5);
    }
    public void attackPoint06()
    {
        view.RPC("RPC_attackPoint06", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint06()
    {
        feedbackToEnemy(6);
    }
    public void attackPoint07()
    {
        view.RPC("RPC_attackPoint07", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint07()
    {
        feedbackToEnemy(7);
    }
    public void attackPoint08()
    {
        view.RPC("RPC_attackPoint08", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint08()
    {
        feedbackToEnemy(8);
    }
    public void attackPoint09()
    {
        view.RPC("RPC_attackPoint09", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint09()
    {
        feedbackToEnemy(9);
    }
    public void attackPoint10()
    {
        view.RPC("RPC_attackPoint10", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint10()
    {
        feedbackToEnemy(10);
    }
    public void attackPoint11()
    {
        view.RPC("RPC_attackPoint11", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint11()
    {
        feedbackToEnemy(11);
    }
    public void attackPoint12()
    {
        view.RPC("RPC_attackPoint12", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint12()
    {
        feedbackToEnemy(12);
    }
    public void attackPoint13()
    {
        view.RPC("RPC_attackPoint13", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint13()
    {
        feedbackToEnemy(13);
    }
    public void attackPoint14()
    {
        view.RPC("RPC_attackPoint14", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint14()
    {
        feedbackToEnemy(14);
    }
    public void attackPoint15()
    {
        view.RPC("RPC_attackPoint15", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint15()
    {
        feedbackToEnemy(15);
    }
    public void attackPoint16()
    {
        view.RPC("RPC_attackPoint16", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint16()
    {
        feedbackToEnemy(16);
    }
    public void attackPoint17()
    {
        view.RPC("RPC_attackPoint17", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint17()
    {
        feedbackToEnemy(17);
    }
    public void attackPoint18()
    {
        view.RPC("RPC_attackPoint18", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint18()
    {
        feedbackToEnemy(18);
    }
    public void attackPoint19()
    {
        view.RPC("RPC_attackPoint19", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint19()
    {
        feedbackToEnemy(19);
    }
    public void attackPoint20()
    {
        view.RPC("RPC_attackPoint20", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint20()
    {
        feedbackToEnemy(20);
    }
    public void attackPoint21()
    {
        view.RPC("RPC_attackPoint21", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint21()
    {
        feedbackToEnemy(21);
    }
    public void attackPoint22()
    {
        view.RPC("RPC_attackPoint22", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint22()
    {
        feedbackToEnemy(22);
    }
    public void attackPoint23()
    {
        view.RPC("RPC_attackPoint23", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint23()
    {
        feedbackToEnemy(23);
    }
    public void attackPoint24()
    {
        view.RPC("RPC_attackPoint24", RpcTarget.Others);
    }
    [PunRPC]
    void RPC_attackPoint24()
    {
        feedbackToEnemy(24);
    }

}
