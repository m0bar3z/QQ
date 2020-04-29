using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashSC : MonoBehaviour
{
    public float time;
    public LoadingBar loadingBar;
    public string levelName;

    public void LoadingStartMenu()
    {
        loadingBar.StartLoading("StartMenu");
    }

    public void LoadingNextScene(string lvlName)
    {
        loadingBar.StartLoading(lvlName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Start()
    {
        Invoke(nameof(LoadingStartMenu), time);
    }
}
