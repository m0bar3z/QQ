using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : QQObject
{
    private static List<Bullet> bullets = new List<Bullet>();
    private static int bulletsLimit = 500;

    [Space(20)]
    [Header("Bullet Vars")]
    public GameObject bulletEffect, explosionFX;
    public int damage = 100, contactBeforeDestruction = 2;
    public float explosionChance = 0f;

    [Range(5, 100)]
    public float bulletSpeed = 10; // speed of the bullet

    public bool testShoot = false; // if on bullet flies on start
    public bool destroyOnTouch = true;
    public float destroyAfter = 5; // the bullet get's destroyed after this amount of time

    private Vector3 dir; // dir in which the bullet is shot

    public void Shoot(Vector3 dir, bool withRecoil = false, float recoilStrength = 1)
    {
        this.dir = dir.normalized;
        Fly(withRecoil, recoilStrength);
    }

    public void Blow()
    {
        Instantiate(bulletEffect, transform.position, Quaternion.identity);

        if (destroyOnTouch)
            Destroy(gameObject);
    }

    protected override void Start()
    {
        base.Start();

        CheckBulletLimit();
        Destroy(gameObject, destroyAfter);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            Vibration.Vibrate(100);

            try
            {
                collision.GetComponent<Enemy>().Hurt(damage);
            }
            catch { }

            contactBeforeDestruction--;
            if (contactBeforeDestruction > 0)
            {
                return;
            }
        }

        if (collision.gameObject.layer == 12 || collision.gameObject.layer == 16 || collision.gameObject.layer == 10 || collision.gameObject.layer == 11)
            return;

        if (explosionChance > 0)
            BlowUp();

        Blow();
    }

    protected virtual void BlowUp()
    {
        if (Random.Range(0, 0.99f) < explosionChance)
        {
            Instantiate(explosionFX, transform.position, Quaternion.identity);
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

    private void Fly(bool withRecoil, float recoilStrength = 1)
    {
        transform.up = dir;
        Vector2 recoil_ = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)) * bulletSpeed / 5;
        rb.velocity = dir * bulletSpeed + (Vector3)recoil_ * recoilStrength;
    }
}
