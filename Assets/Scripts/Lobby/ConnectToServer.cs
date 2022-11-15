using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public InputField usernameInput;
    public Text buttonText;
    public void OnClickConnect()
    {
        if (usernameInput.text.Length>=1)
        {
            PhotonNetwork.NickName = usernameInput.text;    
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
            SceneManager.LoadScene("Waiting Room");
        }
    }
    private void OnConnectedToServer()
    {
        SceneManager.LoadScene("Waiting Room");
        print("ooa");
    }
    public void OnConnectToMaster()
    {
        SceneManager.LoadScene("Waiting Room");
        print("ooa");
    }

    

}
