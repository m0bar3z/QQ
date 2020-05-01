using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashSC : MonoBehaviour
{
    public float time;
    public SceneManage sceneManager;

    public void LoadingStartMenu()
    {
        sceneManager.LoadNext(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        Invoke(nameof(LoadingStartMenu), time);
    }
}
