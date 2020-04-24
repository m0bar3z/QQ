using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 13)
        {
            collision.GetComponent<Enemy>().Hurt(100, transform.position - collision.transform.position);
        }
        else if(collision.gameObject.layer == 10)
        {
            collision.GetComponent<Blood>().Burn();
        }
    }
}
