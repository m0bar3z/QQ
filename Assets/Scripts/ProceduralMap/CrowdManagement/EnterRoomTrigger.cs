using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterRoomTrigger : MonoBehaviour
{
    public bool playerInside = false;

    public event SystemTools.SimpleSystemCB OnPlayerGotIn;
    public event SystemTools.SimpleSystemCB OnPlayerGotOut;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 11)
        {
            if (playerInside)
                GotOut();
            else
                GotIn();
        }
    }

    private void GotOut()
    {
        playerInside = false;
        OnPlayerGotOut?.Invoke();
    }

    private void GotIn()
    {
        playerInside = true;
        OnPlayerGotIn?.Invoke();
    }
}
