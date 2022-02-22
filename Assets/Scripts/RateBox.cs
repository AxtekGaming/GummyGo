using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateBox : MonoBehaviour
{
    public void ClickNoThanks()
    {
        PlayerPrefs.SetInt("AppHasBeenRated", 1);
        gameObject.SetActive(false);
    }

    public void ClickLater()
    {
        gameObject.SetActive(false);
    }

    public void ClickRate()
    {
        PlayerPrefs.SetInt("AppHasBeenRated", 1);
        gameObject.SetActive(false);
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.axtekgaming.GummyGo"); 
    }

}