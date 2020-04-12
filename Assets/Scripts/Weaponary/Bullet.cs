using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private static List<Bullet> bullets = new List<Bullet>();
    private static int bulletsLimit = 500;

    [Range(5, 100)]
    public float bulletSpeed = 10; // speed of the bullet

    public bool testShoot = false; // if on bullet flies on start
    public float destroyAfter = 5; // the bullet get's destroyed after this amount of time
    public Rigidbody2D rb; // rigid body of bullet

    private Vector3 dir; // dir in which the bullet is shot

    public void Shoot(Vector3 dir)
    {
        this.dir = dir.normalized;
        Fly();
    }

    private void Start()
    {
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
        Destroy(gameObject);
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

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
