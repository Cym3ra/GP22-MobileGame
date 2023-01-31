using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{

    [SerializeField] Sprite[] plantStages;
    [SerializeField] SpriteRenderer plant;
    [SerializeField] GameObject plantPanel;

    public int plantStage = 0;
    public int wateringPressed = 0;
    float timeBetweenStages = 2f;
    float timer;

    void Start()
    {

    }

    void Update()
    {
        //timer -= Time.deltaTime;

    }

    private void OnMouseDown()
    {
        plantPanel.SetActive(true);
        plantPanel.GetComponent<PlantPanelController>().currentPlant = this;
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
                Debug.Log("First");
            }
        }

        if (plantStages[1])
        {
            if (wateringPressed == 5)
            {
                plantStage++;
                UpdatePlantStage();
                Debug.Log("Second");
            }
        }
    }

    public void Watering()
    {
        PlantInteraction(1);
    }

    private void UpdatePlantStage()
    {
        plant.sprite = plantStages[plantStage];
    }
}
