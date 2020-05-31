using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSFXVolume : MonoBehaviour
{
    public SFXManager sfxManager;
    public AudioSource audioSource;

    public void SetCharVolume()
    {
        sfxManager.SetSFXVoiume();
    }

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        sfxManager = FindObjectOfType<SFXManager>();
        sfxManager.audioSource = audioSource;
        sfxManager.maxVolume = audioSource.volume;
        SetCharVolume();
    }
}
