using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text _Arrange;
    [SerializeField] Text _textError;

    public GameObject buttonBattle;

    Controller _controller;
    private void Start()
    {
        _controller = FindObjectOfType<Controller>();
    }

    public void showButtonBattle(bool status)
    {
        buttonBattle.SetActive(status);
    }
    public void setArrangeText(string text)
    {
        if (_Arrange)
        {
            _Arrange.text = text;
        }
    }
    public void setErrortext(string text)
    {
        if (_textError)
        {
            _textError.text = text;
        }
    }
    public void buttonBattleHit()
    {
        buttonBattle.SetActive(false);
    }
}
