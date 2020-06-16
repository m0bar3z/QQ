using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BarrelExplosion : MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosionFire;
    public BoxCollider2D boxCollider;
    public CamManager camManager;
    public float explosionRadius;
    public int maximumFlames;
    public float shakingDuration, shakingStrngth;

    public void RandomDestroyTime()
    {

    }
    public void ExplosionEffect()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);

        if (Shaking)
            Camera.main.DOShakeRotation(shakingDuration, shakingStrngth, 10);

        camManager.ResetCameraRotation(shakingDuration);
    }
    public void AfterExplosion()
    {
        for (int i = 1; i < maximumFlames; i++)
        {
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-explosionRadius, explosionRadius),
                                                                                                                     transform.position.y + Random.Range(-explosionRadius, explosionRadius), transform.position.z);
            Instantiate(explosionFire, pos, Quaternion.identity);

        }
    }
    bool Shaking
    {
        get
        {
            if (PlayerPrefsManager.CameraShakeIsActive)
                return true;
            else
                return false;
        }
    }
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        camManager = FindObjectOfType<CamManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ExplosionEffect();
        AfterExplosion();
        boxCollider.edgeRadius = explosionRadius;
        Destroy(gameObject);
    }
}
