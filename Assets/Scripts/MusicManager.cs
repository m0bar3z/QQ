using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager instance;

    public AudioSource audioSource;
    public AudioSource[] audioSourcesOnScene;

    private void FindAuidoSourcesOnScene()
    {
        audioSourcesOnScene = FindObjectsOfType<AudioSource>();
        foreach (AudioSource Asrc in audioSourcesOnScene)
        {
            Asrc.volume = audioSource.volume;
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        FindAuidoSourcesOnScene();
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
