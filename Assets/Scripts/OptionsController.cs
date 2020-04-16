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
        print("getmastervolume:   " + PlayerPrefsManager.GetMasterVolume());
        volumeSlide.value = PlayerPrefsManager.GetMasterVolume();
        musicManager = GameObject.FindObjectOfType<MusicManager>().GetComponent<MusicManager>();
    }

    private void Update()
    {
        musicManager.ChnageVolume(volumeSlide.value);
    }


}
