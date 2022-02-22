using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyPolicy : MonoBehaviour
{
    [SerializeField] GameObject privacyPolicySplash;

    public void OpenPPMenu()
    {
        privacyPolicySplash.SetActive(true);
    }

    public void ClosePPMenu()
    {
        privacyPolicySplash.SetActive(false);
    }

    public void OpenPPOnWeb()
    {
        Application.OpenURL("https://axtekgaming.wixsite.com/website/gummygo-privacypolicy");
    }
}


