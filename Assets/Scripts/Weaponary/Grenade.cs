using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Bullet
{
    [Space(20)]
    [Header("Bazuka Bullet")]
    public float blowUpAfter, explosionRange = 1, betweenExplosions = 0.3f;
    [Range(1, 20)]
    public int explosionChunck = 4;

    protected override void Start()
    {
        base.Start();

        Invoke(nameof(BlowUp), blowUpAfter);
    }

    protected override void BlowUp()
    {
        StartCoroutine(ExplosionSpawn());
    }

    protected virtual IEnumerator ExplosionSpawn()
    {
        for (int i = 0; i < explosionChunck; i++)
        {
            Instantiate(explosionFX, transform.position + new Vector3(Random.Range(-.5f, .5f), Random.Range(-.5f, .5f), 0).normalized * explosionRange, Quaternion.identity);
            yield return new WaitForSeconds(betweenExplosions);
        }

        Destroy(gameObject);
    }
}
