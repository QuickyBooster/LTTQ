using System.Collections;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;
using Firebase.Firestore;

public class AuthManager : MonoBehaviour
{

    OpenScene openScene;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] GameObject xxx;

    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    FirebaseUser user;
    FirebaseAuth auth;
    FirebaseFirestore db;

    //Login variables
    [Header("Login")]
    public TMP_InputField emailLoginField; //0
    public TMP_InputField passwordLoginField; //1
    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;
    public int InputSelected;

    //Register variables
    [Header("Register")]
    public TMP_InputField emailRegisterField; //0
    public TMP_InputField passwordRegisterField; //1
    public TMP_InputField passwordRegisterVerifyField; //2
    public TMP_InputField usernameRegisterField; //3
    public TMP_Text warningRegisterText;

    private void Awake()
    {
        openScene = FindObjectOfType<OpenScene>();
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
    private void Update()
    {
        void SelectInputField()
        {
            if (loginUI.activeSelf == true)
            {
                switch (InputSelected)
                {
                    case 0: emailLoginField.Select();
                        break;
                    case 1: passwordLoginField.Select();
                        break;
                }
            }
            else if (registerUI.activeSelf == true)
            {
                switch (InputSelected)
                {
                    case 0:
                        emailRegisterField.Select();
                        break;
                    case 1:
                        passwordRegisterField.Select();
                        break;
                    case 2:
                        passwordRegisterVerifyField.Select();
                        break;
                    case 3:
                        usernameRegisterField.Select();
                        break;
                }
            }
        }
        if (loginUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                LoginButton();
                openScene.LoadTransition();
            }
            if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                InputSelected--;
                if (InputSelected < 0)
                    InputSelected = 1;
                SelectInputField();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                InputSelected++;
                if (InputSelected > 1)
                    InputSelected = 0;
                SelectInputField();
            }
        }
        else if (registerUI.activeSelf == true)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                InputSelected--;
                if (InputSelected < 0)
                    InputSelected = 3;
                SelectInputField();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                InputSelected++;
                if (InputSelected > 3)
                    InputSelected = 0;
                SelectInputField();
            }
        }
    }
    public void emailLoginSelected() => InputSelected = 0;
    public void passwordLoginSelected() => InputSelected = 1;
    public void emailRegisterSelected() => InputSelected = 0;
    public void passwordRegisterSelected() => InputSelected = 1;
    public void passwordRegisterVerifySelected() => InputSelected = 2;
    public void usernameRegisterSelected() => InputSelected = 3;

    private void InitializeFirebase()
    {
        //Set the authentication instance object
        db = FirebaseFirestore.DefaultInstance;
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
    }
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text, usernameRegisterField.text));
    }
    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);
        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            user = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", user.DisplayName, user.Email);
            confirmLoginText.text = "Logged In";
            yield return new WaitForSeconds(0.15f);
            warningLoginText.text = "Welcome "+user.DisplayName;
            playerManager.userUID = user.UserId;
            playerManager.user = user;
            playerManager.auth = auth;
            playerManager.userName = user.DisplayName;
            openScene.LoadTransition();
            yield return new WaitForSeconds(2.5f);
            Debug.Log("login");
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator Register(string _email, string _password, string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            warningRegisterText.text = "Missing Username";
        }
        else if (passwordRegisterField.text != passwordRegisterVerifyField.text)
        {
            //If the password does not match show a warning
            warningRegisterText.text = "Password Does Not Match!";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                user = RegisterTask.Result;

                if (user != null)
                {
                    //Create a user profile and set the username
                    Firebase.Auth.UserProfile profile = new Firebase.Auth.UserProfile { DisplayName = _username };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = user.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);
                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //adding user to firestore
                        PlayerData player = new PlayerData { exp=0,silver =0 };

                         db.Collection("account").Document(user.UserId).SetAsync(player);
                        
                        //Username is now set
                        //Now return to login screen
                        warningRegisterText.text = "Register success";
                        yield return new WaitForSeconds(1f);
                        LoginScreen();
                    }
                }
            }
        }
    }

    // Manger the button
    [SerializeField] GameObject loginUI;
    [SerializeField] GameObject registerUI;
    public void LoginScreen() //Back button
    {
        loginUI.SetActive(true);
        //registerUI.SetActive(false);
    }
    public void RegisterScreen() // Register button
    {
        loginUI.SetActive(false);
        registerUI.SetActive(true);
    }
}
