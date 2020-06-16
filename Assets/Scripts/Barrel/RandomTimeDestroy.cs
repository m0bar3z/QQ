using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimeDestroy : MonoBehaviour
{
    public float minDestroyTime, maxiDestroyTime;
    void Start()
    {
        Destroy(gameObject, Random.Range(minDestroyTime, maxiDestroyTime));
    }
}
