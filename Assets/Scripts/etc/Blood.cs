using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : Burnable
{
    int x, y;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            Burn();
        }
    }
}
