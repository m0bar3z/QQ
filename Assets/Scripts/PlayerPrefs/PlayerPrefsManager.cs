using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    const string MASTER_MUSIC_KEY = "master_volume";
    const string MASTER_SCORE_KEY = "master_score";
    const string MASTER_VIBRATION_KEY = "master_vibrate";
    
    public static void ResetKeys()
    {
        PlayerPrefs.DeleteAll();
    }
    // vibration activity
    public static void SetMasterVibration(int activity)
    {
        PlayerPrefs.SetInt(MASTER_VIBRATION_KEY, activity);
    }
    public static int GetMasterVibration()
    {
        return PlayerPrefs.GetInt(MASTER_VIBRATION_KEY);
    }
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

    // Score or whatever
    public static void SetMasterScore(int score)
    {
        PlayerPrefs.SetInt(MASTER_SCORE_KEY, score);
    }

    public static int GetMasterScore()
    {
        return PlayerPrefs.GetInt(MASTER_SCORE_KEY);
    }

}
