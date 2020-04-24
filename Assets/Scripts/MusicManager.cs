using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    static MusicManager instance;

    public GameObject music;
    public AudioSource audioSource;
    public AudioSource[] audioSourcesOnScene;

    public void SetMusicVolume()
    {
        music.GetComponent<AudioSource>().volume = PlayerPrefsManager.GetMasterVolume();
    }
    private void FindAuidoSourcesOnScene()
    {
        audioSourcesOnScene = FindObjectsOfType<AudioSource>();
        foreach (AudioSource Asrc in audioSourcesOnScene)
        {
            if(Asrc.volume == 1)
            {
                Asrc.volume = audioSource.volume;
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        //FindAuidoSourcesOnScene();
        music = GameObject.FindGameObjectWithTag("music");
        SetMusicVolume();
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
