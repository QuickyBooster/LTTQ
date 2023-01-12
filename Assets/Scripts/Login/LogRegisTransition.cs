using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRegisTransition : MonoBehaviour
{
    public Animator loginPanel;
    public Animator registerPanel;

    private void Awake()
    {
        loginPanel = GameObject.Find("Login Panel").GetComponent<Animator>();
        registerPanel = GameObject.Find("Register Panel").GetComponent<Animator>();
    }
    public void showRegister()
    {
        StartCoroutine(register_click());
    }
    public void showLogin()
    {
        StartCoroutine(login_click());
    }
    IEnumerator register_click()
    {
        loginPanel.SetTrigger("login1");
        registerPanel.SetTrigger("register1");
        yield return new WaitForSeconds(.5f);
    }
    IEnumerator login_click()
    {
        registerPanel.SetTrigger("register2");
        loginPanel.SetTrigger("login2");
        yield return new WaitForSeconds(.5f);
    }    
}
