using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public abstract class AndroidButton : MonoBehaviour
{
    public abstract void BackButton();
    public int startMenuIndex = 1;
    public Shop shop;
    public SceneManage sceneManage;
}
public  class AndroidButtons : MonoBehaviour {}

public class BackToAndroid : AndroidButton
{
    public override void BackButton()
    {
        Application.Quit();
        print("Baby");
    }
}

public class BackToStartmenu : AndroidButton
{
    public override void BackButton()
    {
        sceneManage = FindObjectOfType<SceneManage>();
        sceneManage.ActiveLoadingWindow(startMenuIndex);
    }
}

public class OpenShopMenu : AndroidButton
{
    public override void BackButton()
    {
        shop = FindObjectOfType<Shop>();
        shop.OpenUp();
    }
}

public class CloseShopMenu : AndroidButton
{
    public override void BackButton()
    {
        shop = FindObjectOfType<Shop>();
        shop.Close();
    }
}
