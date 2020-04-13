using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : QQObject
{
    public QQObject rightHand;
    public bool rightHandFull, facingRight = true;
    public float handsReach = 2;
    public Transform handPos;

    public void PickUp()
    {
        Collider2D[] nearbyObjs = Physics2D.OverlapCircleAll(transform.position, handsReach);
        foreach(Collider2D c in nearbyObjs)
        {
            QQObject o = c.GetComponent<QQObject>();
            if (o == null) continue;
            o.PickUp(this);
            break;
        }
    }

    protected void DoInteract()
    {

    }

    protected override void Start()
    {
        base.Start();

        if (rightHandFull)
        {
            rightHand.holder = transform;
            rightHand.holderController = this;
        }
    }

    protected void CheckFacing()
    {
        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mp.z = 0;

        bool shouldFaceRight = (mp - transform.position).x < 0;
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
