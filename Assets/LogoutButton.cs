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
        StartCoroutine(loginSceneDelay());
        playerManager.LogoutButton();
    }
    public void loadLoginScene()
    {
        SceneManager.LoadScene(0);
    }
    IEnumerator loginSceneDelay()
    {
        yield return new WaitForSeconds(2);
    }
}
