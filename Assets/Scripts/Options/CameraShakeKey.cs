using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CameraShakeKey : MonoBehaviour
{
    public Toggle camShakeToggle;

    public void ChangeCameraShakeActivityStatus()
    {
        if (camShakeToggle.isOn)
            PlayerPrefsManager.CameraShakeIsActive = true;
        else
            PlayerPrefsManager.CameraShakeIsActive = false;
    }


    public void SetCamShakeToggleCheckBox()
    {
        camShakeToggle.isOn = PlayerPrefsManager.CameraShakeIsActive;
    }


    void Start()
    {
        SetCamShakeToggleCheckBox();
    }
}
