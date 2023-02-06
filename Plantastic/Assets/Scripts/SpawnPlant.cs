using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlant : MonoBehaviour
{
    public List<GameObject> plantList = new List<GameObject>();

    void Start()
    {

    }

    public void SpawnPlantFromIndex(int index)
    {
        //Instantiate(plantList[index], Vector3.zero, Quaternion.identity);
        Vector2 spawnPoint = new Vector2(0, 0);
        plantList[index].gameObject.transform.position = spawnPoint;
        
    }

}
