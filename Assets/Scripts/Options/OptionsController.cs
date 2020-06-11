using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsController : MonoBehaviour
{
    public IBackButton backToStartMenu;
    public SceneManage sceneManager;
    public Canvas canvas;
    public int startMenuIndex;

    public void LoadStartMenuScene()
    {
        sceneManager.ActiveLoadingWindow(startMenuIndex);
    }
   
    void Start()
    {
        sceneManager = FindObjectOfType<SceneManage>();
        sceneManager.mainCanvas = canvas;
        backToStartMenu = gameObject.AddComponent<BackToSM>();
    }

}
