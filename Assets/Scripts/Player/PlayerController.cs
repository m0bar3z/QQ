using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Person
{
    public KeyCode MoveUp, MoveDown, MoveLeft, MoveRight;

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

        Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dir -= (Vector2)transform.position;

        if (rightHandFull)
        {
            rightHand.Trigger(dir);
        }
        else
        {

            ReceiveForce(dir.normalized);
        }
    }
}
