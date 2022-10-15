using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonBattle : MonoBehaviour
{
    Controller controller;
    private void Start()
    {
        controller = FindObjectOfType<Controller>();
    }
    private void OnMouseDown()
    {
        controller.navigateBattle();
    }

}
