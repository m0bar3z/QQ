using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : Burnable
{
    public int benefit = 2;
    int x, y;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9 || collision.gameObject.layer == 15)
        {
            Burn();
        }
        else if(collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<PlayerController>().health.Healt(benefit);
            GetComponent<BloodObject>().Deactivate();
        }
    }
}
