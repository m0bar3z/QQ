using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public AudioSource source;
    public AudioClip[] clips;
    public SFXManager sfxManager;

    private void Start()
    {
        gameObject.tag = "SFX";
        source.PlayOneShot(
            clips[Random.Range(0, clips.Length)]    
        );
    }
}
