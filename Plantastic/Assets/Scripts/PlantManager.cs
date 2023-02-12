using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class PlantSave
{
    public int timesWatered;
    public int plantGrowth;
    public Vector2 plantPos;
    public Sprite savedPlant;
}

public class PlantManager : MonoBehaviour
{

    [SerializeField] Sprite[] plantStages;
    [SerializeField] SpriteRenderer plant;
    BoxCollider2D plantCollider;

    public GameObject plantPanel;
    public int harvestLeavesAmount;
    public int harvestFlowersAmount;
    public int plantStage = 0;
    public int wateringPressed = 0;
    public float timeToHarvest = 10f;
    public float timer;

    void Awake()
    {
        plantPanel = GameObject.Find("PlantPanel");

    }
    private void Start()
    {
        //FirebaseSaveManager.Instance.LoadData<PlantSave>("users/" + FirebaseSignIn.Instance.GetUserID, LoadPlantInfo);
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        timer = timeToHarvest;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SavePlantInfo();
        }
    }

    private void OnMouseDown()
    {
        plantPanel.GetComponent<CanvasGroup>().enabled = false;
        plantPanel.GetComponent<PlantPanelController>().currentPlant = this;
        GetComponent<Animator>().SetTrigger("PlantPressed");
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
        plantCollider.size = plant.sprite.bounds.size;
        plantCollider.offset = new Vector2(0, plant.bounds.size.y / 2);
    }

    public void HarvestPlant()
    {
        CurrencyManager.instance.IncreaseLeaves(harvestLeavesAmount);
        CurrencyManager.instance.IncreaseFlowers(harvestFlowersAmount);
    }

    public void CountDown(Image fillImage)
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            fillImage.fillAmount = timer / timeToHarvest;
        }
        if (timer < 0)
        {
            timer = 0;
        }
    }

    /*private void OnEnable()
    {
        ACallToSave.OnSaveGame += SavePlantInfo;
    }
    private void OnDisable()
    {
        ACallToSave.OnSaveGame -= SavePlantInfo;
    }*/

    public void SavePlantInfo()
    {
        plant.sprite = plantStages[plantStage];
        PlantSave plantInfo = new PlantSave();
        plantInfo.timesWatered = this.wateringPressed;
        plantInfo.savedPlant = plant.sprite;
        plantInfo.plantPos = this.gameObject.transform.position;

        string jsonString = JsonUtility.ToJson(plantInfo);
        string path = "users/" + FirebaseSignIn.Instance.GetUserID;
        FirebaseSaveManager.Instance.SaveData(path, jsonString);
    }

    private void LoadPlantInfo(PlantSave plantSave )
    {
        plant.sprite = plantSave.savedPlant;
        wateringPressed = plantSave.timesWatered;
        plant.sprite = plantStages[plantStage];
        transform.position = plantSave.plantPos;
    }
}
