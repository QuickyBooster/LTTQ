using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public Player player;
    [SerializeField] PhotonView photonView;
    GameObject bubbleSpeechObject;
    [SerializeField] Text updateText;
    [SerializeField] GameObject bubbleSpeechObjectEnemy;
    [SerializeField] Text updateTextEnemy;

    InputField chatInputField;
    private bool disableSend;

    private void Awake()
    {
        disableSend = false;
        chatInputField = GameObject.Find("ChatInput").GetComponent<InputField>();
    }
    private void Start()
    {
        bubbleSpeechObject = GameObject.Find("bubbleMe");
        bubbleSpeechObject.SetActive(false);
    }
    private void Update()
    {

        if (photonView.IsMine)
        {
            if (!disableSend && chatInputField.isFocused)
            {
                print("into 2");
                if (chatInputField.text  != "" && chatInputField.text.Length > 0 && Input.GetKeyDown(KeyCode.Tab ))
                {
                        photonView.RPC("sendMessage", RpcTarget.Others, chatInputField.text);
                        updateText.text = chatInputField.text;
                        bubbleSpeechObject.SetActive(true);
                        chatInputField.text = "";
                        disableSend= true;
                        StartCoroutine("remove");
                }
            }
        }
    }
    [PunRPC]
    void sendMessage(string txt)
    {
        bubbleSpeechObjectEnemy.SetActive(true);
        updateTextEnemy.text = txt;
        StartCoroutine("removeEnemyChat");
    }
    IEnumerator removeEnemyChat()
    {
        yield return new WaitForSeconds(3.5f);
        bubbleSpeechObjectEnemy.SetActive(false);
    }
    IEnumerator remove()
    {
        yield return new WaitForSeconds(3.5f);
        bubbleSpeechObject.SetActive(false);
        disableSend= false;
    }
}
