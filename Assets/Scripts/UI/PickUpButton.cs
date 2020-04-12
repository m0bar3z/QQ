using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpButton : MonoBehaviour
{
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void PickUp()
    {
        player.PickUp();
    }
}
