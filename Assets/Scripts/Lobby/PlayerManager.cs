using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    FirebaseUser user;
    FirebaseAuth auth;

    public string userUID { get; private set; }
    public string userName { get; private set; }

    private void InitializeFirebase()
    {
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
        userName = user.DisplayName;
        userUID = user.UserId;
    }
    private void Start()
    {
        InitializeFirebase();
        DontDestroyOnLoad(this);
    }
    
}
