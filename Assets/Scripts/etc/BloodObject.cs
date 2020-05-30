using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodObject : QQObject
{
    public int x, y;
    public float deactiveAfter = 20f;

    BloodPL pool;
    Blood bld;

    protected override void Start()
    {
        base.Start();

        pool = FindObjectOfType<BloodPL>();
        bld = GetComponent<Blood>();

        Invoke(nameof(Deactivate), deactiveAfter);
    }

    public void Deactivate()
    {
        //BloodSystem.instance.Deactivate(x, y);

        pool.ReturnOne(bld);
    }

    public void SetXY(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    protected override void OnDamage()
    {

    }

    protected override void OnDie()
    {
        Deactivate();
    }
}
