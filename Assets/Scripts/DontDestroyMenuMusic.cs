using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DontDestroyMenuMusic : MonoBehaviour
{
    //public AudioSource audioSource;
    static DontDestroyMenuMusic instance;

    void Singleton()
    {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            else if (SceneManager.GetActiveScene().name != "Game")
             {
                   instance = this;
                    DontDestroyOnLoad(this.gameObject);
               }
    }
    void Start()
    {
        Singleton();
        DontDestroyOnLoad(this.gameObject);
        //audioSource = gameObject.GetComponent<AudioSource>();
        //audioSource.volume = PlayerPrefsManager.GetMasterMusic();
    }

}
