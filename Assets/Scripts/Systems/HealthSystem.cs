using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem
{
    public float amount = 100;

    public event SystemTools.SimpleSystemCB OnDie;
    public event SystemTools.SimpleSystemCB OnDamage;

    public virtual void Health()
    {

    }

    public virtual void Damage(float damage)
    {
        amount -= damage;
        OnDamage?.Invoke();
        if(amount <= 0)
        {
            Die();
        }
    }

    public virtual void GetAlive()
    {

    }

    public virtual void Die()
    {
        OnDie?.Invoke();
    }
}
