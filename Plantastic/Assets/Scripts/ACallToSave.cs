using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ACallToSave : MonoBehaviour
{

    public static event Action OnSaveGame;

    public void SaveGameCall()
    {
        OnSaveGame?.Invoke();
    }
}
