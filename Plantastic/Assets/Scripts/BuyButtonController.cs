using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButtonController : MonoBehaviour
{
    public int buyPrice;
    public bool hasBought = false;

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
        if (buyPrice <= CurrencyManager.instance.leavesAmount && !hasBought || buyPrice <= CurrencyManager.instance.flowerAmounts && !hasBought)
        {
            buyButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            buyButton.GetComponent<Button>().interactable = false;
        }
    }

    // TO DO check if plant has been bought, and if it has, button is non-interactable, put it in check with CheckButtonInteractable
    // add the script to the button component, and add a button listener if clicked: hasBought is true then(check if this works)
}
