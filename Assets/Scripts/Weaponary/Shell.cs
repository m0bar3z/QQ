using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public Rigidbody2D rb;
    public AudioClip shellDropSFX;
    public float speed = 5, playSFXAfter = 0.3f;

    public void Fly(Vector2 vl)
    {
        rb.velocity = vl.normalized * speed;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
