using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QQObject : MonoBehaviour
{
    public Person holderController;
    public HealthSystem health;
    public bool isStatic, hasHolder, isBloody;

    public GameObject bloodEffect;

    protected Rigidbody2D rb;

    public virtual void GetPickedUp(Person picker)
    {
        holderController = picker;
        hasHolder = true;

        transform.parent = picker.handPos;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        picker.rightHand = this;
        picker.rightHandFull = true;
        transform.localPosition = Vector3.zero;
    }

    public virtual void GetThrown(Vector2 dir)
    {
        holderController = null;
        hasHolder = false;

        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        ReceiveForce(dir * 20);
    }

    public virtual void ReceiveForce(Vector2 force)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public virtual void Trigger()
    {

    }

    public virtual void Trigger(Vector3 dir)
    {

    }

    protected virtual void Start()
    {
        health = new HealthSystem();
        health.OnDamage += OnDamage;
        health.OnDie += OnDie;
    }

    protected virtual void Awake()
    {
        if(!isStatic)
            rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() { }

    protected virtual void OnDamage()
    {
        if (isBloody)
        {
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
        }
    }

    protected virtual void OnDie()
    {
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            if (isBloody)
            {
                BloodSystem.instance.Spill((Vector2)transform.position, (Vector2)transform.position - (Vector2)collision.transform.position);
            }

            health.Damage(100);
        }
    }
}
