using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    [Range(1f, 8f)]
    public float Radius;
    public bool Burning;
    public float ExtraTemperature;
    public Collider2D[] Colliders;

    private HealthSystem _health;
    private TemperatureSystem _temperatureSystem;

    private void Start()
    {
        _health = new HealthSystem();
        _temperatureSystem = new TemperatureSystem();
        Burn();
    }
    public virtual void Burn()
    {
      Burning = true;
        _temperatureSystem.AddTemp(ExtraTemperature);
      InvokeRepeating("ReduceHealth", 1f, 0.5f);
    }

    private void ReduceHealth()
    {
        Colliders = Physics2D.OverlapCircleAll(transform.position, Radius);
        if (Colliders.Length >= 1)
        {
            for (int i = 0; i < Colliders.Length; i++)
            {
                print("Health reduced!!!  " + _health.Amount + "  " + Colliders[i].gameObject.name);
                // Colliders[i].GetComponent<HealthSystem>().Amount -= 1;
                _health.Amount--;
            }
        }
        else
        { 
            print("no one in burn radius!!!");
            Burning = false;
        }
    }



}
