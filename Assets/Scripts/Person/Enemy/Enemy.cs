using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
    /*[Range(1, 10)]
    public float sizeofEachStep;*/
  
    [Range(0.25f, 5f)]
    public float timeBetweensteps;
    public float stopAtRange = 4, moveForceMultiplier, timeMultiplier = 1, reach = 4;

    [SerializeField] // for assigning by hand in tests
    private Transform _target; // this will be given to enemy by crowd system
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

    protected override void OnDamage()
    {
        base.OnDamage();
    }

    protected override void OnDie()
    {
        base.OnDie();
    }

    protected override void OnBurn()
    {
        base.OnBurn();
        timeMultiplier = 0.5f;
    }

    private void Tick()
    {
        if (!_burnable.burning)
        {
            Move();
            Shoot();
        }
        else
        {
            RandomRun();
        }
    }

    private void Shoot()
    {
        CheckFacing(_target.position);

        Vector3 diff = _target.position - transform.position;
        diff.z = 0;

        if (diff.magnitude < reach)
            rightHand.Trigger(diff);
    }

    private void Move()
    {
        if (targetDistance.magnitude > stopAtRange)
        {
            targetDistance = _target.position - transform.position;
            ReceiveForce(targetDistance.normalized * moveForceMultiplier);
        }
    }

    private void RandomRun()
    {
        targetDistance = (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f))).normalized;
        ReceiveForce(targetDistance * moveForceMultiplier);
    }
}