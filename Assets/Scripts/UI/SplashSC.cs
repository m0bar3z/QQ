using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashSC : MonoBehaviour
{
    public float time;
    public int startMenuIndex;
    public SceneManage sceneManager;
    public AndroidButton androidButton;

    public void LoadingStartMenu()
    {
        sceneManager.ActiveLoadingWindow(startMenuIndex);
    }

    private void Start()
    {
        androidButton = FindObjectOfType<BackToAndroid>();
        Invoke(nameof(LoadingStartMenu), time);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            androidButton.BackButton();
        }   
    }
}
