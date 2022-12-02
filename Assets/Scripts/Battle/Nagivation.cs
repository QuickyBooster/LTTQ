using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nagivation : MonoBehaviour
{
    public void returnToWaitingRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("Waiting Room");
    }
}
