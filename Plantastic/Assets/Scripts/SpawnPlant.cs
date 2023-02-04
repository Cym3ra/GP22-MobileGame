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
        Instantiate(plantList[index], Vector3.zero, Quaternion.identity);
    }

}
