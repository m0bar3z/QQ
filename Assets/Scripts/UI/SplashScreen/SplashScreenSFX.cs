using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenSFX : MonoBehaviour
{
    public MusicManager musicManager;
    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        musicManager.audioSource = gameObject.GetComponent<AudioSource>();
        musicManager.SetVolume();
    }

    void Update()
    {
        
    }
}
