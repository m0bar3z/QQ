using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QQObject : MonoBehaviour
{
    public Person holderController;

    public HealthSystem health;

    protected Rigidbody2D rb;

    public virtual void GetPickedUp(Person picker)
    {
        transform.parent = picker.handPos;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        picker.rightHand = this;
        picker.rightHandFull = true;
        transform.localPosition = Vector3.zero;

        holderController = picker;
    }

    public virtual void GetThrown(Vector2 dir)
    {
        holderController = null;

        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        ReceiveForce(dir * 20);
    }

    public virtual void ReceiveForce(Vector2 force)
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    public virtual void Interact()
    {

    }

    protected virtual void Start()
    {
        health = new HealthSystem();
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
