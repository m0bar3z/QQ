using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statics : MonoBehaviour
{
    public static Statics instance;

    public GameObject fireFX;
    public GameObject gameOverMenu;

    public Slider healthSlider;

    private bool healthSliderSet = false;

    // Game Over Stuff
    public event SystemTools.SimpleSystemCB OnGameOver;
    public int score;

    public void GameOver()
    {
        OnGameOver?.Invoke();
    }

    public void SetHealth(float value)
    {
        if (healthSliderSet)
        {
            healthSlider.value = value;
        }
    }

    private void Awake()
    {
        instance = this;
        healthSliderSet = healthSlider != null;
    }
}
