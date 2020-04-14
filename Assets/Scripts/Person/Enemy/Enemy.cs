using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Person
{
    private Transform _target; // this will be given to enemy by crowd system

    public void AssignTarget(Transform target)
    {
        _target = target;
    }

    // go for the nearest gun if not holding any

    // get close to target using a trigger

    // shoot the target

    // if burning run around randomly (makes others catch fire xD)
}