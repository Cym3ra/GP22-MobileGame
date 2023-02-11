using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using TMPro;
using System.Collections;

public class FirebaseSignIn : MonoBehaviour
{
    //Singleton variables
    private static FirebaseSignIn _instance;
    public static FirebaseSignIn Instance { get { return _instance; } }

    public TextMeshProUGUI confirmText;

    FirebaseAuth auth;
    //We often need our userID, create a easy way to get it.
    public string GetUserID { get { return auth.CurrentUser.UserId; } }

    private void Awake()
    {
        //Singleton setup
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        if (auth == null)
        {
            //load sign in screen if we are not signed in.
            SceneManager.LoadScene(0);
        }

        //Start our firebase stuff
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            auth = FirebaseAuth.DefaultInstance;

            //Check if we are logged in
            if (auth.CurrentUser == null)
            {
                //if not sign in anonymously
                AnonymousSignIn();
            }
            else
            {
                //We are already logged in
                //the program will remember us
                //Debug.Log(auth.CurrentUser.Email + " is logged in.");
                confirmText.text = auth.CurrentUser.Email + "logging in!";
                StartCoroutine(PlayerIsSignedInLoadNextScene());
            }
        });
    }

    IEnumerator PlayerIsSignedInLoadNextScene()
    {
        yield return new WaitForSecondsRealtime(5f);
        //playButton.interactable = true;
        SceneManager.LoadScene(1);
    }

    private void AnonymousSignIn()
    {
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                //playButton.interactable = true;
                StartCoroutine(PlayerIsSignedInLoadNextScene());
            }
        });
    }

    public void SignInFirebase(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                  newUser.DisplayName, newUser.UserId);
                //status.text = newUser.Email + "is signed in";
                confirmText.text = "Sign in successful!";
                StartCoroutine(PlayerIsSignedInLoadNextScene());
            }
        });
    }

    //Register new user with email/password
    //this also signs the user in
    public void RegisterNewUser(string email, string password)
    {
        Debug.Log("Starting Registration");
        //status.text = "Starting Registration";
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User Registerd: {0} ({1})",
                  newUser.DisplayName, newUser.UserId);
                confirmText.text = "New user registered!";
                //playButton.interactable = true;
                StartCoroutine(PlayerIsSignedInLoadNextScene());
            }
        });
    }

    public void SignOut()
    {
        auth.SignOut();
        SceneManager.LoadScene(0);
    }
}
