using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDPR : MonoBehaviour
{

    [SerializeField] GameObject gdprMenu;


    private void Awake()
    {
        gdprMenu.SetActive(false);
    }

    public void OnUserClickYes()
    {
        PlayerPrefs.SetInt("npa", 0); //double check these values
        HideGDPRMenu();
        FindObjectOfType<LevelLoading>().LoadStartMenu();
    }

    public void OnUserClickNo()
    {
        PlayerPrefs.SetInt("npa", 1);
        HideGDPRMenu();
        FindObjectOfType<LevelLoading>().LoadStartMenu();
    }

    public void ShowGDPRMenu()
    {
        gdprMenu.SetActive(true);
    }

    public void HideGDPRMenu()
    {
        gdprMenu.SetActive(false);
    }



}
