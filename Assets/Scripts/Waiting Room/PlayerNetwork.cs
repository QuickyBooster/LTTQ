using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Collections;

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
            ready = true;
            PlayersInGame++;
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
        if (PlayersInGame == 2)
        {
            while (timeBeforeLoad > 0)  
            {
                timeBeforeLoad -= Time.deltaTime;
            }
            StartCoroutine(waitBeforeBattle());
        }
    }
    IEnumerator waitBeforeBattle()
    {
        yield return new WaitForSeconds(2);
        PhotonNetwork.LoadLevel("Battle");
    }
}
