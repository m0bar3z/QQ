using UnityEngine;
using System.Collections;
using System;

public static class AndroidVibration
{

#if UNITY_ANDROID && !UNITY_EDITOR
    public static AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    public static AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    public static AndroidJavaObject vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#else
    public static AndroidJavaClass unityPlayer;
    public static AndroidJavaObject currentActivity;
    public static AndroidJavaObject vibrator;
#endif

    public static void Vibrate()
    {
        if (PlayerPrefsManager.VibrationIsActive)
        {
            if (isAndroid())
                vibrator.Call("vibrate");
            else
                Handheld.Vibrate();
        }
    }


    public static void Vibrate(long milliseconds)
    {
        if(PlayerPrefsManager.VibrationIsActive)
        {
            if (isAndroid())
            {
                try
                {
                    vibrator.Call("vibrate", milliseconds);
                }
                catch(System.Exception e) 
                {
                    OnScreenConsole.Print(e.ToString());
                }
            }
            else
                Handheld.Vibrate();
        }
    }

    /// <summary>
    /// takes 0.6 of second
    /// </summary>
    /// <param name="repeat">-1 means once and 0 means forever</param>
    public static void VibrateHeartBeat(int repeat)
    {
        long[] pattern = {100, 100, 100, 300};

        if (PlayerPrefsManager.VibrationIsActive)
        {
            if (isAndroid())
                vibrator.Call("vibrate", pattern, repeat);
            else
                Handheld.Vibrate();
        }
    }

    public static bool HasVibrator()
    {
        return isAndroid();
    }

    public static void Cancel()
    {
        if (isAndroid())
            vibrator.Call("cancel");
    }

    private static bool isAndroid()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
	return true;
#else
        return false;
#endif
    }
}