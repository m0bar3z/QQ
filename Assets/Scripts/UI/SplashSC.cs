using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SplashSC : MonoBehaviour
{
    public float time;
    void Start()
    {
        Invoke(nameof(LoadingStartMenu), time);
    }

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

}
