using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "shop/WaterBottle")]
public class WaterBottle : ShopGood
{
    public AudioClip purchaseSFX;

    public override void Consume(PlayerController pc)
    {
        Statics.instance.publicAS.PlayOneShot(purchaseSFX);
        pc.GetComponent<Burnable>().StopBurning();
    }
}
