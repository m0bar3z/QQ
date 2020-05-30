using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioSource : MonoBehaviour
{
    public MusicManager musicManager;

    private void OnLevelWasLoaded(int level)
    {
        musicManager.audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }
}
