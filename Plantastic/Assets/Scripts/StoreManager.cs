using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoreManager : MonoBehaviour
{

    public static event Action OnStoreActivate;


    public void OpenStore()
    {
        OnStoreActivate?.Invoke();
    }
}
