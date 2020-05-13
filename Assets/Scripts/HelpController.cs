using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour
{
    public Canvas canvas;
    public SceneManage sceneManager;
    public IBackButton backToMenuAction;

    public int startMenuIndex;

    public void BackButtonFunction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            backToMenuAction.BackButton();
        }
    }

    public void LoadStartMenuScene()
    {
        sceneManager.ActiveLoadingWindow(startMenuIndex);
    }
    void Start()
    {
        backToMenuAction = gameObject.GetComponent<BackToStartMenu>();
        sceneManager = FindObjectOfType<SceneManage>();
        sceneManager.mainCanvas = canvas;
    }

    private void Update()
    {
        BackButtonFunction();
    }
}
