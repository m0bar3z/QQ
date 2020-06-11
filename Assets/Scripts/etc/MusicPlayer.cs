using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioSource ass;
    public VolumeManager musicManager;

    private int index = 0;

    private void Awake()
    {
        musicManager = FindObjectOfType<VolumeManager>();
    }
    private void Start()
    {
        ass = GetComponent<AudioSource>();
        musicManager.audioSource = ass;
        musicManager.SetVolume();
    }

    private void Update()
    {
        if (!ass.isPlaying)
            PlayNext();
    }

    private void PlayNext()
    {
        ass.PlayOneShot(clips[index++ % clips.Count]);
    }
}
