using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    private bool playerDeceted;
    public Transform doorPos;
    public float DoorWidth;
    public float DoorHeight;

    public LayerMask whatIsPlayer;

    [SerializeField]
    private string sceneName;

    SceneSwitcher sceneSwitch;

    private void Start() 
    {
        sceneSwitch = FindObjectOfType<SceneSwitcher>();
    }

    private void Update() 
    {
        playerDeceted = Physics2D.OverlapBox(doorPos.position, new Vector2(DoorWidth, DoorHeight), 0, whatIsPlayer);
        
        if(playerDeceted == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                sceneSwitch.SwitchScene(sceneName);
            }
        }
    }
}
