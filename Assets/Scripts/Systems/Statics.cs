using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics : MonoBehaviour
{
    public static Statics instance;

    public GameObject fireFX;
    public GameObject gameOverMenu;

    // Game Over Stuff
    public event SystemTools.SimpleSystemCB OnGameOver;
    public int score;

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    private void Awake()
    {
        instance = this;
    }
}
