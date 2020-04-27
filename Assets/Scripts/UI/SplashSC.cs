using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashSC : MonoBehaviour
{
    public float time;

    public void LoadingStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
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
