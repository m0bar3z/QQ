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
    private CanvasScaler canvasScaler;

    private Vector2 ScreenScale
    {
        get
        {
            if (canvasScaler == null)
            {
                canvasScaler = GetComponentInParent<CanvasScaler>();
            }

            if (canvasScaler)
            {
                return new Vector2(canvasScaler.referenceResolution.x / Screen.width, canvasScaler.referenceResolution.y / Screen.height);
            }
            else
            {
                return Vector2.one;
            }
        }
    }

    public void AddCoins()
    {
        coins++;
        SetCoins();
    }

    public void Buy(int index, int price)
    {
        // TODO: do it with strategy pattern
        if (hasController && coins >= price)
        {
            coins -= price;
            SetCoins();

            if (goods[index].isHandheld)
            {
                if (controller.rightHandFull)
                {
                    controller.Throw();
                }

                QQObject obj = Instantiate(goods[index].prefab).GetComponent<QQObject>();
                if (!controller.facingRight) obj.gameObject.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                if(!controller.facingRight) obj.gameObject.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                controller.PickUp(obj);
            }
            else
            {
                goods[index].Consume(controller);
            }

            Close();
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
        RectTransform rtOfFirstIndex = firstIndex.GetComponent<RectTransform>();
        int i = 0;
        foreach(ShopGood g in goods)
        {
            if(i == 0)
            {
                firstIndex.SetImage(g.sprite);
                firstIndex.SetTxt(g.name_ + " - $" + g.price);
                firstIndex.index = i;
                firstIndex.good = g;

                panels.Add(firstIndex);
            }
            else
            {
                GunPanel p = Instantiate(itemPanel, panelParent).GetComponent<GunPanel>();
                p.GetComponent<RectTransform>().anchoredPosition = new Vector2(rtOfFirstIndex.anchoredPosition.x, rtOfFirstIndex.anchoredPosition.y + rtOfFirstIndex.sizeDelta.y * -i);
                p.SetImage(g.sprite);
                p.SetTxt(g.name_ + " - $" + g.price);
                p.index = i;
                p.good = g;

                panels.Add(p);
            }
            i++;
        }
    }
}
