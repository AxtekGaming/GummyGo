using UnityEngine;

public class AppPaused : MonoBehaviour
{
    
    bool isPaused = true;

    void OnApplicationPause(bool pauseStatus)
    {
        var checkingIfShouldPause= FindObjectOfType<AdMobAds>().CheckIfAdWillLoadOnGameOver();

        if (!checkingIfShouldPause)
        {
            if (!isPaused)
            {
                FindObjectOfType<Pause>().OpenMenu();
            }
        }  
    }
    
    public void GameAlreadyPaused()
    {
        isPaused = true;
    }

    public void GameHasResumed()
    {
        isPaused = false;
    }

}
