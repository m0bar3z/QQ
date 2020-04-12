using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicGun : QQObject
{
    [Range(0, 5)]
    public float betweenBullets = 0.2f;
    [Range(0.01f, 30f)]
    public float recoil = 0.5f;
    public float reloadTime = 2;
    [Range(1, 20)]
    public int chunkSize = 3;
    public int capacity = 7;
    public GameObject bulletPref;
    public Transform gunHole;
    public AudioClip shootSFX, reloadSFX;
    public AudioSource audioSource;

    // TODO : change player controller to controller after adding enemies
    public Transform holder;
    public Person holderController;

    private float time = 0;
    private bool waiting = false, reloading = false;
    private int mag = 0;

    public void Shoot()
    {
        if (waiting || reloading) return;

        Vector3 dir = Vector3.zero;

        for (int i = 0; i < chunkSize; i++)
        {
            waiting = true;

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir = pos - gunHole.position;
            dir.z = 0;
            dir = dir.normalized;

            transform.right = holderController.facingRight?-dir:dir;

            pos.z = 0;
            Instantiate(bulletPref, gunHole.position, Quaternion.identity).GetComponent<Bullet>().Shoot(dir);
        }

        mag--;
        CheckForReload();

        audioSource.PlayOneShot(shootSFX);
        holderController.ReceiveForce(-dir * recoil * 10);
    }

    protected override void Start()
    {
        base.Start();

        // set mag to full capacity
        ResetMagToFull();
    }

    private void CheckForReload()
    {
        if (mag <= 0)
        {
            mag = 0;
            reloading = true;
            Reload();
        }
    }

    private void Reload()
    {
        transform.DOPunchRotation(new Vector3(0, 0, 270), reloadTime, vibrato: 0).OnComplete(
            () =>
            {
                ResetMagToFull();
                reloading = false;
            }
        );
        audioSource.PlayOneShot(reloadSFX);
    }

    private void ResetMagToFull()
    {
        mag = capacity;
    }

    private void Update()
    {
        TimerTick();
    }

    private void TimerTick()
    {
        if (waiting)
        {
            time += Time.deltaTime;
            if (time > betweenBullets)
            {
                time = 0;
                waiting = false;
            }
        }
    }
}
