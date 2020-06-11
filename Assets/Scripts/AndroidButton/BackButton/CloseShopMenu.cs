using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShopMenu : MonoBehaviour, IBackButton
{
    public Shop shop;
   public void BackButton()
    {
        shop.Close();
    }
}
