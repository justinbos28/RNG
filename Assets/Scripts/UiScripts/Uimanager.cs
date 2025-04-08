using UnityEngine;

public class Uimanager : MonoBehaviour 
{
    public GameObject Inventory;
    public GameObject Shop;
    public GameObject Index;
    public GameObject Stats;
    public GameObject Crafter;
    public GameObject Teleporter;

    public CraftingRecipes CraftingRecipes;
    public MoneyLogic MoneyLogic;
    public StatsScript StatsScript;

    public bool ShopPanel;
    public bool OpenIndex;
    public bool InventoryPanel;
    public bool OpenStats;
    public bool OpenCrafter;
    public bool OpenPortal;

    public void ShopButton()
    {
        if (Shop == null)
        {
            Debug.LogError("Shop GameObject is not assigned.");
            return;
        }

        if (MoneyLogic == null)
        {
            Debug.LogError("MoneyLogic script is not assigned.");
            return;
        }

        if (ShopPanel == false)
        {
            Shop.SetActive(true);
            ShopPanel = true;
            MoneyLogic.UpdateEverything();

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
        OreStorage OreStorage = FindObjectOfType<OreStorage>();
        OreStorage.SetToDefault();
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
            CraftingRecipes.UpdateUi();
        }
        else
        {
            OpenCrafter = false;
            Crafter.SetActive(false);
        }
    }

    public void OpenIndexSwitch()
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
            Teleporter.SetActive(false);
            OpenPortal = false;
            StatsScript.RebirthMenu.SetActive(false);
        }
        else
        {
            OpenStats = false;
            Stats.SetActive(false);
        }
    }

    public void OpenTeleporter()
    {
        if (OpenPortal == false)
        {
            StatsScript.RebirthMenu.SetActive(false);
            OpenStats = false;
            Stats.SetActive(false);
            Teleporter.SetActive(true);
            OpenPortal = true;
        }
        else
        {
            Teleporter.SetActive(false);
            OpenPortal = false;
        }
    }

}
