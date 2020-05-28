using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    const string MASTER_MUSIC_KEY = "master_volume";
    const string MASTER_SCORE_KEY = "master_score";
    
    public static void ResetKeys()
    {
        PlayerPrefs.DeleteAll();
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
