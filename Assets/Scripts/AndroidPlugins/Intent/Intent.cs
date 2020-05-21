using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intent
{
    static AndroidJavaObject intent;
    static AndroidJavaClass Uri;
    static AndroidJavaClass unityPlayer;
    static AndroidJavaObject curActivity;

    static void Initialize()
    {
        // android code: Intent intent = new Intent(Intent.ACTION_EDIT);
        intent = new AndroidJavaObject("android.content.Intent");
        Uri = new AndroidJavaClass("android.net.Uri");

        unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
    }

    public static void IntentReview()
    {
        if (Application.platform != RuntimePlatform.Android)
            return;

#if UNITY_ANDROID

        // android code: intent.setData(Uri.parse("bazaar://details?id=" + "PACKAGE_NAME"));
        intent.Call("setData", 
            Uri.CallStatic<AndroidJavaObject>("parse", "bazaar://details?id=com.beraria.qq")
        );

        // android code: intent.setPackage("com.farsitel.bazaar");
        intent.Call("setPackage", "com.farsitel.bazaar");

        // android code: startActivity(intent);
        curActivity.Call("startActivity", intent);

#endif
    }
}
