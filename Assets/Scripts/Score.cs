using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    GameController gamecontrollerScript;
    GPGSLeaderboard myLeaderboard;
    int score = 0;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gamecontrollerScript = FindObjectOfType<GameController>();
        myLeaderboard = FindObjectOfType<GPGSLeaderboard>();
        scoreText.text = score.ToString();
    }

    public void AddToScore()
    {
        score++;
        scoreText.text = score.ToString();
        gamecontrollerScript.SetPauseCounted();
    }
    
    public float GetScore()
    {
        return score;
    }

    public int FinalScore()
    {
        return score;
    }

    public int HighScore()
    {
        myLeaderboard.UpdateLeaderBoardScore(score);
        if (score > HighScoreController.GetHighScore())
        {
            HighScoreController.SetHighScore(score);
            return score;
        }
        else
        {
            return HighScoreController.GetHighScore();

        }
    }

}
