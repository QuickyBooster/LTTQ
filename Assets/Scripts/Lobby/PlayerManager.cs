using UnityEngine;
using Firebase;
using Firebase.Auth;

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
        Debug.Log("Setting up Firebase Auth");
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
