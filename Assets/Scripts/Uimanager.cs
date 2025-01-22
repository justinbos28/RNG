using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uimanager : MonoBehaviour 
{
    public GameObject Inventory;
    public GameObject Shop;
    public bool ShopPanel;
    public bool InventoryPanel;
    public void ShopButton()
    {
        if (ShopPanel == false)
        {
            Shop.SetActive(true);
            ShopPanel = true;
            InventoryPanel = false;
            Inventory.SetActive(false);
        }
        else
        {
            Shop.SetActive(false);
            ShopPanel = false;
        }
    }
    public void InventoryButton()
    {
        if (InventoryPanel == false)
        {
            ShopPanel = false;
            Inventory.SetActive(true);
            InventoryPanel = true;
            Shop.SetActive(false);
        }
        else
        {
            Inventory.SetActive(false);
            InventoryPanel = false;
        }
    }
}
