using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


[Serializable]
public class CheckIfBought
{
    public bool isBought;
}

public class LeavesBuyManager : MonoBehaviour
{
    public int buyPriceLeaf;
    public bool hasBought = false;
    [SerializeField] Button buyButton;
    [SerializeField] ParticleSystem particles;

    private void Start()
    {
        //buyButton.onClick.AddListener(CheckPlantBought);

        //FirebaseSaveManager.Instance.LoadData<CheckIfBought>("users/" + FirebaseSignIn.Instance.GetUserID, LoadIfPlantBought);
    }

    public void SpawnPlantFromIndex(int index)
    {
        //Instantiate(plantList[index], Vector3.zero, Quaternion.identity);
        Vector2 spawnPoint = new Vector2(0, -4);
        FindObjectOfType<SpawnPlant>().plantList[index].gameObject.transform.position = spawnPoint;
        hasBought = true;
        buyButton.GetComponent<Button>().interactable = false;
        particles.Play();
    }

    private void OnEnable()
    {
        StoreManager.OnStoreActivate += CheckButtonInteractable;
    }

    private void OnDisable()
    {
        StoreManager.OnStoreActivate -= CheckButtonInteractable;
    }


    public void CheckButtonInteractable()
    {
        if (buyPriceLeaf <= CurrencyManager.instance.leavesAmount && !hasBought)
        {
            buyButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            buyButton.GetComponent<Button>().interactable = false;
        }
    }

    public void CheckPlantBought()
    {
        CheckIfBought plantBought = new CheckIfBought();
        plantBought.isBought = hasBought;

        string jsonString = JsonUtility.ToJson(plantBought);
        string path = "users/" + FirebaseSignIn.Instance.GetUserID;
        FirebaseSaveManager.Instance.PushData(path, jsonString);
    }

    public void LoadIfPlantBought(CheckIfBought checkBought)
    {
        hasBought = checkBought.isBought;
    }
}
