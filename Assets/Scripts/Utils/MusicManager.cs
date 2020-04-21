using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager instance;
    
    public AudioClip[] musics;
    public AudioSource audioSource;

    public void PlaySceneMusic(int index)
    {
        audioSource.clip = musics[index];
        audioSource.Play();
    }

    public void ChnageVolume(float vol)
    {
        audioSource.volume = vol;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level != 3)
        {
            PlaySceneMusic(level);
        }

    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsManager.GetMasterVolume();
    }
    void Update()
    {
        
    }
}
