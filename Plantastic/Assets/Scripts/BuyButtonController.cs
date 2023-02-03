using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonController : MonoBehaviour
{
    public int buyPrice;

    [SerializeField] Button buyButton;


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
            buyButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            buyButton.GetComponent<Button>().interactable = false;
        }
    }
}
