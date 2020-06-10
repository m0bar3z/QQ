using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SMController : MonoBehaviour
{
    public Canvas canvas;
    public GameObject music, musicParent;
    public VolumeManager musicManager;
    public int gameSceneIndex;
    public int helpSceneIndex;
    public int optionSceneIndex;
    public IBackButton quitAction;
    //public int shopSceneIndex;

    public SceneManage scenesManager;

    public void SetAudioSource()
    {
        musicManager = FindObjectOfType<VolumeManager>();
        musicManager.audioSource = music.GetComponent<AudioSource>();
    }
    public void AddParentToMusic()
    {
        music.transform.parent = musicParent.transform;
       // Destroy(MusicParent);
    }
    public void BackButtonFunction()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitAction.BackButton();
        }
    }

    public void LoadOptionScene()
    {
        scenesManager.ActiveLoadingWindow(optionSceneIndex);
    }
    public void LoadHelpScene()
    {
        scenesManager.ActiveLoadingWindow(helpSceneIndex);
    }
    public void LoadGameScene()
    {
        AddParentToMusic();
        scenesManager.ActiveLoadingWindow(gameSceneIndex);
    }

    private void Update()
    {
        BackButtonFunction();
    }
    void Start()
    {
        scenesManager = FindObjectOfType<SceneManage>();
        scenesManager.mainCanvas = canvas;
        music = FindObjectOfType<DontDestroyMenuMusic>().gameObject;
        quitAction = gameObject.GetComponent<BackToAndroid>();
    }
}
