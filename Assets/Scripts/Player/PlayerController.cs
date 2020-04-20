using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Person
{
    public KeyCode MoveUp, MoveDown, MoveLeft, MoveRight;
    public GameObject coinFX;

    protected override void Start()
    {
        base.Start();
        health.OnHeal += OnHeal;
    }

    protected override void Update()
    {
        if (!locked)
        {
            base.Update();
            CheckInput();
        }
    }

    protected override void OnDie()
    {
        Statics.instance.GameOver();
        locked = true;
    }

    protected override void OnDamage()
    {
        Statics.instance.SetHealth(health.amount / 100);
        base.OnDamage();
    }

    protected void OnHeal()
    {
        Statics.instance.SetHealth(health.amount / 100);
    }

    private void CheckInput()
    {
        if (Input.GetMouseButton(0))
        {
            RightHandTrigger();
        }
    }

    private void RightHandTrigger()
    {
        CheckFacing(Camera.main.ScreenToWorldPoint(Input.mousePosition));        

        if (rightHandFull)
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir -= (Vector2)rightHand.transform.position;
            rightHand.Trigger(dir);
        }
        else
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dir -= (Vector2)transform.position;
            ReceiveForce(dir.normalized);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if(collision.gameObject.layer == 12)
        {
            if(collision.gameObject.tag == "coin")
            {
                Shop.coins++;
                Instantiate(coinFX, transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
        }
    }
}
