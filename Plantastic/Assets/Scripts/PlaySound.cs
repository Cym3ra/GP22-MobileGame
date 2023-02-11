using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
 
    void Start()
    {

    }


    public void PlayEffect()
    {
        SoundManager.Instance.PlaySound(clip);
    }

}
