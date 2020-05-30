using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public float maxVolume;
    public AudioSource audioSource;
    public void SetSFXVoiume()
    {
        audioSource.volume = PlayerPrefsManager.GetMasterSFX() * maxVolume;
    }
}
