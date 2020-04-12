using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    private float spawnSpeed;

    public virtual void AddSpeed(float change)
    {
        spawnSpeed += change;
    }
}
