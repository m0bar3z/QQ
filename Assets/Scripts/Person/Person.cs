using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : QQObject
{
    public QQObject rightHand;
    public bool hasGun, facingRight = true;

    public void PickUp()
    {

    }

    protected void DoInteract()
    {

    }

    protected override void Start()
    {
        base.Start();

        if (hasGun)
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
