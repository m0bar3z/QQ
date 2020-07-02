using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class AndroidVibrationMock : MonoBehaviour
{
    static AndroidJavaObject vibrator;

    public static void Initialize()
    {
        // Get instance of Vibrator from current Context
        // Vibrator v = (Vibrator)getSystemService(Context.VIBRATOR_SERVICE);

        vibrator = GetSystemService("VIBRATOR_SERVICE", "Vibrator");
    }

    public static void Vibrate(int ms)
    {
        // Vibrate for 400 milliseconds
        // VibrationEffect effect = VibrationEffect.createOneShot(ms, VibrationEffect.DEFAULT_AMPLITUDE)
        // ((Vibrator)getSystemService(VIBRATOR_SERVICE)).vibrate(effect);

        AndroidJavaClass ve = new AndroidJavaClass("android.os.VibrationEffect");
        AndroidJavaObject effect = ve.CallStatic<AndroidJavaObject>("createOneShot", ms, ve.GetStatic<int>("EFFECT_CLICK"));
        vibrator.Call("vibrate", effect);
    }

    private static AndroidJavaObject Cast(AndroidJavaObject source, string destClass)
    {
        AndroidJavaClass classClass = new AndroidJavaClass("java.lang.Class");

        AndroidJavaObject classCaster = classClass.CallStatic<AndroidJavaObject>("forName", destClass);

        return classCaster.Call<AndroidJavaObject>("cast", source);
    }

    private static AndroidJavaObject GetSystemService(string name, string serviceClass)
    {
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject serviceObj = curActivity.Call<AndroidJavaObject>("getSystemService", name);

            return Cast(serviceObj, serviceClass);
        }
        catch
        {
            Debug.LogWarning("Failed to get " + name);
            return null;
        }
    }
}
