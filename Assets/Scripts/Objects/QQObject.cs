using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QQObject : MonoBehaviour
{
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

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
}
