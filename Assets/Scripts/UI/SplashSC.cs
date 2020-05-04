using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashSC : MonoBehaviour
{
    public float time;
    public int startMenuIndex;
    public SceneManage sceneManager;

    public void LoadingStartMenu()
    {
        sceneManager.ActiveLoadingWindow(startMenuIndex);
    }

    private void Start()
    {
        Invoke(nameof(LoadingStartMenu), time);
    }
}
