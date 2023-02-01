using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class BackToLobby : MonoBehaviour
{
    public void BackButton()
    {
        StartCoroutine(lobbyDelayTime());
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
    IEnumerator lobbyDelayTime()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(1);
        
    }
}
