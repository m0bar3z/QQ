using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Walk : MonoBehaviour
{
    public float offset;

    private Animator Animator;
    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
    }
   
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.DOMove(transform.position + (transform.up * offset), 0.3f);
            Animator.SetBool("_walk", true);
        } else if (Input.GetKeyUp(KeyCode.W))
        {
            Animator.SetBool("_walk", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.DOMove(transform.position +(- transform.up * offset), 0.3f);
            Animator.SetBool("_walk", true);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            Animator.SetBool("_walk", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.DOMove(transform.position + (transform.right * offset), 0.3f);
            Animator.SetBool("_walk", true);
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            Animator.SetBool("_walk", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            transform.DOMove(transform.position + transform.right * offset, 0.3f);
            Animator.SetBool("_walk", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            Animator.SetBool("_walk", false);
        }
    }
}
