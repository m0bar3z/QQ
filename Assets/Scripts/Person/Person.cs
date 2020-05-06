using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author : Mh
public class Person : QQObject
{
    [Header("Person Vars")]
    [Space(20)]

    public QQObject rightHand;
    public bool rightHandFull, facingRight = true;
    public float handsReach = 2;
    public Transform handPos;

    protected Burnable _burnable;
    protected bool locked = false;

    public virtual void PickUp()
    {
        if (!rightHandFull)
        {
            Collider2D[] nearbyObjs = Physics2D.OverlapCircleAll(transform.position, handsReach);
            foreach (Collider2D c in nearbyObjs)
            {
                if (c.gameObject == gameObject) continue;

                QQObject o = c.GetComponent<QQObject>();
                if (o == null || o.hasHolder || o.isStatic) continue;
                o.GetPickedUp(this);
                break;
            }
        }
    }

    public virtual void PickUp(QQObject obj)
    {
        obj.GetPickedUp(this);
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

        _burnable = GetComponent<Burnable>();
        _burnable.OnBurn += OnBurn;
    }

    protected override void Update() { }

    protected void CheckFacing(Vector3 targetPos)
    {
        targetPos.z = 0;

        bool shouldFaceRight = (targetPos - transform.position).x > 0;
        if (shouldFaceRight && !facingRight)
        {
            InvertXScale();
        }
        if (!shouldFaceRight && facingRight)
        {
            InvertXScale();
        }
    }

    protected override void OnDamage()
    {
        base.OnDamage();
    }

    protected override void OnDie()
    {
        if (reallyDie)
        {
            _burnable.StopBurning();
            base.OnDie();
        }
    }

    protected virtual void OnBurn()
    {

    }

    private void InvertXScale()
    {
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        facingRight = !facingRight;
    }
}
