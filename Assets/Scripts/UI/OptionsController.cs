using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public Slider volumeSlide;
    public MusicManager musicManager;
    public void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlide.value);
    }
    void Start()
    {
        volumeSlide.value = PlayerPrefsManager.GetMasterVolume();
        musicManager = FindObjectOfType<MusicManager>().GetComponent<MusicManager>();
    }

    private void Update()
    {
        musicManager.audioSource.volume = volumeSlide.value;
    }


}
