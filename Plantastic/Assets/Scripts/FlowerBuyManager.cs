using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BoughtFlower
{
    public bool fBought;
}

public class FlowerBuyManager : MonoBehaviour
{
    public int buyPriceFlower;
    public bool hasBought = false;
    [SerializeField] Button buyButton;
    [SerializeField] ParticleSystem particles;

    private void Start()
    {
        //buyButton.onClick.AddListener(CheckFlowerBought);

        //FirebaseSaveManager.Instance.LoadData<BoughtFlower>("users/" + FirebaseSignIn.Instance.GetUserID, LoadIfFlowerBought);
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
        if (buyPriceFlower <= CurrencyManager.instance.flowerAmounts && !hasBought)
        {
            buyButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            buyButton.GetComponent<Button>().interactable = false;
        }
    }

    public void CheckFlowerBought()
    {
        BoughtFlower plantBought = new BoughtFlower();
        plantBought.fBought = hasBought;

        string jsonString = JsonUtility.ToJson(plantBought);
        string path = "users/" + FirebaseSignIn.Instance.GetUserID;
        FirebaseSaveManager.Instance.PushData(path, jsonString);
    }

    public void LoadIfFlowerBought(BoughtFlower flower)
    {
        hasBought = flower.fBought;
    }
}
