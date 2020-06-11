using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DontDestroyMenuMusic : MonoBehaviour
{
    public VolumeManager musicManager;
    static DontDestroyMenuMusic instance;

    void Singleton()
    {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            else
             {
                   instance = this;
                    DontDestroyOnLoad(this.gameObject);
             }
    }
    void Start()
    {
        Singleton();
        DontDestroyOnLoad(this.gameObject);
        musicManager = FindObjectOfType<VolumeManager>();
        musicManager.audioSource = GetComponent<AudioSource>();
        musicManager.SetVolume();
    }
}
