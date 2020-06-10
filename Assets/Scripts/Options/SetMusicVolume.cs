using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetMusicVolume : MonoBehaviour
{
    public Slider musicSlider;
    public VolumeManager vm;
    public AudioSource audioSource;

    public void ChangeMusicVolume()
    {
        PlayerPrefsManager.SetMasterMusic(musicSlider.value);
        vm.SetVolume();
    }

    private void Awake()
    {
        vm = FindObjectOfType<VolumeManager>();
    }

    void Start()
    {
        vm = FindObjectOfType<VolumeManager>();
        audioSource = FindObjectOfType<AudioSource>();
        musicSlider.value = PlayerPrefsManager.GetMasterMusic();
        vm.audioSource = audioSource;
    }
}
