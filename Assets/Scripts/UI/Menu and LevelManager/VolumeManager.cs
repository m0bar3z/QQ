using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public float MaxVolume;
    public AudioSource audioSource;

    private void OnLevelWasLoaded(int level)
    {
        SetVolume();
    }

    public void SetVolume()
    {
        float sfxVol = PlayerPrefsManager.GetMasterSFX() * MaxVolume;
        float musicVol = PlayerPrefsManager.GetMasterMusic() * MaxVolume;

        foreach (AudioSource audioSource in FindObjectsOfType<AudioSource>())
        {
            switch (audioSource.gameObject.tag)
            {
                case "Music":
                    audioSource.volume = musicVol;
                    break;

                case "SFX":
                    audioSource.volume = sfxVol;
                    break;

                default:
                    audioSource.volume = sfxVol;
                    break;
            }
        }
    }
}
