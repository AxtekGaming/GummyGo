using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreController : MonoBehaviour
{
    const string HIGH_SCORE = "high score";
    const string GPGS_HIGH_SCORE = "GPGS high score";

    public static void SetHighScore (int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
        PlayerPrefs.Save();
    }

    public static void SetGPGSHighScore(int score)
    {
        PlayerPrefs.SetInt(GPGS_HIGH_SCORE, score);
        PlayerPrefs.Save();
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);  
    }

    public static int GetGPGSHighScore()
    {
        return PlayerPrefs.GetInt(GPGS_HIGH_SCORE);
    }

}
