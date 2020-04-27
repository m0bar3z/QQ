using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPanel : MonoBehaviour
{
    public Text txt;
    public Image img;
    public int index;
    public ShopGood good;

    public void SetImage(Sprite sprite)
    {
        img.sprite = sprite;
    }

    public void SetTxt(string txt)
    {
        this.txt.text = txt;
    }

    public void Buy()
    {
        Statics.instance.shop.Buy(index, good.price);
    }
}
