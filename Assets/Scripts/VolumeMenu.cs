using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeMenu : MonoBehaviour
{

    [SerializeField] GameObject volumeSplashMenu;

    public void OpenVolumeMenu()
    {
        volumeSplashMenu.SetActive(true);
    }

    public void CloseVolumeMenu()
    {
        volumeSplashMenu.SetActive(false);
    }
}
