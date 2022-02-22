using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] int maxSpeedFromScore = 40;
    bool GameStarted = false;
    bool pauseScoreCounted = true;
    float platformFallSpeed;
    float startingPlatformFallSpeed = 15f;

    Pause pauseScript;
    Score scoreScript;
    AudioSource myAudioSource;
    AudioSource myBackgroundAudio;
    AdMobAds myAdMobAds;

    [Header("Buttons")]
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject rankButton;
    [SerializeField] GameObject shareButton;
    [SerializeField] GameObject rateButton;
    [SerializeField] GameObject restartButton;
    Button theRankButton;
    Button theMenuButton;
    Button theShareButton;
    Button theRateButton;
    Button theRestartButton;

    [Header("Game Begin")]
    [SerializeField] GameObject startingPlatform;
    [SerializeField] GameObject firstLandingPlatform;
    [SerializeField] GameObject gameBeginLabel;


    [Header("Player Death")]
    [SerializeField] GameObject gameOverLabel;
    [SerializeField] GameObject spawners;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] GameObject mainGameScoreText;
    [SerializeField] GameObject pauseButton;
    int score = 0;

    AppPaused appPausedScript;

    void Start()
    {
        gameOverLabel.SetActive(false);
        scoreText.text = score.ToString();
        pauseScript = FindObjectOfType<Pause>();
        appPausedScript = FindObjectOfType<AppPaused>();
        scoreScript = FindObjectOfType<Score>();
        myAudioSource = GetComponent<AudioSource>();
        myBackgroundAudio = FindObjectOfType<BackgroundScroller>().GetComponent<AudioSource>(); 
        theRankButton = rankButton.GetComponent<Button>();
        theMenuButton = menuButton.GetComponent<Button>();
        theShareButton = shareButton.GetComponent<Button>();
        theRateButton = rateButton.GetComponent<Button>();
        theRestartButton = restartButton.GetComponent<Button>();
        myAdMobAds = FindObjectOfType<AdMobAds>();
    }

    private void Update()
    {
        HasGameStarted();
    }

    private void HasGameStarted()
    {
        if (GameStarted)
        {
            return;
        }
        else
        {
            if (!FindObjectOfType<Character>().IsInDeadZone())
            {
                StartGame();
            }
            else return;
        }
    }

    private void StartGame()
    {
        if (Input.GetMouseButtonUp(0))
        {
            pauseButton.SetActive(true);
            FindObjectOfType<Spawner>().SetSpawnToSpawning();
            StartCoroutine(SettingSpeedToInitialPlatforms());
            GameStarted = true;
            appPausedScript.GameHasResumed();
        }
    }

    IEnumerator SettingSpeedToInitialPlatforms()
    {
        yield return new WaitForSeconds(Mathf.Epsilon);
        platformFallSpeed = FindObjectOfType<Platform>().GetCurrentSpeed();
        startingPlatform.GetComponent<Platform>().SetCurrentSpeed(startingPlatformFallSpeed);
        firstLandingPlatform.GetComponent<Platform>().SetCurrentSpeed(platformFallSpeed);
    }

    public void HandleLoseCondition()
    {
        Time.timeScale = 1;
        gameOverLabel.SetActive(true);
        pauseButton.SetActive(false);
        spawners.SetActive(false);

        if(PlayerPrefsController.GetMasterSFX() != 0)
        {
            myAudioSource.Play();
        }

        scoreText.text = scoreScript.FinalScore().ToString();
        highScoreText.text = scoreScript.HighScore().ToString();
        mainGameScoreText.SetActive(false);
        appPausedScript.GameAlreadyPaused();

        if(PlayerPrefsController.GetMasterVolume() == 0.35f)
        {
            myBackgroundAudio.volume = 0.1f;
        }

        ToggleButtons(false);
        StartCoroutine(RaiseBackgroundSound());
    }

    IEnumerator RaiseBackgroundSound()
    {
        yield return new WaitForSeconds(1.3f);
        myBackgroundAudio.volume = PlayerPrefsController.GetMasterVolume();
        ToggleButtons(true);
        FindObjectOfType<RateManager>().CountDeathsToLaunchRate(); 
    }

    private void ToggleButtons(bool ToggleButton)
    {
        theRankButton.interactable = ToggleButton;
        theMenuButton.interactable = ToggleButton;
        theShareButton.interactable = ToggleButton;
        theRateButton.interactable = ToggleButton;
        theRestartButton.interactable = ToggleButton; 
    }

    public void IncreaseGameSpeed(float currentScore)
    {
        if (currentScore >= maxSpeedFromScore || pauseScoreCounted)
        {
            return;
        }
        else
        {
            float speedCalc = currentScore / 80; //original is 80
            float currentSpeedOfGame = 1.1f + speedCalc; 
            Time.timeScale = currentSpeedOfGame;
            pauseScript.SetTheTimeScaleForPausedGame(currentSpeedOfGame);
            pauseScoreCounted = true;
        }
    }

    public void SetPauseCounted()
    {
        pauseScoreCounted = false;
    }

    public void DisableGameStartCanvas() 
    {
        gameBeginLabel.SetActive(false);
    }

}
