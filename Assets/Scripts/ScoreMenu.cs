using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreMenu : MonoBehaviour
{
    [SerializeField] GameObject scoreSplashScreen;
    [SerializeField] TextMeshProUGUI highScoreText;

    public void OpenScoreMenu()
    {
        scoreSplashScreen.SetActive(true);
        highScoreText.text = HighScoreController.GetHighScore().ToString();
    }

    public void CloseScoreMenu()
    {
        scoreSplashScreen.SetActive(false);
    }
}
