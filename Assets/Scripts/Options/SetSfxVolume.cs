using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetSfxVolume : MonoBehaviour
{
    public Slider sfxSlider;
    public VolumeManager vm;

    public void ChangeSfxVolume()
    {
        PlayerPrefsManager.SetMasterSFX(sfxSlider.value);
    }

    private void Awake()
    {
        vm = FindObjectOfType<VolumeManager>();
    }

    void Start()
    {
        sfxSlider.value = PlayerPrefsManager.GetMasterSFX();
    }
}
