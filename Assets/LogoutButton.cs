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
        playerManager.LogoutButton();
    }
    public void loadLoginScene()
    {
        SceneManager.LoadScene(0);
    }
}
