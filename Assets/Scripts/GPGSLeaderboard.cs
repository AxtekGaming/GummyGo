using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using TMPro;

public class GPGSLeaderboard : MonoBehaviour
{
    static bool loggedInGame = false;

    [SerializeField] GameObject messageForLB;

    public void OpenLeaderBoard()
    {
        if (loggedInGame)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            ShowLBMenu();
        }
    }

    public void ShowLBMenu()
    {
        messageForLB.SetActive(true);
    }

    public void HideLBMenu()
    {
        messageForLB.SetActive(false);
    }


    public void AbleToDisplayLB(bool loggedIn)
    {
        loggedInGame = loggedIn;
    }


    public void UpdateLeaderBoardScore(int score) 
    {
        if (score <= HighScoreController.GetGPGSHighScore())
        {
            return;
        }

        Social.ReportScore(score, GPGSIds.leaderboard_high_score, (bool success) =>
        {
            if (success)
            {
                HighScoreController.SetGPGSHighScore(score);
            }
        });
    }

}
