using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Rigidbody2D rb;
    public AudioClip shellDropSFX;
    public float speed = 5, playSFXAfter = 0.3f;

    public void Fly()
    {
        rb.velocity = new Vector2(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f)).normalized * speed;
    }

    private void Start()
    {
        Fly();
    }
}
