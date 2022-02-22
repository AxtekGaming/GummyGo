using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RateManager : MonoBehaviour
{
    [SerializeField] RateBox rateBox;
    [SerializeField] int countToRate = 0;

    public void CountDeathsToLaunchRate()
    {
        if (PlayerPrefs.GetInt("AppHasBeenRated") == 0)
        {
            PlayerPrefs.SetFloat("RatingCounter", PlayerPrefs.GetFloat("RatingCounter") + 1);
        }


        if (PlayerPrefs.GetFloat("RatingCounter") % countToRate == 0 && PlayerPrefs.GetInt("AppHasBeenRated") == 0)
        {
            rateBox.gameObject.SetActive(true);
        }
    }

}

