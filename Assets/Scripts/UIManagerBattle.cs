using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerBattle : MonoBehaviour
{
    [SerializeField] Text _textTurn;
    [SerializeField] Controller _controller;
    // Start is called before the first frame update
    void Start()
    {
        _controller = FindObjectOfType<Controller>();   
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
