using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject menuItems;
    public SceneManage sceneManager;
    public Canvas canvas;

    public void Restart()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        sceneManager.ActiveLoadingWindow(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        Statics.instance.OnGameOver += GameOver;
        sceneManager = FindObjectOfType<SceneManage>();
        sceneManager.mainCanvas = canvas;
    }

    private void GameOver()
    {
        menuItems.SetActive(true);
    }
}
