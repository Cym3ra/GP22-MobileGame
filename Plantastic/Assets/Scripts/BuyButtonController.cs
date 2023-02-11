using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonController : MonoBehaviour
{
    public int buyPrice;
    public bool hasBought = false;
    [SerializeField] Button buyButton;


    public void SpawnPlantFromIndex(int index)
    {
        //Instantiate(plantList[index], Vector3.zero, Quaternion.identity);
        Vector2 spawnPoint = new Vector2(0, 0);
        FindObjectOfType<SpawnPlant>().plantList[index].gameObject.transform.position = spawnPoint;
        hasBought = true;
        buyButton.GetComponent<Button>().interactable = false;
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
        if (buyPrice <= CurrencyManager.instance.leavesAmount || buyPrice <= CurrencyManager.instance.flowerAmounts)
        {
            if (!hasBought)
            {
                buyButton.GetComponent<Button>().interactable = true;
            }
        }
        else
        {
            buyButton.GetComponent<Button>().interactable = false;
        }
    }
}
