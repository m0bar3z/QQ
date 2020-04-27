using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodObject : QQObject
{
    public int x, y;

    public void Deactivate()
    {
        BloodSystem.instance.Deactivate(x, y);
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
