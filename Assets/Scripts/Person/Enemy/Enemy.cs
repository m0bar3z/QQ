using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
  /*  [Range(1, 10)]
    public float sizeofEachStep;*/
  
    [Range(0.25f, 5f)]
    public float timeBetweensteps;
    public float stopAtRange = 4, moveForceMultiplier, timeMultiplier = 1;


    [SerializeField] // for assigning by hand in tests
    private Transform _target; // this will be given to enemy by crowd system
    [SerializeField]
    private Burnable _burnable;
    [SerializeField]
    private Vector2 targetDistance;

    private float _time;

    public void AssignTarget(Transform target)
    {
        _target = target;
    }

    protected override void Start()
    {
        base.Start();
        _burnable.OnBurn += onBurn;
    }

    protected override void Update()
    {
        _time += Time.deltaTime;
        if(_time > timeBetweensteps * timeMultiplier)
        {
            _time = 0;
            Tick();
        }
    }

    private void onBurn()
    {
        timeMultiplier = 0.5f;
    }

    private void Tick()
    {
        Move();
        Shoot();
    }

    private void Shoot()
    {
        rightHand.Trigger(_target.position - rightHand.transform.position);
    }

    private void Move()
    {
        targetDistance = _target.position - transform.position;

        if (targetDistance.magnitude > stopAtRange && !_burnable.burning)
        {
            ReceiveForce(targetDistance.normalized * moveForceMultiplier);
        }
        else if (_burnable.burning)
        {
            RandomRun();
        }
    }

    private void RandomRun()
    {
        targetDistance = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
        ReceiveForce(targetDistance * moveForceMultiplier);
    }
}