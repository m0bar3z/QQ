using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    [Range(0.01f, 8f)]
    public float radius;
    public bool burning;
    public float extraTemperature, burnEffectTick;
    public Collider2D[] colliders;
    public GameObject burnFX;
    public bool hasBurnFX = false;

    private QQObject _obj;

    // OnBurn event
    public event SystemTools.SimpleSystemCB OnBurn;

    public virtual void Burn()
    {
        burning = true;
        InvokeRepeating(nameof(BurnEffect), burnEffectTick, 0.5f);

        if (hasBurnFX)
        {
            burnFX.SetActive(true);
        }
        else
        {
            burnFX = Instantiate(Statics.instance.fireFX, transform.position + Vector3.back * 0.1f, Quaternion.identity, transform);
        }        

        OnBurn?.Invoke();

        // for test purposes
        GetComponent<SpriteRenderer>().color = Color.red;
    }

    public virtual void StopBurning()
    {
        burning = false;
        Destroy(burnFX);
        CancelInvoke(nameof(BurnEffect));
    }

    private void Start()
    {
        SetObj();

        if (burning)
        {
            Burn();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void SetObj()
    {
        _obj = GetComponent<QQObject>();
        if (_obj == null)
        {
            Debug.LogWarning("there's a burnable component on something that's not an object");
        }
    }

    // TODO: find a way so we don't have this many getcomponents
    private void BurnEffect()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        if (colliders.Length >= 1)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                Burnable adjucantBurnable = colliders[i].GetComponent<Burnable>();
                if(adjucantBurnable != null && !adjucantBurnable.burning && !colliders[i].isTrigger)
                {
                    adjucantBurnable.Burn();
                }

                _obj.Hurt(5);
            }
        }
    }
}
