using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
}
