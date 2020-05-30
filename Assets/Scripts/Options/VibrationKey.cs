using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VibrationKey : MonoBehaviour
{
    public Toggle vibrationToggle;

    public void SetVibrationToggleCheckbox()
    {
        vibrationToggle.isOn = PlayerPrefsManager.VibrationIsActive;
    }


    public void ChangeVibrationActivityStatus()
    {
        if (vibrationToggle.isOn)
        {
            PlayerPrefsManager.VibrationIsActive = true;
        } else
        {
            PlayerPrefsManager.VibrationIsActive = false;
        }
    }


    private void Start()
    {
        SetVibrationToggleCheckbox();
    }
}
