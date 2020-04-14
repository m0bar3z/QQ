using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
    [Range(1, 10)]
    public float sizeofEachStep;
  
    [Range(0.25f, 5f)]
    public float timeBetweensteps;
    public Vector2 targetDistance;
    public PlayerController playerTest;
    

    private Transform _target; // this will be given to enemy by crowd system

    public void AssignTarget(Transform target)
    {
        _target = target;
    }
    private void Start()
    {
        playerTest = FindObjectOfType<PlayerController>();
        _target = playerTest.GetComponent<Transform>(); 
        targetDistance = new Vector2(0f, 0f);
        InvokeRepeating(nameof(GetClose), 1f, timeBetweensteps);
    }

    // go for the nearest gun if not holding any

    // get close to target using a trigger

    // shoot the target

    // if burning run around randomly (makes others catch fire xD)

    public void GetClose()
    {
        FindTarget();
        if(targetDistance.magnitude > 10f)
        {
            print("magn " + targetDistance.magnitude);
            ReceiveForce(targetDistance);
        }
        
        //Vector2.MoveTowards(transform.position, _target.position, 0.5f);
    }

    public void FindTarget()
    {
       // speed += Time.deltaTime;
        targetDistance.x = (_target.position.x - transform.position.x) * sizeofEachStep;
        targetDistance.y = (_target.position.y - transform.position.y) * sizeofEachStep;
    }

    public void Shooting()
    {

    }

}