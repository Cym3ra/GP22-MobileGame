using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{

    [SerializeField] Sprite[] plantStages;
    [SerializeField] SpriteRenderer plant;
    public GameObject plantPanel;

    public int harvestLeavesAmount;
    public int harvestFlowersAmount;
    public int plantStage = 0;
    public int wateringPressed = 0;
    float timeBetweenStages = 2f;
    float timer;

    void Awake()
    {
        plantPanel = GameObject.Find("PlantPanel");

    }
    private void Start()
    {
        plantPanel.gameObject.SetActive(false);
    }

    void Update()
    {
        //timer -= Time.deltaTime;

    }

    private void OnMouseDown()
    {
        plantPanel.gameObject.SetActive(true);
        plantPanel.GetComponent<PlantPanelController>().currentPlant = this;
    }

    public void Watering()
    {
        PlantInteraction(1);
    }

    private void PlantInteraction(int watered)
    {
        wateringPressed += watered;

        if (plantStages[0])
        {
            if (wateringPressed == 2)
            {
                plantStage++;
                UpdatePlantStage();
            }
        }

        if (plantStages[1])
        {
            if (wateringPressed == 5)
            {
                plantStage++;
                UpdatePlantStage();
            }
        }
    }

    private void UpdatePlantStage()
    {
        plant.sprite = plantStages[plantStage];
    }

    public void HarvestPlant()
    {
        CurrencyManager.instance.IncreaseLeaves(harvestLeavesAmount);
        CurrencyManager.instance.IncreaseFlowers(harvestFlowersAmount);
    }
}
