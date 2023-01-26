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
            ready = true;
            loadedGame();
            PlayersInGame++;
            if (PlayersInGame == 2)
            {
                StartCoroutine(waitBeforeBattle());
            }
        }
    }

    void loadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.Others);
    }


    [PunRPC]
    void RPC_LoadedGameScene()
    {
        PlayersInGame++;
        if (PlayersInGame == 2)
        {
            StartCoroutine(waitBeforeBattle());
        }
    }
    IEnumerator waitBeforeBattle()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.LoadLevel("Battle");
    }
}
