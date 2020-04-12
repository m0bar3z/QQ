using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public BasicGun gun;
    public Moving PlayerMoving;
    public bool hasGun, facingRight = true;
    public KeyCode MoveUp, MoveDown, MoveLeft, MoveRight;

    private void Start()
    {
        if (hasGun)
        {
            gun.holder = transform;
            gun.holderController = this;
        }
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButton(0))
        {
            CheckFacing();

            if (hasGun)
            {
                gun.Shoot();
            }
        }
        ////////////////// Moving!!!
        /// Move Up
        if (Input.GetKeyDown(MoveUp))
        {
            PlayerMoving.Up = true;
        } else if (Input.GetKeyUp(MoveUp))
        {
            PlayerMoving.Up = false;
        }
        /// Move Down
        if (Input.GetKeyDown(MoveDown))
        {
            PlayerMoving.Down = true;
        }else if (Input.GetKeyUp(MoveDown))
        {
            PlayerMoving.Down = false;
        }
        /// Move Left
        if (Input.GetKeyDown(MoveLeft))
        {
            PlayerMoving.Left = true;
        } else if (Input.GetKeyUp(MoveLeft))
        {
            PlayerMoving.Left = false;
        }
        /// Move Right
        if (Input.GetKeyDown(MoveRight))
        {
            PlayerMoving.Right = true;
        } else if (Input.GetKeyUp(MoveRight))
        {
            PlayerMoving.Right = false;
        }
    }

    private void CheckFacing()
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
