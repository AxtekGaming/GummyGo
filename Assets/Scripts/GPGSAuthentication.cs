using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GPGSAuthentication : MonoBehaviour
{

    public static PlayGamesPlatform platform;

    LevelLoading myLevelLoader;

    void Start()
    {
        myLevelLoader = FindObjectOfType<LevelLoading>();

        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true; 

            platform = PlayGamesPlatform.Activate(); 
        }

        Social.Active.localUser.Authenticate(success =>
        {
            if (success)
            {
                StartCoroutine(DelaySceneLoading());
                FindObjectOfType<GPGSLeaderboard>().AbleToDisplayLB(true);
            }
            else
            {
                StartCoroutine(DelaySceneLoading());
                FindObjectOfType<GPGSLeaderboard>().AbleToDisplayLB(false);
            }
        });
    }
    
    IEnumerator DelaySceneLoading() 
    {
        yield return new WaitForSeconds(2.925f);
        myLevelLoader.LoadStartMenuFromGDPR();
    }
    

}
