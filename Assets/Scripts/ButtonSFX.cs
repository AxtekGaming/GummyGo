using UnityEngine;

public class ButtonSFX : MonoBehaviour
{

    AudioSource volumeController, myButtonSound;
    bool musicMuted, sfxMuted;

    private void Start()
    {
        myButtonSound = GetComponent<AudioSource>();
        volumeController = FindObjectOfType<VolumeController>().GetComponent<AudioSource>();

        if (PlayerPrefsController.GetInitialSoundAndVolume() == 0)
        {
            musicMuted = false;
            sfxMuted = false;
            SettingVolumeLevel(0.35f);
            SettingSFXLevel(0.5f);
            PlayerPrefsController.SetInitalSoundAndVolume(1);
        }
        else
        {
            if (PlayerPrefsController.GetMasterVolume() == 0)
            {
                musicMuted = true;
            }
            else
            {
                musicMuted = false;
            }

            if (PlayerPrefsController.GetMasterSFX() == 0)
            {
                sfxMuted = true;
            }
            else
            {
                sfxMuted = false;
            }
        }
    }

    public void PlayButtonSound()
    {
        if (PlayerPrefsController.GetMasterSFX() != 0)
        {
            GetComponent<AudioSource>().Play();
        }  
    }

    public void MutingBackground()
    {
        if (!musicMuted)
        {
            SettingVolumeLevel(0);
            FindObjectOfType<SoundButtons>().MutedVolume(); ;
            musicMuted = true;
        }
        else
        {
            SettingVolumeLevel(0.35f);
            FindObjectOfType<SoundButtons>().VolumeIsOn();
            musicMuted = false;
        }
    }

    private void SettingVolumeLevel (float volume)
    {
        volumeController.volume = volume;
        PlayerPrefsController.SetMasterVolume(volume);
    }


    public void MutingSFX()
    {
        if (!sfxMuted)
        {
            SettingSFXLevel(0);
            FindObjectOfType<SFXButtons>().MutedSFX();
            sfxMuted = true;
        }
        else
        {
            SettingSFXLevel(0.5f);
            FindObjectOfType<SFXButtons>().SFXIsOn();
            sfxMuted = false;
        }
    }


    private void SettingSFXLevel(float volume)
    {
        myButtonSound.volume = volume;
        PlayerPrefsController.SetMasterSFX(volume); 
    }



}
