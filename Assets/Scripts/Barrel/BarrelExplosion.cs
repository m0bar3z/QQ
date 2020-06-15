using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosion : MonoBehaviour
{
    public GameObject explosion;
    public BoxCollider2D boxCollider;
    public float explosionRadius;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        boxCollider.edgeRadius = explosionRadius;
        Destroy(gameObject);
    }
}
