using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpreader : MonoBehaviour
{
    public BloodPL pool;
    public float betweenSpawns = 0.2f, destroyAfter;
    public Rigidbody2D rb;

    private float time;

    private void Start()
    {
        Destroy(gameObject, destroyAfter);

        Shoot();

        pool = BloodPL.instance;
    }

    private void Shoot()
    {
        Vector2 rnd = new Vector2(
                    Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f)
                );

        rb.velocity = rnd.normalized * 2;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time > betweenSpawns)
        {
            time = 0;
            betweenSpawns *= 1.5f;
            Spawn();
        }
    }

    private void Spawn()
    {
        Blood b = pool.GetOne();
        b.transform.position = transform.position;
    }
}
