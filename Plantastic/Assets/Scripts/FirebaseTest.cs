using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;

[Serializable]
public class SavePosition
{
    public Vector3 pos;
    public string name = "kalle";
}

public class FirebaseTest : MonoBehaviour
{
    FirebaseAuth auth;

    SavePosition savePosition;

    void Start()
    {
        savePosition = new SavePosition();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            auth = FirebaseAuth.DefaultInstance;

            if (auth.CurrentUser == null)
                AnonymousSignIn();
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Sparar position");
            savePosition.pos = transform.position;

            string jsonString = JsonUtility.ToJson(savePosition);

            DataTest(auth.CurrentUser.UserId, jsonString);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadFromFirebase();
        }
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
            }
        });
    }

    private void DataTest(string userID, string data)
    {
        Debug.Log("Trying to write data...");
        var db = FirebaseDatabase.DefaultInstance;
        db.RootReference.Child("users").Child(userID).SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);
            else
                Debug.Log("DataTestWrite: Complete");
        });
    }


    private void LoadFromFirebase()
    {
        var db = FirebaseDatabase.DefaultInstance;
        var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }

            //here we get the result from our database.
            DataSnapshot snap = task.Result;

            //And send the json data to a function that can update our game.

            Debug.Log(snap.GetRawJsonValue());

            savePosition = JsonUtility.FromJson<SavePosition>(snap.GetRawJsonValue());
            transform.position = savePosition.pos;
        });
    }
}
