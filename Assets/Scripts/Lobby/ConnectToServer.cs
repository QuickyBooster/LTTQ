using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Firebase.Firestore;
using System.Collections;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    [SerializeField] Text buttonText;
    [SerializeField] Text Header;
    [SerializeField] TMP_Text exp;
    [SerializeField] TMP_Text lv;
    [SerializeField] TMP_Text silver;

    //Firebase
    PlayerManager player;
    FirebaseFirestore db;
    ListenerRegistration listener;
    private void Start()
    {
        db = FirebaseFirestore.DefaultInstance;
        StartCoroutine(getData());
    }
    IEnumerator getData()
    {
        yield return new WaitForSeconds(0.01f);
        player = FindObjectOfType<PlayerManager>().GetComponent<PlayerManager>();
        listener = db.Collection("account").Document(player.userUID).Listen(snapshot =>
        {
            PlayerData data = snapshot.ConvertTo<PlayerData>();
            lv.text = "LV: "+data.exp/100;
            exp.text = "exp: "+data.exp%100;
            silver.text = "silver: "+data.silver;
        });
        Header.text = "welcome back\n captain "+ player.userName;

    }
    public void OnClickConnect()
    {
            PhotonNetwork.NickName = player.userName;    
            buttonText.text = "Connecting...";
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Waiting Room");
    }


}
