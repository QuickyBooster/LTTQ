using Photon.Pun;
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
