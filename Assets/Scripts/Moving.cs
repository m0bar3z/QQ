using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Rigidbody2D Rig;
    [Range(0, 5)]
    public float Speed;
    public bool Up, Down, Right, Left;
    
    void Start()
    {
        Rig = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (Up)
        {
            transform.Translate(Vector3.up * Time.deltaTime * Speed);
        } else if (!Up)
        {
            transform.Translate(Vector3.up * 0);
        }

        if (Down)
        {
            transform.Translate(Vector3.down * Time.deltaTime * Speed);
        } else if (!Down)
        {
            transform.Translate(Vector3.down * 0);
        }

        if (Left)
        {
            transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }else if (!Left)
        {
            transform.Translate(Vector3.left * 0);
        }

        if (Right)
        {
            transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }else if (!Right)
        {
            transform.Translate(Vector3.right * 0);
        }
    }
}
