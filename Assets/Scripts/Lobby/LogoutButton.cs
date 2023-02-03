using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogoutButton : MonoBehaviour
{
    PlayerManager playerManager;

    private void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
    }
    public void Logout()
    {
        //StartCoroutine(loginSceneDelay());
        playerManager.LogoutButton();
    }
    public void loadLoginScene()
    {
        StartCoroutine(loginSceneDelay());
    }
    IEnumerator loginSceneDelay()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(0);
    }
}
