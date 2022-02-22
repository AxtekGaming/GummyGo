using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using System;
using System.Collections;

public class AdMobAds : MonoBehaviour
{
    AudioSource volumeController;

    private BannerView adBanner;
    private InterstitialAd adInterstitial;
    bool adHasBeenClosed = false;
    bool showingAdPause = false;

    int npaValue = -1; 

    private string appID, idBanner, idInterstitial;

    static int loadCounter = 1;
    static bool bannerAdInitilized = false;
    static bool onGameScreen = false;

    void Start()
    {
        npaValue = PlayerPrefs.GetInt("npa", 0); 

        appID = "ca-app-pub-9722636113060048~8819660426";

        MobileAds.Initialize(appID);


        showingAdPause = false;

        if (loadCounter == 3 && onGameScreen)
        {
            RequestInterstitialAd();
            onGameScreen = false;
        }
        
        if (bannerAdInitilized)
        {
            return;
        }
        else
        {
            this.RequestBannerAd();
            bannerAdInitilized = true;
        }
        
        volumeController = FindObjectOfType<VolumeController>().GetComponent<AudioSource>();

    }

    #region Banner Methods
    public void RequestBannerAd()
    {
        //idBanner = "ca-app-pub-3940256099942544/6300978111"; //- testing ID from Google
        idBanner = "ca-app-pub-9722636113060048/1050897338";
        this.adBanner = new BannerView(idBanner, AdSize.Banner, AdPosition.Bottom); 
        AdRequest requestBanAd = AdRequestBuild();
        this.adBanner.LoadAd(requestBanAd);
    }

    public void GameScreenStarted()
    {
        onGameScreen = true;
    }

    private void OnDestroy()
    {
        DestroyInterAd();
    }


    public void DestroyBannerAd() 
    {
        if (adBanner != null)
            adBanner.Destroy();
    }
    #endregion


    public void RequestInterstitialAd()
    {
        //idInterstitial = "ca-app-pub-3940256099942544/1033173712"; //- testing ID from Google
        idInterstitial = "ca-app-pub-9722636113060048/8103390849";

        this.adInterstitial = new InterstitialAd(idInterstitial);
        this.adInterstitial.LoadAd(this.AdRequestBuild());


        this.adInterstitial.OnAdClosed += HandleOnAdClosed;

 
    }


    public void DestroyInterAd()
    {
        if (adInterstitial != null)
            adInterstitial.Destroy();
    }


    public void TriggerAdvertDeathCounter()
    {
        if (loadCounter == 3)
        {
            loadCounter = 1;
            showingAdPause = true;
            ShowInterstitial();
        }
        else
        {
            loadCounter++;
            onGameScreen = false;
            FindObjectOfType<GameController>().HandleLoseCondition();
        }
    }


    public void ShowInterstitial()
    {
        if (adInterstitial.IsLoaded())
        {
            adInterstitial.Show();
            RequestInterstitialAd();
            StartCoroutine(CheckIntAdIsFinished());
        }
        else
        {
            onGameScreen = false;
            FindObjectOfType<GameController>().HandleLoseCondition();
        }
    }

    IEnumerator CheckIntAdIsFinished()
    {
        yield return new WaitUntil(() => adHasBeenClosed);
        onGameScreen = false;
        FindObjectOfType<GameController>().HandleLoseCondition();
        volumeController.volume = PlayerPrefsController.GetMasterVolume();
        adHasBeenClosed = false;
    }

    AdRequest AdRequestBuild() 
    {
        return new AdRequest.Builder().AddExtra("npa", npaValue.ToString()).Build(); 
    }
    

    public bool CheckIfAdWillLoadOnGameOver()
    {
        return showingAdPause;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        adHasBeenClosed = true;
        this.adInterstitial.OnAdClosed -= HandleOnAdClosed;
        RequestInterstitialAd();
    }


}
