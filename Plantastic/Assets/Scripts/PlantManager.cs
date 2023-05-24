using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class PlantSave
{
    public int timesWatered;
    public Vector2 plantPos;
    public int plantStage;
}

public class PlantManager : MonoBehaviour
{
    [SerializeField] Sprite[] plantStages;
    [SerializeField] Image waterTimerImage;
    public int harvestLeavesAmount;
    public int harvestFlowersAmount;
    public int plantStage = 0;
    public int wateringPressed = 0;
    public float timeToHarvest = 10f;
    public float timer;

    float gameTime = 5f;
    SpriteRenderer plant;
    BoxCollider2D plantCollider;
    GameObject plantPanel;

    void Awake()
    {
        plantPanel = GameObject.Find("PlantPanel");
        plant = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        //FirebaseSaveManager.Instance.LoadData<PlantSave>("users/" + FirebaseSignIn.Instance.GetUserID, LoadPlantInfo);
        plantCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        timer = timeToHarvest;

        string path = "rooms/" + FirebaseSignIn.Instance.GetUserRoomID + "/plants/" + gameObject.name;
        FirebaseSaveManager.Instance.LoadData<PlantSave>(path, LoadPlantInfo);
    }

    private void OnMouseDown()
    {
        plantPanel.GetComponent<CanvasGroup>().enabled = false;
        plantPanel.GetComponent<PlantPanelController>().currentPlant = this;
        GetComponent<Animator>().SetTrigger("PlantPressed");
    }

    public IEnumerator Watering(Image waterImg)
    {
        PlantInteraction(1);

        timer = gameTime;

        do
        {
            timer -= Time.deltaTime;
            waterImg.fillAmount = timer / gameTime;
            yield return null;

        } while (timer > 0);
    }

    private void PlantInteraction(int watered)
    {
        wateringPressed += watered;

        if (wateringPressed == 2 || wateringPressed == 5)
        {
            plantStage++;
            UpdatePlantStage();
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

    private void OnEnable()
    {
        ACallToSave.OnSaveGame += SavePlantInfo;
    }

    internal void LoadRoom(string roomID)
    {
        string path = "rooms/" + roomID + "/plants/" + gameObject.name;
        FirebaseSaveManager.Instance.LoadData<PlantSave>(path, LoadPlantInfo);
    }

    private void OnDisable()
    {
        ACallToSave.OnSaveGame -= SavePlantInfo;
    }

    public void SavePlantInfo()
    {
        PlantSave plantInfo = new PlantSave();
        plantInfo.timesWatered = this.wateringPressed;
        plantInfo.plantStage = plantStage;
        plantInfo.plantPos = this.gameObject.transform.position;

        string jsonString = JsonUtility.ToJson(plantInfo);
        string path = "rooms/" + FirebaseSignIn.Instance.GetUserRoomID + "/plants/" + gameObject.name;
        FirebaseSaveManager.Instance.SaveData(path, jsonString);
    }

    private void LoadPlantInfo(PlantSave plantSave )
    {
        wateringPressed = plantSave.timesWatered;
        plant.sprite = plantStages[plantSave.plantStage];
        transform.position = plantSave.plantPos;
    }
}
