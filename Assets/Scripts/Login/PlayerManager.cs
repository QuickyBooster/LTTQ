using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    [Header("Firebase")]
    public FirebaseUser user;
    public FirebaseAuth auth;
    public string userUID { get;  set; }
    public string userName { get; set; }
    int currentScene;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
     void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
     void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0)
        {
            if (currentScene!= 0)
            {
                Destroy(this.gameObject);
            }
        }else if (scene.buildIndex ==1)
        {
            currentScene++;
        }
    }

    public void LogoutButton()
    {
        if (auth != null && user != null)
        {
            auth.SignOut();
        }
    }
}
