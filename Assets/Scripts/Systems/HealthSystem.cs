using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HealthSystem
{
    public float amount;

    public event SystemTools.SimpleSystemCB OnDie, OnHeal;
    public event SystemTools.SimpleSystemCB OnDamage;
    public bool isDead = false;

    public Slider2D healthSlider;
    public bool hasSlider = false;

    private float baseHealth;

    public HealthSystem(float baseAmount)
    {
        amount = baseAmount;
        baseHealth = amount;
    }

    public void AssignSlider(Slider2D s)
    {
        healthSlider = s;
        hasSlider = true;
        SetSlider();
    }

    public void SetSlider()
    {
        if (hasSlider)
        {
            healthSlider.value = amount / baseHealth;
        }
    }

    public float Amount
    {
        get
        {
            return amount;
        }
    }

    public virtual void Heal(float amount)
    {
        this.amount += amount;
        if (this.amount > 100) this.amount = 100;
        SetSlider();
        OnHeal?.Invoke();
    }

    public virtual void Damage(float damage)
    {
        amount -= damage;
        SetSlider();
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
