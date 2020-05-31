using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopMenu : MonoBehaviour, IBackButton
{
    public Shop shop;
    public void BackButton()
    {
        shop.OpenUp();
    }
}
