using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VibrationKey : MonoBehaviour
{
    public Toggle vibrationToggle;

    public void SetVibrationToggleCheckbox()
    {
        if(PlayerPrefsManager.VibrationIsActive)
        {
            vibrationToggle.isOn = true;
        }
        else
        {
            vibrationToggle.isOn = false;
        }
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
