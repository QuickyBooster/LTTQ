using UnityEngine;
using UnityEngine.UI;

public class UIManagerBattle : MonoBehaviour
{
    [SerializeField] Text _textTurn;
     Controller _controller;
    // Start is called before the first frame update
    void Start()
    {
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
        }else
        {
            this.setTextTurn("Your turn: ");
        }
    }
    public void setTextTurn(string text)
    {
        if (_textTurn)
        {
            _textTurn.text = text;
        }
    }
}
