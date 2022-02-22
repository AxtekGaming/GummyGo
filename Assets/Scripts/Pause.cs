using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseSplashScreen;
    [SerializeField] GameObject pauseButton;

    AppPaused appPausedScript;
    Character characterScript;

    float theCurrentSpeedOfGame = 1.0f;

    private void Start()
    {
        appPausedScript = FindObjectOfType<AppPaused>();
        characterScript = FindObjectOfType<Character>();
    }

    public void OpenMenu()
    {
        characterScript.PauseTheGame();
        pauseSplashScreen.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0;
        appPausedScript.GameAlreadyPaused();
    }

    public void ResumeGame()
    {
        pauseSplashScreen.SetActive(false);
        Time.timeScale = theCurrentSpeedOfGame;
        pauseButton.SetActive(true);
        characterScript.ResumeTheGame();
        appPausedScript.GameHasResumed();
    }

    public void SetTheTimeScaleForPausedGame(float currentGameSpeed)
    {
        theCurrentSpeedOfGame = currentGameSpeed;
    }

}
