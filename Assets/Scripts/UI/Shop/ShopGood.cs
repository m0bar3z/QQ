using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="shop/good")]
public class ShopGood : ScriptableObject
{
    public GameObject prefab;
    public bool isHandheld;
    public string name_;
    public int price;
    public Sprite sprite;

    public virtual void Consume(PlayerController pc) { }
}
