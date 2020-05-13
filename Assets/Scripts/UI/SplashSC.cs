using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashSC : MonoBehaviour
{
    public float time;
    public int startMenuIndex;
    public SceneManage sceneManager;
    public IBackButton quitAction;

    public void BackButtonFunction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitAction.BackButton();
        }
    }

    public void LoadingStartMenu()
    {
        sceneManager.ActiveLoadingWindow(startMenuIndex);
    }

    private void Start()
    {
        quitAction = gameObject.GetComponent<BackToAndroid>();
        Invoke(nameof(LoadingStartMenu), time);
    }
    private void Update()
    {
        BackButtonFunction();
    }
}
