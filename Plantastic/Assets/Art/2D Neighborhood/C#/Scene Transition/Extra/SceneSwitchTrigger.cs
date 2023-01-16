using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchTrigger : MonoBehaviour
{

    SceneSwitcher sceneSwitch;

    [SerializeField]
    private string sceneName;
    private void Start()
    {
        sceneSwitch = FindObjectOfType<SceneSwitcher>();
    }

    private void OnTriggerEnter2D(Collider2D collsion) 
    {
        if(collsion.tag == "Player")
        {
            sceneSwitch.SwitchScene(sceneName);
        }   
    }
}
