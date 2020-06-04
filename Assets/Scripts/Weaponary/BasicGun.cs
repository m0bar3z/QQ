using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasicGun : QQObject
{
    public static float defCamSize = 8;

    [Header("Gun vars")]
    [Space(20)]

    [Header("Floats")]
    [Range(0, 5)]
    public float betweenBullets = 0.2f;
    [Range(0.01f, 30f)]
    public float recoil = 0.5f;
    [Range(0f, 0.99f)]
    public float explosionChanceOfBullets = 0f;
    public float reloadTime = 2;
    public float shakeStrength, recoilStrength = 1;

    [Header("Ints")]
    [Range(1, 20)]
    public int chunkSize = 3;
    public int capacity = 7;
    public int shakeVibrato;
    public int vibrationDuration = 100;
    public int scope = 1;
    public int hitBeforeDes = 1;

    [Header("GO, Transforms")]
    public GameObject bulletPref;
    public GameObject shellPref;
    public Transform gunHole;

    [Header("Bool")]
    public bool plyReloadSFX = true;
    public bool dirRecoil = false, shake = false, halfVibration;

    [Header("Audio")]
    public AudioClip reloadSFX;
    public AudioClip[] shootingSFX;
    public AudioSource audioSource;
    public SFXManager sfxManager;

    [Space(20)]

    private float time = 0;

    private int mag = 0;


    private bool waiting = false, reloading = false;
    private bool vibrate = true;
    private bool scopeSet = false;

    public bool Shaking
    {
        get
        {
            if (shake && PlayerPrefsManager.CameraShakeIsActive)
                return true;
            else
                return false;
        }
    }

    public void SetVolume()
    {
        try
        {
            sfxManager = FindObjectOfType<SFXManager>();
            sfxManager.audioSource = audioSource;
            sfxManager.maxVolume = audioSource.volume;
            sfxManager.SetSFXVoiume();
        }
        catch { }
    }

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
            b.contactBeforeDestruction = hitBeforeDes;
            b.Shoot(tempDir, dirRecoil, recoilStrength);
        }

        mag--;
        CheckForReload();

        if (Shaking)
        {
            Camera.main.DOShakePosition(betweenBullets / 2, shakeStrength, shakeVibrato);
            Camera.main.DOShakeRotation(betweenBullets / 2, shakeStrength * 4, shakeVibrato);

            if (halfVibration)
            {
                if (vibrate)
                {
                    vibrate = false;
                    Vibration.Vibrate(vibrationDuration);
                }
                else
                {
                    vibrate = true;
                }
            }
            else
            {
                Vibration.Vibrate(vibrationDuration);
            }
        }

        Instantiate(shellPref, transform.position, Quaternion.identity).GetComponent<Shell>().Fly(-dir);

        if (playerHeld)
            SetCount();

        PlayShootingSFX();
        holderController.ReceiveForce(-dir * recoil * 10); // recoil
    }

    protected virtual void SetCount()
    {
        try
        {
            Statics.instance.bulletCounter.SetNumber(mag);
        }
        catch
        {
            print("Can't find bullet counter");
        }
    }

    protected override void Start()
    {
        base.Start();
        ResetMagToFull();
        SetVolume();
        SetScope();
    }

    private void SetScope()
    {
        if (playerHeld)
        {
            scopeSet = true;

            if(scope > 0)
                CamManager.SetSize(CamManager.GetDefSize() * scope);
        }
    }

    private void PlayShootingSFX()
    {
        audioSource.pitch = 1 + Random.Range(-0.2f, 0.2f);
        audioSource.PlayOneShot(shootingSFX[
            Random.Range(0, shootingSFX.Length)
        ]);
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

    private void DisableReloadFX()
    {
        Statics.instance.reloadFX.SetActive(false);
    }

    private void Reload()
    {
        if (playerHeld)
        {
            try
            {
                Statics.instance.reloadFX.SetActive(true);
                Invoke(nameof(DisableReloadFX), reloadTime);
            }
            catch { }
        }

        transform.DOPunchRotation(new Vector3(0, 0, 270), reloadTime, vibrato: 0).OnComplete(
            () =>
            {
                ResetMagToFull();
                reloading = false;
            }
        );

        if (plyReloadSFX)
        {
            audioSource.pitch = 1 + Random.Range(-0.2f, 0.2f);
            audioSource.PlayOneShot(reloadSFX);
        }
    }

    private void ResetMagToFull()
    {
        mag = capacity;

        if (playerHeld)
            SetCount();
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
