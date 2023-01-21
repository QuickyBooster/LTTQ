using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTransition : MonoBehaviour
{
    public Animator ship1;
    public void Awake()
    {
        ship1 = GameObject.Find("Ship image").GetComponent<Animator>();
    }
    public void battleStart()
    {
        StartCoroutine(startBattle());
    }
    IEnumerator startBattle()
    {
        ship1.SetTrigger("startBattle");
        yield return new WaitForSeconds(.5f);
    }
}
