using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadRandomRoom : MonoBehaviour
{
    private void OnEnable()
    {
        ACallToSave.OnLoadOtherRoom += LoadRoom;
    }



    private void OnDisable()
    {
        ACallToSave.OnLoadOtherRoom  -= LoadRoom;
    }


    internal void LoadRoom()
    {
        //Select random room
        Debug.Log("try to load");
        FirebaseSaveManager.Instance.LoadMultipleID("rooms", RoomWasSelected);
    }

    private static void RoomWasSelected(List<string> roomList)
    {
        string myRoom = FirebaseSignIn.Instance.GetUserRoomID;

        string randomRoom;
        //Seöect one random key frpm this list
        //check so it's not our key

        do
        {
            int randomrooms = Random.Range(0, roomList.Count);
            randomRoom = roomList[randomrooms];
        }
        while (myRoom == randomRoom);

        ////Set roomID to our new random key instead
        //string roomID = roomList2[randomrooms];
            //"-NWC06MkElXuyPsCibOJ";


        //tell all plants to load them self
        var plantManagerList = FindObjectsOfType<PlantManager>();

        foreach (var plant in plantManagerList)
        {
            plant.LoadRoom(randomRoom);
        }
    }
}
