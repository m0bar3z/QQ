using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetMusicVolume : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider musicSlider;

    public void ChangeMusicVolume()
    {
        PlayerPrefsManager.SetMasterMusic(musicSlider.value);
        audioSource.volume = musicSlider.value;
    }

    void Start()
    {
        musicSlider.value = PlayerPrefsManager.GetMasterMusic();
        audioSource = FindObjectOfType<AudioSource>(); 
    }
    void Update()
    {
        ChangeMusicVolume();
    }
}
