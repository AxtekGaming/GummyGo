using UnityEngine;
using UnityEngine.UI;
public class SoundButtons : MonoBehaviour
{

    [SerializeField] Sprite volumeButton;
    [SerializeField] Sprite mutedVolumeButton;

    Image myImage;

    private void Start()
    {
        myImage = GetComponent<Image>();

        if(PlayerPrefsController.GetInitialSoundAndVolume() == 0)
        {
            VolumeIsOn();
        }
        else
        {
            if (PlayerPrefsController.GetMasterVolume() == 0)
            {
                MutedVolume();
            }
            else
            {
                VolumeIsOn();
            }
        }
    }

    public void VolumeIsOn()
    {
        myImage.sprite = volumeButton;
    }

    public void MutedVolume()
    {
        myImage.sprite = mutedVolumeButton;
    }


}
