using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SMController : MonoBehaviour
{
    public Canvas canvas;
    public int gameSceneIndex;
    public int helpSceneIndex;
    public IBackButton quitAction;
    //public int optionSceneIndex;
    //public int shopSceneIndex;

    public SceneManage scenesManager;

    public void BackButtonFunction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitAction.BackButton();
        }
    }
     
    public void LoadHelpScene()
    {
        scenesManager.ActiveLoadingWindow(helpSceneIndex);
    }
    public void LoadGameScene()
    {
        scenesManager.ActiveLoadingWindow(gameSceneIndex);
    }

    private void Update()
    {
        BackButtonFunction();
    }

    void Start()
    {
        quitAction = gameObject.GetComponent<BackToAndroid>();
        scenesManager = FindObjectOfType<SceneManage>();
        scenesManager.mainCanvas = canvas;
    }
}
