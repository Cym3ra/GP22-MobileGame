using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//[Serializable]
class PlayerSaveData
{
    public string Name;
    public float ColorHUE;
    public bool Hidden;
    public Vector3 Position;
}


public class TestSAveScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void Save()
    {
        //Create our saveData object
        PlayerSaveData saveData = new PlayerSaveData();

        //Put our data in our object
        //saveData.name = "Robert";
        saveData.ColorHUE = 0.5f;
        saveData.Hidden = false;
        saveData.Position = transform.position;
        //saveData.Rotation = transform.rotation.eulerAngles;

        //Convert saveData object to JSON
        string jsonString = JsonUtility.ToJson(saveData);

        //For now just save it using PlayerPrefs
        PlayerPrefs.SetString("PlayerSaveData", jsonString);
    }

    public void Load()
    {
        //Get the saved jsonString
        string jsonString = PlayerPrefs.GetString("PlayerSaveData");

        //Convert the data to a object
        PlayerSaveData loadedData = JsonUtility.FromJson<PlayerSaveData>(jsonString);

        //We probably would like to add some code in this function
        //that runs if we get broken or no data.
        
    }
}
