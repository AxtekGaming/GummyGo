using UnityEngine;
using UnityEngine.UI;
public class SFXButtons : MonoBehaviour
{
    [SerializeField] Sprite sfxButton;
    [SerializeField] Sprite mutedSFXButton;
    Image myImage;

    private void Start()
    {
        myImage = GetComponent<Image>();

        if (PlayerPrefsController.GetInitialSoundAndVolume() == 0)
        {
            SFXIsOn();
        }
        else
        {
            if (PlayerPrefsController.GetMasterSFX() == 0)
            {
                MutedSFX();
            }
            else
            {
                SFXIsOn();
            }
        }
    }

    public void SFXIsOn()
    {
        myImage.sprite = sfxButton;
    }

    public void MutedSFX()
    {
        myImage.sprite = mutedSFXButton;
    }

}
