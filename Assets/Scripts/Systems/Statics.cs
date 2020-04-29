using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statics : MonoBehaviour
{
    public static Statics instance;

    // coin pitch shift
    public static float pitchShift = 0;
    public static float resetIn = 1f;

    public GameObject fireFX;

    public AudioSource publicAS;

    public Slider healthSlider;

    public BulletCounter bulletCounter;

    public Shop shop;

    public Image ReloadBar;

    private bool healthSliderSet = false, inGlitch = false;

    // Game Over Stuff
    public event SystemTools.SimpleSystemCB OnGameOver;
    public int score;

    /// 
    public void SetScore()
    {
        PlayerPrefsManager.SetMasterScore(shop.coins);
    }

    public int GetScore()
    {
        return PlayerPrefsManager.GetMasterScore();
    }

    public void GlitchForS(float duration)
    {
        if(!inGlitch)
            StartCoroutine(GlitchCoroutine(duration));
    }

    public void StartResetPitch()
    {
        Invoke(nameof(ResetPitch), resetIn);
    }
    
    public void StopResetPitch()
    {
        CancelInvoke(nameof(ResetPitch));
    }

    public void ResetPitch()
    {
        pitchShift = 0;
    }

    public IEnumerator GlitchCoroutine(float duration)
    {
        inGlitch = true;
        float ts = Time.timeScale;
        //float fts = Time.fixedDeltaTime;
        Time.timeScale = 0;
        //Time.fixedDeltaTime = 0;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = ts;
        //Time.fixedDeltaTime = fts;
        inGlitch = false;
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
