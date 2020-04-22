using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coinFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            Statics.instance.StopResetPitch();
            Shop.coins++;
            Instantiate(coinFX, transform.position, Quaternion.identity).GetComponent<AudioSource>().pitch += Statics.pitchShift;
            Statics.pitchShift += 0.07f;
            Statics.instance.StartResetPitch();
            Destroy(transform.parent.gameObject);
        }
    }
}
