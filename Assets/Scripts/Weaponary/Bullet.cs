using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : QQObject
{
    [Space(20)]
    [Header("Bullet Vars")]
    public GameObject bulletEffect, explosionFX;
    public int damage = 100;
    public float explosionChance = 0f;

    private static List<Bullet> bullets = new List<Bullet>();
    private static int bulletsLimit = 500;

    [Range(5, 100)]
    public float bulletSpeed = 10; // speed of the bullet

    public bool testShoot = false; // if on bullet flies on start
    public bool destroyOnTouch = true;
    public float destroyAfter = 5; // the bullet get's destroyed after this amount of time

    private Vector3 dir; // dir in which the bullet is shot

    public void Shoot(Vector3 dir, bool withRecoil = false)
    {
        this.dir = dir.normalized;
        Fly(withRecoil);
    }

    protected override void Start()
    {
        base.Start();

        CheckBulletLimit();
        Destroy(gameObject, destroyAfter);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if(explosionChance > 0)
            BlowUp();

        if (destroyOnTouch)
            Destroy(gameObject);
    }

    protected virtual void BlowUp()
    {
        if (Random.Range(0, 0.99f) < explosionChance)
        {
            Instantiate(explosionFX, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(bulletEffect, transform.position, Quaternion.identity);
        }        
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

    private void Fly(bool withRecoil)
    {
        transform.up = dir;
        Vector2 recoil_ = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)) * bulletSpeed / 5;
        rb.velocity = dir * bulletSpeed + (Vector3)recoil_;
    }
}
