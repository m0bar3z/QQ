using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QQObject : MonoBehaviour
{
    public Transform holder;
    public Person holderController;

    protected Rigidbody2D rb;

    public virtual void PickUp(PlayerController picker)
    {

    }

    public virtual void ReceiveForce(Vector2 force)
    {
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
