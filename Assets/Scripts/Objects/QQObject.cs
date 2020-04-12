using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QQObject : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void PickUp(PlayerController picker)
    {

    }

    public virtual void ReceiveForce(Vector2 force)
    {

    }
}
