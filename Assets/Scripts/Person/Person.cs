using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author : Mh
public class Person : QQObject
{
    public QQObject rightHand;
    public bool rightHandFull, facingRight = true;
    public float handsReach = 2;
    public Transform handPos;

    public virtual void PickUp()
    {
        Collider2D[] nearbyObjs = Physics2D.OverlapCircleAll(transform.position, handsReach);
        foreach(Collider2D c in nearbyObjs)
        {
            if (c.gameObject == gameObject) continue;

            QQObject o = c.GetComponent<QQObject>();
            if (o == null) continue;
            o.GetPickedUp(this);
            break;
        }
    }

    public virtual void Throw()
    {
        rightHand.GetThrown(facingRight?Vector2.right:Vector2.left);
        rightHand = null;
        rightHandFull = false;
    }

    protected void DoInteract()
    {

    }

    protected override void Start()
    {
        base.Start();

        if (rightHandFull)
        {
            rightHand.holderController = this;
        }
    }

    protected void CheckFacing()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0;

        bool shouldFaceRight = (mp - transform.position).x > 0;
        if (shouldFaceRight && !facingRight)
        {
            InvertXScale();
        }
        if (!shouldFaceRight && facingRight)
        {
            InvertXScale();
        }
    }

    private void InvertXScale()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        facingRight = !facingRight;
    }
}
