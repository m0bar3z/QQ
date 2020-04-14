using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
  /*  [Range(1, 10)]
    public float sizeofEachStep;*/
  
    [Range(0.25f, 5f)]
    public float timeBetweensteps;
    public float stopAtRange = 4, moveForceMultiplier, burnForceMultiplier;


    [SerializeField] // for assigning by hand in tests
    private Transform _target; // this will be given to enemy by crowd system
    [SerializeField]
    private Burnable _burnable;
    [SerializeField]
    private Vector2 targetDistance;

    public void AssignTarget(Transform target)
    {
        _target = target;
    }

    public void GetClose()
    {
        targetDistance = _target.position - transform.position;

        if (targetDistance.magnitude > stopAtRange && !_burnable.burning )
        {
            ReceiveForce(targetDistance.normalized * moveForceMultiplier);
        } else if (_burnable.burning)
        {
            RandomRun();
        }
    }

    public void Shooting()
    {

    }

    public void RandomRun()
    {
        targetDistance = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
        ReceiveForce(targetDistance * burnForceMultiplier);
    }

    protected override void Start()
    {
        base.Start();

        InvokeRepeating(nameof(GetClose), 1f, timeBetweensteps);
    }

    // go for the nearest gun if not holding any

    // get close to target using a trigger

    // shoot the target

    // if burning run around randomly (makes others catch fire xD)

}