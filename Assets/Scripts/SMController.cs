using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SMController : MonoBehaviour
{
    public Canvas canvas;
    public int gameSceneIndex;
    //public int optionSceneIndex;
    //public int shopSceneIndex;

    public SceneManage scenesManager;
    
    public void LoadGameScene()
    {
        scenesManager.ActiveLoadingWindow(gameSceneIndex);
    }
    void Start()
    {
        scenesManager = FindObjectOfType<SceneManage>();
        scenesManager.mainCanvas = canvas;
    }
}
