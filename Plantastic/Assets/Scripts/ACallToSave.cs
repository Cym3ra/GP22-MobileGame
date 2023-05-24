using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ACallToSave : MonoBehaviour
{

    public static event Action OnSaveGame;
    public static event Action OnLoadOtherRoom;

    public void SaveGameCall()
    {
        OnSaveGame?.Invoke();
    }

    public void LoadRoomCall()
    {
        OnLoadOtherRoom?.Invoke();
    }
}
