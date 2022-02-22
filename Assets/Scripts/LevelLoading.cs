using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour
{
    public void LoadStartMenu()
    {
        Time.timeScale = 1;
        StartCoroutine(DelayNextScene(1));      
    }

    public void LoadStartMenuFromGDPR()
    {
        CheckForGDPR();
    }


    public void LoadGame()
    {
        Time.timeScale = 1;
        FindObjectOfType<AdMobAds>().GameScreenStarted();
        StartCoroutine(DelayNextScene(2));
    }
      
    IEnumerator DelayNextScene(int Scene) 
    {
        yield return new WaitForSeconds(0.075f);
        SceneManager.LoadScene(Scene);
    }

    void CheckForGDPR()
    {
        if (PlayerPrefs.GetInt("npa", -1) == -1) //checks to see if null
        {
            Time.timeScale = 0;
            FindObjectOfType<GDPR>().ShowGDPRMenu();
        }
        else
        {
            LoadStartMenu();
        }
    }




}
