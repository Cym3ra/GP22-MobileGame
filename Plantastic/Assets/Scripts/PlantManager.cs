using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{
    [SerializeField] Sprite[] plantStages;
    [SerializeField] SpriteRenderer plant;
    [SerializeField] Button waterButton;

    public int plantStage = 0;
    public int wateringPressed = 0;
    float timeBetweenStages = 2f;
    float timer;

    void Start()
    {
        
    }


    void Update()
    {
        timer -= Time.deltaTime;

    }

    private void OnMouseDown()
    {
        waterButton.gameObject.SetActive(true);
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
                //timer = timeBetweenStages;

                Debug.Log("First");
            }
        }

        if (plantStages[1])
        {
            if (wateringPressed == 5)
            {
                Debug.Log("Second");
                plantStage++;
                UpdatePlantStage();
                //timer = timeBetweenStages;

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
