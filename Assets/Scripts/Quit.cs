using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Quit : MonoBehaviour
{
    [SerializeField]
    private Button QuitButton;
    public void ExitGame()
    {
        Application.Quit();
    }
    private void OnLevelWasLoaded(int level)
    {
     if (level == 1)
        {
            QuitButton = GameObject.FindGameObjectWithTag("quitButton").GetComponent<Button>();
            QuitButton.onClick.AddListener(() => { ExitGame(); });
        }   
    }
}
