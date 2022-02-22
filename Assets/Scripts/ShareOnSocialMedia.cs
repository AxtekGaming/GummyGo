using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ShareOnSocialMedia : MonoBehaviour
{
    [SerializeField] GameObject Panel_share;
    [SerializeField] TextMeshProUGUI txtPanelScore;
    [SerializeField] TextMeshProUGUI txtHomeScore;
    [SerializeField] TextMeshProUGUI txtDate;

    public void ShareScore()
    {
        txtPanelScore.text = txtHomeScore.text;
        DateTime dt = DateTime.Now;

        txtDate.text = dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();

        Panel_share.SetActive(true);
        StartCoroutine(TakeScreenShotAndShare());
    }

    IEnumerator TakeScreenShotAndShare()
    {
        yield return new WaitForEndOfFrame();
        var panelSize = Panel_share.GetComponent<RectTransform>();


        Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false); 
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0); 
        tx.Apply();

        string path = Path.Combine(Application.temporaryCachePath, "shareImage.png");
        File.WriteAllBytes(path, tx.EncodeToPNG());

        Destroy(tx);

        new NativeShare().AddFile(path).SetSubject("This is my score").SetText("share your score with your friends").Share();

        Panel_share.SetActive(false);
    }
}
