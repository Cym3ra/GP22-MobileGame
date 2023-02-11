using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;
using System;


[Serializable]
public class CurrencyAmount
{
    public int leavesCurrency;
    public int flowersCurrency;
    public string leavesDisplay;
    public string flowersDisplay;
}

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    public int leavesAmount = 3;
    public int flowerAmounts = 0;
    [SerializeField] TextMeshProUGUI leavesText;
    [SerializeField] TextMeshProUGUI flowersText;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
            
            instance = this;
    }

    private void Start()
    {
        FirebaseSaveManager.Instance.LoadData<CurrencyAmount>("users/" + FirebaseSignIn.Instance.GetUserID, CurrencyLoaded);
    }

    public void IncreaseLeaves(int amount)
    {
        leavesAmount += amount;
        leavesText.text = "x" + leavesAmount;
    }

    public void IncreaseFlowers(int amount)
    {
        flowerAmounts += amount;
        flowersText.text = "x" + flowerAmounts;
    }

    public void DecreaseLeaves(int amount)
    {
        leavesAmount -= amount;
        leavesText.text = "x" + leavesAmount;
    }

    public void DecreaseFlowers(int amount)
    {
        flowerAmounts -= amount;
        flowersText.text = "x" + flowerAmounts;
    }

    private void OnEnable()
    {
        ACallToSave.OnSaveGame += SaveCurrency;
    }
    private void OnDisable()
    {
        ACallToSave.OnSaveGame -= SaveCurrency;
    }

    public void SaveCurrency()
    {
        CurrencyAmount totalCurrency = new CurrencyAmount();
        totalCurrency.leavesCurrency = leavesAmount;
        totalCurrency.flowersCurrency = flowerAmounts;
        totalCurrency.leavesDisplay = leavesText.text;
        totalCurrency.flowersDisplay = flowersText.text;

        string jsonString = JsonUtility.ToJson(totalCurrency);
        string path = "users/" + FirebaseSignIn.Instance.GetUserID;
        FirebaseSaveManager.Instance.SaveData(path, jsonString);
    }

    private void CurrencyLoaded(CurrencyAmount currency)
    {
        leavesAmount = currency.leavesCurrency;
        flowerAmounts = currency.flowersCurrency;
        leavesText.text = currency.leavesDisplay;
        flowersText.text = currency.flowersDisplay;
    }
}
