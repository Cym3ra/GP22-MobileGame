using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogOutUser : MonoBehaviour
{

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        //Call the firebase singleton and let it handle logout.
        FirebaseSignIn.Instance.SignOut();
    }
}
