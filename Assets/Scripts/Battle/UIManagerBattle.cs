using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerBattle : MonoBehaviourPunCallbacks
{
    [SerializeField] Text _textTurn;
    [SerializeField] Text _textResult;
    [SerializeField] GameObject _resultPanel;
    [SerializeField] GameObject _buttonReadyToCountinue;

    Controller _controller;
    CardFunction _cardFunction;
    // Start is called before the first frame update
    void Start()
    {
        _resultPanel.SetActive(false);
        this.setTextTurn("who turn?");
        _controller = FindObjectOfType<Controller>();
        _cardFunction = FindObjectOfType<CardFunction>();
        if (_controller.isEnemyTurn())
        {
            this.setTextTurn("Enemy turn: ");
        }
        else
        {
            this.setTextTurn("Your turn: ");
        }
    }
    public void showButtonReadyToCountinue(bool state)
    {
        if (_buttonReadyToCountinue)
        {
            _buttonReadyToCountinue.SetActive(state);
        }
    }
    public void buttonReadyToContinueClick()
    {
        _controller.setBattleAgain();
        this.showButtonReadyToCountinue(false);
        _controller.toggleEnemyTurnWithText();
    }
    public void endMatch()
    {
        _cardFunction.endMatch();
        showResult(false);
    }
    public void setTextTurn(string text)
    {
        if (_textTurn)
        {
            _textTurn.text = text;
        }
    }
    public void showResult(bool status)
    {
        _resultPanel.SetActive(true);

        if (status)
        {
            _textResult.text = "Victory!!";

        }
        else
        {
            _textResult.text = "Defeat!!";
        }

    }

    public void OKbutton()
    {
        _controller.exitGame();
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.JoinLobby();

        base.OnLeftRoom();
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Waiting Room");
    }
}
