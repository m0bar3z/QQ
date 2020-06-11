using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButtonInGame : MonoBehaviour
{
    public IBackButton openShopMenuAction;
    public IBackButton closeShopMenuAction;
    
    public void CloseShopMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            closeShopMenuAction.BackButton();
        }
    }
    public void OpenShopMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           openShopMenuAction.BackButton();
        }
    }
    void Start()
    {
        openShopMenuAction = gameObject.GetComponent<OpenShopMenu>();
        closeShopMenuAction = gameObject.GetComponent<CloseShopMenu>();
    }

    void Update()
    {
        if(Time.timeScale > 0)
        {
            OpenShopMenu();
        } else
        {
            CloseShopMenu();
        }
    }
}
