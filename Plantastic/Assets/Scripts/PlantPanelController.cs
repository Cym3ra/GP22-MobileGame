using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantPanelController : MonoBehaviour
{

    [SerializeField] Button waterButton;
    [SerializeField] Button harvestButton;
    public Image harvestTimerImage;
    public Image waterTimerImage;

    public PlantManager currentPlant;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void WateringInput()
    {
        StartCoroutine(currentPlant.Watering(waterTimerImage));
    }

    public void HarvestInput()
    {
        currentPlant.HarvestPlant();
    }
}
