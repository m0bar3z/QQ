using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionFire : QQObject
{
    protected override void Start()
    {
        base.Start();
        GetComponent<Burnable>().Burn();
    }
}
