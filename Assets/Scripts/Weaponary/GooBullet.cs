using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Vibration.Vibrate(100);
        Statics.instance.publicAS.PlayOneShot(hitSFX);

        try
        {
            collision.GetComponent<Enemy>().Hurt(damage);
        }
        catch { }

        transform.parent = collision.transform;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;

        Invoke(nameof(BlowUp), 3);
    }
}
