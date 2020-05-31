using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    const string MASTER_MUSIC_KEY = "master_volume";
    const string MASTER_SCORE_KEY = "master_score";
    const string MASTER_VIBRATION_KEY = "master_vibrate";
    const string MASTER_CAMERASHAKE_KEY = "master_camera_shake";
    const string MASTER_SFX_KEY = "master_sfx";
    const string CHARACTER_INDEX = "char_index";
    
    public static void ResetKeys()
    {
        PlayerPrefs.DeleteAll();
    }
    //
    // SFX volume
    public static void SetMasterSFX(float sfxVol)
    {
        if (sfxVol >= 0 && sfxVol <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_SFX_KEY, sfxVol);
        }
    }
    public static float GetMasterSFX()
    {
        return PlayerPrefs.GetFloat(MASTER_SFX_KEY);
    }
    //
    // camera shake activity
    public static bool CameraShakeIsActive
    {
        get
        {
            if (PlayerPrefs.GetInt(MASTER_CAMERASHAKE_KEY) == 1)
                return true;
            else
                return false;
        }
        set
        {
            if (value == true)
                SetMasterCamShake(1);
            else
                SetMasterCamShake(0);
        }
    } 
    static void SetMasterCamShake(int activity)
    {
        if(activity == 1 || activity == 0)
        {
            PlayerPrefs.SetInt(MASTER_CAMERASHAKE_KEY, activity);
        }
    }
    //
    // vibration activity
    public static bool VibrationIsActive
    {
        get
        {
            if (PlayerPrefs.GetInt(MASTER_VIBRATION_KEY) == 1)
                return true;
            else
                return false;
        }
        set
        {
            if(value == true)
                SetMasterVibration(1);
            else
                SetMasterVibration(0);
        }
    }
    static void SetMasterVibration(int activity)
    {
        if(activity == 1 || activity == 0)
        {
            PlayerPrefs.SetInt(MASTER_VIBRATION_KEY, activity);
        }
    }
    //
    // music volume
    public static void SetMasterMusic(float volume)
    {
        if(volume >= 0 && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_MUSIC_KEY, volume);
        }
    }
    public static float GetMasterMusic()
    {
        return PlayerPrefs.GetFloat(MASTER_MUSIC_KEY);
    }
    //
    // Score or whatever
    public static void SetMasterScore(int score)
    {
        PlayerPrefs.SetInt(MASTER_SCORE_KEY, score);
    }

    public static int GetMasterScore()
    {
        return PlayerPrefs.GetInt(MASTER_SCORE_KEY);
    }

    // character
    public static void SetCharacter(int charIndex)
    {
        PlayerPrefs.SetInt(CHARACTER_INDEX, charIndex);
    }

    public static int GetCharacter()
    {
        return PlayerPrefs.GetInt(CHARACTER_INDEX);
    }

}
