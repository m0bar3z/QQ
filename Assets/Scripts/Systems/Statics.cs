using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statics : MonoBehaviour
{
    public static Statics instance;

    public GameObject fireFX;

    public Slider healthSlider;

    private bool healthSliderSet = false;

    // Game Over Stuff
    public event SystemTools.SimpleSystemCB OnGameOver;
    public int score;

    public void GlitchForS(float duration)
    {
        StartCoroutine(GlitchCoroutine(duration));
    }

    public IEnumerator GlitchCoroutine(float duration)
    {
        float ts = Time.timeScale;
        //float fts = Time.fixedDeltaTime;
        Time.timeScale = 0;
        //Time.fixedDeltaTime = 0;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = ts;
        //Time.fixedDeltaTime = fts;
    }

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
