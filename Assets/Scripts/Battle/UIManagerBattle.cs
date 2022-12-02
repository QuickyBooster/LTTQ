using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManagerBattle : MonoBehaviour
{
    [SerializeField] Text _textTurn;
    [SerializeField] TextMeshPro _textResult;
    [SerializeField] GameObject _resultPanel;
    [SerializeField] CanvasGroup _canvasGroup;

    Controller _controller;

    bool end;
    // Start is called before the first frame update
    void Start()
    {
        end = false;
        _canvasGroup.alpha =0;
        _resultPanel.SetActive(false);
        this.setTextTurn("who turn?");
        _controller = FindObjectOfType<Controller>();
        if (_controller.isEnemyTurn())
        {
            this.setTextTurn("Enemy turn: ");
        }
        else
        {
            this.setTextTurn("Your turn: ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_controller.isEnemyTurn())
        {
            this.setTextTurn("Enemy turn: ");
        }
        else
        {
            this.setTextTurn("Your turn: ");
        }
        if (end)
        {
            _canvasGroup.alpha += Time.deltaTime;
            if (_canvasGroup.alpha>=1) end = false;
        }
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
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("Waiting Room");
    }
}
