using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel, player;

    public void GamePaused()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        player.SetActive(false);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        player.SetActive(true);
    }
    
    public void Exit()
    {
        player.SetActive(true);
        Application.Quit();
    }
    void Start()
    {
        pausePanel.SetActive(false);
    }



    void Update()
    {
        
    }
}
