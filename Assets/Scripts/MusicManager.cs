using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float MaxVolume;
    public AudioSource audioSource;

    public void SetVolume()
    {
        audioSource.volume = PlayerPrefsManager.GetMasterMusic() * MaxVolume;
    }
    private void OnLevelWasLoaded(int level)
    {
        audioSource = FindObjectOfType<AudioSource>();
        SetVolume();
        print("testtesttsetset  " + PlayerPrefsManager.GetMasterMusic());
    }
}
