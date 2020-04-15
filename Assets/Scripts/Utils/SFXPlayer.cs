using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;

    private void Start()
    {
        source.PlayOneShot(
            clips[Random.Range(0, clips.Length)]    
        );
    }
}
