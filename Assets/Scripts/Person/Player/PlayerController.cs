using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Person
{
    public static bool isAlive = true;

    public KeyCode MoveUp, MoveDown, MoveLeft, MoveRight;
    public GameObject willieSprite, billieSprite, dashFXPrefab;
    public int characterIndex;
    public float dashWait = 0.2f, dashSpeed = 70;

    private float time;
    public bool timerOn = false, isShooting = false;

    public void TouchInput()
    {
        RightHandTrigger();
    }

    public override void PickUp(QQObject obj)
    {
        obj.GetPickedUp(this);
    }

    protected override void Start()
    {
        isAlive = true;
        base.Start();
        health.OnHeal += OnHeal;

        SetCharacter();
    }

    private void SetCharacter()
    {
        characterIndex = PlayerPrefsManager.GetCharacter();
        if (characterIndex == 0)
        {
            billieSprite.SetActive(false);
        }
        else
        {
            willieSprite.SetActive(false);
        }
    }

    protected override void Update()
    {
        if (!locked)
        {
            base.Update();
            CheckInput();

            if (timerOn)
            {
                time += Time.deltaTime;
                if(time > dashWait)
                {
                    time = 0;
                    timerOn = false;
                }
            }
        }
    }

    protected virtual void Dash()
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir -= (Vector2)transform.position;
        ReceiveForce(dir.normalized * dashSpeed);
        Instantiate(dashFXPrefab, transform);
    }

    protected virtual void CheckInput()
    {
        if (Input.GetMouseButton(0))
        {
            if (!timerOn)
                RightHandTrigger();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (timerOn)
            {
                timerOn = false;
                time = 0;
                Dash();
            }
            else
            {
                timerOn = true;
                time = 0;
            }
        }
    }

    protected override void OnDie()
    {
        Statics.instance.GameOver();
        isAlive = false;
        locked = true;
    }

    protected override void OnDamage()
    {
        Statics.instance.SetHealth(health.Amount / 100);
        base.OnDamage();
    }

    protected void OnHeal()
    {
        Statics.instance.SetHealth(health.Amount / 100);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 15)
        {
            Bullet b = collision.gameObject.GetComponent<Bullet>();
            Hurt(b.damage);
            b.Blow();
        }
    }
}
