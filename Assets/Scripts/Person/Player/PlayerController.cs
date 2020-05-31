using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Person
{
    public static bool isAlive = true;

    public KeyCode MoveUp, MoveDown, MoveLeft, MoveRight;
    public GameObject willieSprite, billieSprite;
    public int characterIndex;

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
        }
    }

    protected virtual void CheckInput()
    {
        if (Input.GetMouseButton(0))
        {
            RightHandTrigger();
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
}
