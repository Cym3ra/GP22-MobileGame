using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlantManager : MonoBehaviour
{

    [SerializeField] Sprite[] plantStages;
    [SerializeField] SpriteRenderer plant;
    [SerializeField] Button waterButton;
    [SerializeField] GameObject plantPanel;

    public int plantStage = 0;
    public int wateringPressed = 0;
    float timeBetweenStages = 2f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //timer -= Time.deltaTime;
    }

    private void OnMouseDown()
    {
        //waterButton.gameObject.SetActive(true);
        plantPanel.SetActive(true);
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
