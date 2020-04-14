using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author : Mh
public class InteractButton : MonoBehaviour
{
    public Image pickUpImg, throwImg;
    public bool throwing = false;

    private PlayerController player;

    public void InterAct()
    {
        if (throwing)
        {
            player.Throw();
        }
        else 
        {
            player.PickUp();
        }
        
        ActivateProperButton();
    }

    private void Start()
    {
        SetUpPlayer();
    }

    private void SetUpPlayer()
    {
        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("no player in scene, pickUp button needs player");
        }
        else
        {
            ActivateProperButton();
        }
    }

    private void ActivateProperButton()
    {
        if (player.rightHandFull)
        {
            EnableThrow();
            throwing = true;
        }
        else
        {
            EnablePickUp();
            throwing = false;
        }
    }

    private void EnablePickUp()
    {
        pickUpImg.gameObject.SetActive(true);
        throwImg.gameObject.SetActive(false);
    }

    private void EnableThrow()
    {
        pickUpImg.gameObject.SetActive(false);
        throwImg.gameObject.SetActive(true);
    }
}
