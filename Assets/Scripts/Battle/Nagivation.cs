using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nagivation : MonoBehaviour
{
    public void returnToWaitingRoom()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Waiting Room");
    }
}
