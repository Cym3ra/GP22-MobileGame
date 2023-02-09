using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;
using System;


[Serializable]
public class CurrencyAmount
{
    public string leavesCurrency;
    public string flowersCurrency;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SaveCurrency();
        }
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

    public void SaveCurrency()
    {
        CurrencyAmount totalCurrency = new CurrencyAmount();
        totalCurrency.leavesCurrency = leavesText.text;
        totalCurrency.flowersCurrency = flowersText.text;

        string jsonString = JsonUtility.ToJson(totalCurrency);
        string path = "users/" + FirebaseSignIn.Instance.GetUserID;
        FirebaseSaveManager.Instance.SaveData(path, jsonString);
    }
}
