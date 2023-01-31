using Firebase.Auth;
using UnityEngine;
using Firebase;
using Firebase.Firestore;

public class PlayerManager : MonoBehaviour
{
    [Header("Firebase")]
    public FirebaseUser user;
    public FirebaseAuth auth;
    public string userUID { get;  set; }
    public string userName { get; set; }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LogoutButton()
    {
        if (auth != null && user != null)
        {
            auth.SignOut();
        }
    }
}
