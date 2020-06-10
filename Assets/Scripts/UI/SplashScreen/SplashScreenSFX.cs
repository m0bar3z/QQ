using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenSFX : MonoBehaviour
{
    public VolumeManager musicManager;
    void Start()
    {
        musicManager = FindObjectOfType<VolumeManager>();
        musicManager.audioSource = gameObject.GetComponent<AudioSource>();
        musicManager.SetVolume();
    }

    void Update()
    {
        
    }
}
