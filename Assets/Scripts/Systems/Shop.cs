using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Shop : MonoBehaviour
{
    public int coins = 0;
    public Text coinsText;
    public RectTransform panel;

    public GameObject itemPanel;
    public GunPanel firstIndex;
    public ShopGood[] goods;
    public Transform panelParent;

    private Vector2 basePos;
    private float ts;
    private List<GunPanel> panels = new List<GunPanel>();

    private PlayerController controller;
    private bool hasController;

    public void AddCoins()
    {
        coins++;
        SetCoins();
    }

    public void Buy(int index)
    {
        // TODO: do it with strategy pattern
        if (hasController)
        {
            if (goods[index].isHandheld)
            {
                if (controller.rightHandFull)
                {
                    controller.Throw();
                }

                QQObject obj = Instantiate(goods[index].prefab).GetComponent<QQObject>();

                controller.PickUp(obj);
            }
        }
    }

    public void SetCoins()
    {
        coinsText.text = coins + "";
    }

    public void OpenUp()
    {
        ts = Time.timeScale;
        Time.timeScale = 0;
        panel.DOMoveX(0, 1).SetUpdate(true);
    }

    public void Close()
    {
        panel.position = new Vector2(basePos.x, basePos.y);
        panel.DOMoveX(basePos.x, 1).SetUpdate(true);
        Time.timeScale = ts;
    }

    private void Start()
    {
        basePos = panel.position;
        AssignPC();
        DrawShop();
    }

    private void AssignPC()
    {
        controller = FindObjectOfType<PlayerController>();
        hasController = controller != null;
    }

    private void DrawShop()
    {
        int i = 0;
        foreach(ShopGood g in goods)
        {
            if(i == 0)
            {
                firstIndex.SetImage(g.sprite);
                firstIndex.SetTxt(g.name_ + " - " + g.price);
                firstIndex.index = i;

                panels.Add(firstIndex);
            }
            else
            {
                GunPanel p = Instantiate(itemPanel, firstIndex.transform.position + firstIndex.GetComponent<RectTransform>().rect.height / 2 * i * Vector3.down, Quaternion.identity, panelParent).GetComponent<GunPanel>();
                p.SetImage(g.sprite);
                p.SetTxt(g.name_ + " - " + g.price);
                p.index = i;

                panels.Add(p);
            }
            i++;
        }
    }
}
