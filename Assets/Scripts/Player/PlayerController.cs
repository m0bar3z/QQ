using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public BasicGun gun;
    public bool hasGun, facingRight = true;

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
