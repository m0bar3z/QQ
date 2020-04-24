using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    const string MASTER_VOLUME_KEY = "master_volume";
    const string MASTER_SCORE_KEY = "master_score";

    public static void SetMasterVolume(float volume)
    {
        if(volume >= 0 && volume <= 1f)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
    }

    public static void SetMasterScore(int score)
    {
        PlayerPrefs.SetInt(MASTER_SCORE_KEY, score);
    }

    public static int GetMasterScore()
    {
        return PlayerPrefs.GetInt(MASTER_SCORE_KEY);
    }
    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

}
