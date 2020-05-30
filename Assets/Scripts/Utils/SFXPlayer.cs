using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;
    public SFXManager sfxManager;
    public void SetVolume()
    {
        sfxManager = FindObjectOfType<SFXManager>();
        sfxManager.audioSource = source;
        sfxManager.maxVolume = source.volume;
        sfxManager.SetSFXVoiume();
    }
    private void Start()
    {
        SetVolume();
        source.PlayOneShot(
            clips[Random.Range(0, clips.Length)]    
        );
    }
}
