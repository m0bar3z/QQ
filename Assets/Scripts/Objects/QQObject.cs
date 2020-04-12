using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QQObject : MonoBehaviour
{
    public Transform holder;
    public Person holderController;

    protected Rigidbody2D rb;

    public virtual void PickUp(Person picker)
    {
        transform.parent = picker.handPos;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        picker.rightHand = this;
        picker.rightHandFull = true;
        transform.localPosition = Vector3.zero;
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
        
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
