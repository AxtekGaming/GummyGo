using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeController : MonoBehaviour
{

    AudioSource myBackgroundMusic;

    private static VolumeController volumecontrollerInstance;

    void Start()
    {

       if(volumecontrollerInstance == null)
        {
            DontDestroyOnLoad(this);
            volumecontrollerInstance = this;
            myBackgroundMusic = GetComponent<AudioSource>();

            myBackgroundMusic.volume = PlayerPrefsController.GetMasterVolume();
            myBackgroundMusic.Play();
        }
    }
}
