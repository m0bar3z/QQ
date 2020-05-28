using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VibrationKey : MonoBehaviour
{
    public Toggle vibrationToggle;
    public void ChangeVibrationActivityStatus()
    {
        if (vibrationToggle.isOn)
        {
            PlayerPrefsManager.SetMasterVibration(1);
        } else
        {
            PlayerPrefsManager.SetMasterVibration(0);
        }
    }
}
