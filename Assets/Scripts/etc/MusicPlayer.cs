using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioSource ass;

    private int index = 0;

    private void Start()
    {
        ass = GetComponent<AudioSource>();
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
