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
    [SerializeField] GameObject bubbleSpeechObject;
    [SerializeField] Text updateText;
    [SerializeField] GameObject enemyBubbleSpeechObject;
    [SerializeField] Text updateTextEnemy;

    InputField chatInputField;


    private void Awake()
    {

        chatInputField = GameObject.Find("ChatInput").GetComponent<InputField>();
    }
    private void Start()
    {
            bubbleSpeechObject.SetActive(false);

    }
    private void Update()
    {     
            if (chatInputField.text != "")
            {
                if (/*chatInputField.isFocused &&*/ Input.GetKeyDown(KeyCode.Return))
                {
                    photonView.RPC("sendMessage", RpcTarget.Others, chatInputField.text);
                    updateText.text = chatInputField.text;
                    bubbleSpeechObject.SetActive(true);
                   
                    chatInputField.text = "";
                    StartCoroutine("remove");
                }
            }
    }
    [PunRPC]
    void sendMessage(string txt)
    {
        enemyBubbleSpeechObject.SetActive(true);
        updateTextEnemy.text = txt;
        StartCoroutine("removeEnemyChat");
    }
    IEnumerator removeEnemyChat()
    {
        yield return new WaitForSeconds(3.5f);
        enemyBubbleSpeechObject.SetActive(false);
    }
    IEnumerator remove()
    {
        yield return new WaitForSeconds(3.5f);
        bubbleSpeechObject.SetActive(false);

    }
}
