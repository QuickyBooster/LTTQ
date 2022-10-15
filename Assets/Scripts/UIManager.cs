using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text doneArrange;
    [SerializeField] Text textError;
    [SerializeField] GameObject buttonBattle;
    [SerializeField] Text textBattle;

    Controller controller;
    private void Start()
    {
        controller = FindObjectOfType<Controller>();    
    }

    public void showButtonBattle(bool status)
    {
        buttonBattle.SetActive(status);   
    }
    public void setArrangeText(string text)
    {
        if (doneArrange)
        {
            doneArrange.text = text;   
        }
    }
    public void setErrortext(string text)
    {
        if (textError)
        {
            textError.text = text;
        }
    }
    public void buttonBattleHit()
    {
            buttonBattle.SetActive(false);  
    }
    
}
