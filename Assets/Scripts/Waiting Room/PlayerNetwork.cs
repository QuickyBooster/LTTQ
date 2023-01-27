using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;
public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    [SerializeField] PhotonView photonView;
    int PlayersInGame;
    bool ready;
    float timeBeforeLoad = 6f;
    //float timeElapsed = 0;

    private void Awake()
    {
        PlayersInGame = 0;
        Instance = this;
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            PlayerName = player.Value.ToString();
        }
        //SceneManager.sceneLoaded += OnSceneFinishedLoading;
        PhotonNetwork.AutomaticallySyncScene= true;
        ready = false;
    }
    public void readyToBattle()
    {
        if (!ready)
        {
            Debug.Log("ready");
            ready = true;
            loadedGame();
        }
    }

    void loadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.All);
    }


    [PunRPC]
    void RPC_LoadedGameScene()
    {
        PlayersInGame++;
        Debug.Log("load");
        if (PlayersInGame == 2)
        {
            while (timeBeforeLoad > 0)
            {
                Debug.Log("loop");
                timeBeforeLoad -= Time.deltaTime;
            }
            StartCoroutine(waitBeforeBattle());
            //PhotonNetwork.LoadLevel("Battle");
        }
    }
    IEnumerator waitBeforeBattle()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.LoadLevel("Battle");
        Debug.Log("aaaa");
    }
}