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
    [Range(0f, 0.99f)]
    public float explosionChanceOfBullets = 0f;
    public float reloadTime = 2;
    [Range(1, 20)]
    public int chunkSize = 3;
    public int capacity = 7;
    public GameObject bulletPref;
    public Transform gunHole;
    public AudioClip reloadSFX;
    public AudioClip[] shootingSFX;
    public AudioSource audioSource;
    public bool dirRecoil = false;

    private float time = 0;
    private bool waiting = false, reloading = false;
    private int mag = 0;

    public override void Trigger(Vector3 dir)
    {
        if (waiting || reloading) return;

        int recoilDir = holderController.facingRight ? 1 : -1;
        transform.DOLocalMove(new Vector3(-dir.x * recoilDir, -dir.y, dir.z)/10, betweenBullets / 3).SetLoops(2, LoopType.Yoyo);

        for (int i = 0; i < chunkSize; i++)
        {
            waiting = true;

            dir.z = 0;
            dir = dir.normalized;

            transform.right = holderController.facingRight ? dir : -dir;

            Vector3 tempDir = dir;
            if (chunkSize > 1)
                tempDir = Quaternion.Euler(0, 0, (i - chunkSize/2) * 5) * dir;

            Bullet b = Instantiate(bulletPref, gunHole.position, Quaternion.identity).GetComponent<Bullet>();
            b.explosionChance = explosionChanceOfBullets;
            b.Shoot(tempDir, dirRecoil);
        }

        mag--;
        CheckForReload();

        PlayShootingSFX();
        holderController.ReceiveForce(-dir * recoil * 10); // recoil
    }

    private void PlayShootingSFX()
    {
        audioSource.PlayOneShot(shootingSFX[
            Random.Range(0, shootingSFX.Length)
        ]);
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

    protected override void Update()
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
