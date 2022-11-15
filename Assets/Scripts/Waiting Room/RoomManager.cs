using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class RoomManager : MonoBehaviourPunCallbacks
{
    public InputField roomInputField;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;

    public RoomItem roomItem;
    List<RoomItem> roomList = new List<RoomItem> ();
    public Transform contentObject;
    public float timeUpdates = 1.5f;
    float nextUpdates;

    public List<PlayerItem> playerItemList = new List<PlayerItem>();
    public PlayerItem playerItemPrefabs;
    public Transform playerItemParent;

    private void Start()
    {
        PhotonNetwork.JoinLobby();
        roomPanel.SetActive(false);
    }
    public void OnClickCreate()
    {
        if (roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers=2 });
        }

    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text="Room:"  +PhotonNetwork.CurrentRoom.Name;
        updatePlayerList();
    }
    

    public override void OnLeftRoom()
    {
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();

    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time>=nextUpdates)
        {

            updateRoomList(roomList);
            nextUpdates =  Time.time+timeUpdates;
        }
    }
    void updateRoomList(List<RoomInfo> list)
    {
        foreach (RoomItem item in roomList)
        {
            Destroy(item.gameObject);
        }
        roomList.Clear();
        foreach (RoomInfo room in list)
        {
            RoomItem newRoom = Instantiate(roomItem, contentObject);
            newRoom.setRoomName(room.Name);
            roomList.Add(newRoom);
        }
    }
    public void onClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void updatePlayerList()
    {
       foreach (PlayerItem item in playerItemList)
        {
            Destroy (item.gameObject);

        }
        playerItemList.Clear();
        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {

            PlayerItem newPlayerItem = Instantiate(playerItemPrefabs, playerItemParent);
            newPlayerItem.setPlayerName(player.Value);
            playerItemList.Add(newPlayerItem);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        updatePlayerList();
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        updatePlayerList();
    }
    public void joinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
