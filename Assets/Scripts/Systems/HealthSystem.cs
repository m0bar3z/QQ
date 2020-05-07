using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HealthSystem
{
    public float amount = 100;

    public event SystemTools.SimpleSystemCB OnDie, OnHeal;
    public event SystemTools.SimpleSystemCB OnDamage;
    public bool isDead = false;

    public virtual void Healt(float amount)
    {
        this.amount += amount;
        if (this.amount > 100) this.amount = 100;
        OnHeal?.Invoke();
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
        isDead = true;
        OnDie?.Invoke();
    }
}
