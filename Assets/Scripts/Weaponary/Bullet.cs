using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : QQObject
{
    private static List<Bullet> bullets = new List<Bullet>();
    private static int bulletsLimit = 500;

    public Enemy[] enemies;

    [Space(20)]
    [Header("Bullet Vars")]
    public GameObject bulletEffect, explosionFX;
    public int damage = 100, contactBeforeDestruction = 2;
    public float explosionChance = 0f;

    [Range(5, 100)]
    public float bulletSpeed = 10; // speed of the bullet
    [Range(1, 10)]
    public float chasingSpeed; // speed of the bullet when chasing is active

    public bool testShoot = false; // if on bullet flies on start
    public bool destroyOnTouch = true;
    public bool chasingEnemy;
    public float destroyAfter = 5; // the bullet get's destroyed after this amount of time

    private Vector3 dir; // dir in which the bullet is shot

    public void BulletAccelerate()
    {
        chasingSpeed += 0.5f;
    }
    public void ChasingEnemy()
    {
        enemies = FindObjectsOfType<Enemy>();
        Vector2 shortestDistanceToEnemy = new Vector2(1000, 1000);

        for (int i = 0; i < enemies.Length; i++)
        {
            Vector2 enemyDistance = enemies[i].transform.position - transform.position;
            if (enemyDistance.magnitude < shortestDistanceToEnemy.magnitude)
            {
                shortestDistanceToEnemy = enemyDistance;
            }
        }
        dir = shortestDistanceToEnemy.normalized * chasingSpeed;
    }

    public void Shoot(Vector3 dir, bool withRecoil = false, float recoilStrength = 1)
    {
        if (!chasingEnemy)
        {
            this.dir = dir.normalized;
            Fly(withRecoil, recoilStrength);
        }
        else
        {
            ChasingEnemy();
            if(dir.normalized.x > 0 && this.dir.normalized.x > 0 || dir.normalized.x < 0 && this.dir.normalized.x < 0)
            {
                Fly(withRecoil, recoilStrength);
            }
            else
            {
                this.dir = dir.normalized;
                //InvokeRepeating(nameof(BulletAccelerate), 0.1f, 0.2f);
                Fly(withRecoil, recoilStrength);
            }
        }
    }

    protected override void Start()
    {
        base.Start();

        CheckBulletLimit();
        Destroy(gameObject, destroyAfter);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            Vibration.Vibrate(100);
            contactBeforeDestruction--;
            if(contactBeforeDestruction > 0)
            {
                return;
            }
        }

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

    private void Fly(bool withRecoil, float recoilStrength = 1)
    {
        transform.up = dir;
        Vector2 recoil_ = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)) * bulletSpeed / 5;
        rb.velocity = dir * bulletSpeed + (Vector3)recoil_ * recoilStrength;
    }
}
