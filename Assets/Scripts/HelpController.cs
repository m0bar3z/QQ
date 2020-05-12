using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour
{
    public Canvas canvas;
    public SceneManage sceneManager;

    public int startMenuIndex;

    public void LoadStartMenuScene()
    {
        sceneManager.ActiveLoadingWindow(startMenuIndex);
    }
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManage>();
        sceneManager.mainCanvas = canvas;
    }
}
