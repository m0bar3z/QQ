using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Person
{
    public KeyCode MoveUp, MoveDown, MoveLeft, MoveRight;

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButton(0))
        {
            CheckFacing();

            if (rightHandFull)
            {
                rightHand.Interact();
            }
            else
            {
                Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dir -= (Vector2)transform.position;

                ReceiveForce(dir.normalized);
            }
        }
    }
}
