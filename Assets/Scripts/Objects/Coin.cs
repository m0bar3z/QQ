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
            Shop.coins++;
            Instantiate(coinFX, transform.position, Quaternion.identity);
            Destroy(transform.parent.gameObject);
        }
    }
}
