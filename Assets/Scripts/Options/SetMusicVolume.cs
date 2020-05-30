using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetMusicVolume : MonoBehaviour
{
    public Slider musicSlider;
    public MusicManager musicManager;

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
        musicSlider.value = PlayerPrefsManager.GetMasterMusic();
    }
}
