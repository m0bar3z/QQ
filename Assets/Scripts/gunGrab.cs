﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunGrab : MonoBehaviour
{
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "gun")
        {
            print("Trigger");
        }
    }
}
