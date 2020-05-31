using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodPL : PoolList<Blood>
{
    public static BloodPL instance;

    protected override void Start()
    {
        base.Start();

        instance = this;
    }
}
