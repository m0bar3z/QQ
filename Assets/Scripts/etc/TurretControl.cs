using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : QQObject
{
    public Transform target, aim, gunHole;
    public float reloadTime = 1f;
    public float turnSpeed = 2f; 
    public float firePauseTime = 0.25f;
    public float range = 4f;

    public GameObject bulletPref;
    public AudioClip fireSFX;

    

    protected override void Start()
    {

    }

    protected override void Update()
    {
        
    }
}
