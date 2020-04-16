using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics : MonoBehaviour
{
    public static Statics instance;

    public GameObject fireFX;

    private void Start()
    {
        instance = this;
    }
}
