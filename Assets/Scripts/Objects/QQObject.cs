using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QQObject : MonoBehaviour
{
    public Person holderController;
    public HealthSystem health;
    public bool isStatic, hasHolder;

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
    }

    protected virtual void Awake()
    {
        if(!isStatic)
            rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update() { }
}
