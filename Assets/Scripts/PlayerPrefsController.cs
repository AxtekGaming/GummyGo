using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{

    const string MASTER_VOLUME_KEY = "master volume";
    const string MASTER_SFX_KEY = "master SFX";
    const string MASTER_FIRST_TIME_GAME_START = "inital SFX and volume";

    public static void SetMasterVolume(float volume)
    {
        PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
    }

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static void SetMasterSFX(float volume)
    {
        PlayerPrefs.SetFloat(MASTER_SFX_KEY, volume);
    }

    public static float GetMasterSFX()
    {
        return PlayerPrefs.GetFloat(MASTER_SFX_KEY);
    }

    public static void SetInitalSoundAndVolume(float allSounds)
    {
        PlayerPrefs.SetFloat(MASTER_FIRST_TIME_GAME_START, allSounds);
    }

    public static float GetInitialSoundAndVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_FIRST_TIME_GAME_START);
    }




}

