using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SetSfxVolume : MonoBehaviour
{
    public Slider sfxSlider;
    public SFXManager sfxManager;

    public void ChangeSfxVolume()
    {
        PlayerPrefsManager.SetMasterSFX(sfxSlider.value);
    }
    private void Awake()
    {
        sfxManager = FindObjectOfType<SFXManager>();
    }
    void Start()
    {
        sfxSlider.value = PlayerPrefsManager.GetMasterSFX();
    }
}
