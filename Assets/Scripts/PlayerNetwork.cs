using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class PlayerNetwork : MonoBehaviour
{
    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    [SerializeField] PhotonView photonView;
    int PlayersInGame;
    bool ready;

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
    private void Update()
    {
    }
    //void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.name =="name" )
    //    {
    //        if (PhotonNetwork.IsMasterClient)
    //        {
    //            MasterLoadedGame();
    //        }else
    //        {
    //            ClientLoadedGame();
    //        }
    //    }
    //}
    public void readyToBattle()
    {
        if (true&&!ready)
        {
            ready = true;
            if (PhotonNetwork.IsMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                ClientLoadedGame();
            }
        }
    }
    void MasterLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.All);
    }
    void ClientLoadedGame()
    {
        photonView.RPC("RPC_LoadedGameScene", RpcTarget.All);
    }


    [PunRPC]
    void RPC_LoadedGameScene()
    {
        PlayersInGame++;
        if (PlayersInGame ==2)
        {
            PhotonNetwork.LoadLevel("Battle");
        }
    }
}
