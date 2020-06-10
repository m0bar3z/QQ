using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shell : MonoBehaviour
{
    public Rigidbody2D rb;
    public AudioClip shellDropSFX;
    public float speed = 5, playSFXAfter = 0.3f, duration = 0.5f;

    public void Fly(Vector2 vl)
    {
        vl += new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        rb.velocity = vl.normalized * speed;

        Vector3 scl = transform.localScale;
        transform.DOScale(scl * 2, duration / 4).OnComplete(
            () =>
            {
                transform.DOScale(scl, 3 * duration / 2).OnComplete(
                    () =>
                    {
                        rb.velocity = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)).normalized * speed / 10;
                    }    
                );
            }    
        );
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
