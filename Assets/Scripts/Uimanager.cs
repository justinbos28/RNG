using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uimanager : MonoBehaviour 
{
    public GameObject Shop;
    public bool ShopPanel;
    public void ShopButton()
    {
        if (ShopPanel == false)
        {
            Shop.SetActive(true);
            ShopPanel = true;
        }
        else
        {
            Shop.SetActive(false);
            ShopPanel = false;
        }
    }
}
