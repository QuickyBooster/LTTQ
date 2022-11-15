using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
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
        roomName.text="Room Name:"  +PhotonNetwork.CurrentRoom.Name;
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
        
        base.OnRoomListUpdate(roomList);    
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
}
