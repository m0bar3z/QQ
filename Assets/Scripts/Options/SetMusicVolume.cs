using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetMusicVolume : MonoBehaviour
{
    public Slider musicSlider;
    public MusicManager musicManager;
    public AudioSource audioSource;

    public void ChangeMusicVolume()
    {
        PlayerPrefsManager.SetMasterMusic(musicSlider.value);
        musicManager.SetVolume();
    }

    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        audioSource = FindObjectOfType<AudioSource>();
        musicSlider.value = PlayerPrefsManager.GetMasterMusic();
        musicManager.audioSource = audioSource;
    }
}
