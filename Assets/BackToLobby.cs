using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class BackToLobby : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene(1);
    }
    public void DisconnectedFromServer()
    {
        StartCoroutine(Disconnect());
    }

    IEnumerator Disconnect()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            yield return null;
        }
    }
}
