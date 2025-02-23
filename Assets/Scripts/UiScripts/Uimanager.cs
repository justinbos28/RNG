using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uimanager : MonoBehaviour 
{
    public GameObject Inventory;
    public GameObject Shop;
    public GameObject Index;
    public GameObject Stats;
    public GameObject Crafter;
    public bool ShopPanel;
    public bool OpenIndex;
    public bool InventoryPanel;
    public bool OpenStats;
    public bool OpenCrafter;
    public void ShopButton()
    {
        if (ShopPanel == false)
        {
            Shop.SetActive(true);
            ShopPanel = true;

            InventoryPanel = false;
            OpenCrafter = false;
            Inventory.SetActive(false);
            Crafter.SetActive(false);
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
            OpenCrafter = false;
            Shop.SetActive(false);
            Crafter.SetActive(false);
        }
        else
        {
            Inventory.SetActive(false);
            InventoryPanel = false;
        }
    }

    public void CrafterButton()
    {
        if (OpenCrafter == false)
        {
            OpenCrafter = true;
            Crafter.SetActive(true);

            InventoryPanel = false;
            ShopPanel = false;
            Inventory.SetActive(false);
            Shop.SetActive(false);
        }
        else
        {
            OpenCrafter = false;
            Crafter.SetActive(false);
        }
    }

    public void SwitchToIndex()
    {
        if (OpenIndex == false)
        {
            Index.SetActive(true);
            OpenIndex = true;
        }
        else
        {
            Index.SetActive(false);
            OpenIndex = false;
        }

    }

    public void SwitchStats()
    {
        if (OpenStats == false)
        {
            OpenStats = true;
            Stats.SetActive(true);
        }
        else
        {
            OpenStats = false;
            Stats.SetActive(false);
        }
    }
}
