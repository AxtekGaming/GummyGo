using UnityEngine;
using GoogleMobileAds.Api;

public class AdInitilization : MonoBehaviour
{
    void Start()
    {
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
        .SetTagForUnderAgeOfConsent(TagForUnderAgeOfConsent.True).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        MobileAds.Initialize(initStatus => { });
    }
}


