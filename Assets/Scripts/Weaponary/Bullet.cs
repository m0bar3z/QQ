using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : QQObject
{
    public GameObject bulletEffect, explosionFX;
    public int damage = 100;
    public float explosionChance = 0f;

    private static List<Bullet> bullets = new List<Bullet>();
    private static int bulletsLimit = 500;

    [Range(5, 100)]
    public float bulletSpeed = 10; // speed of the bullet

    public bool testShoot = false; // if on bullet flies on start
    public float destroyAfter = 5; // the bullet get's destroyed after this amount of time

    private Vector3 dir; // dir in which the bullet is shot

    public void Shoot(Vector3 dir)
    {
        this.dir = dir.normalized;
        Fly();
    }

    protected override void Start()
    {
        base.Start();

        TestShoot();
        CheckBulletLimit();
        Destroy(gameObject, destroyAfter);
    }

    private void CheckBulletLimit()
    {
        bullets.Add(this);
        if (bullets.Count > bulletsLimit)
        {
            Bullet b = bullets[0];
            bullets.RemoveAt(0);
            b.Destroy();
        }
    }

    private void Destroy()
    {
        try
        {
            Destroy(gameObject);
        }
        catch { }
    }

    private void Fly()
    {
        transform.up = dir;
        rb.velocity = dir * bulletSpeed;
    }

    private void TestShoot()
    {
        if (testShoot)
        {
            Shoot(transform.up);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 9)
        {
            if (Random.Range(0, 0.99f) < explosionChance)
            {
                Instantiate(explosionFX, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(bulletEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
