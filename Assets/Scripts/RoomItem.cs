using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomItem : MonoBehaviour
{
    public Text roomName;
    RoomManager roomManager;

    private void Start()
    {
        roomManager = FindObjectOfType<RoomManager>();
    }
    public void onClickItem()
    {
        roomManager.joinRoom(roomName.text);
    }
    public void setRoomName(string name)
    {
        roomName.text = name;
    }
}
