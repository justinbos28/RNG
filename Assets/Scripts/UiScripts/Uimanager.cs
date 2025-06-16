using UnityEngine;

public class Uimanager : MonoBehaviour 
{
    public GameObject Inventory;
    public GameObject Shop;
    public GameObject Index;
    public GameObject Stats;
    public GameObject Machines;
    public GameObject Teleporter;

    public MoneyLogic MoneyLogic;
    public StatsScript StatsScript;

    public bool ShopPanel;
    public bool OpenIndex;
    public bool InventoryPanel;
    public bool OpenStats;
    public bool OpenMachines;
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
            OpenMachines = false;
            Inventory.SetActive(false);
            Machines.SetActive(false);
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
            OpenMachines = false;
            Shop.SetActive(false);
            Machines.SetActive(false);
        }
        else
        {
            Inventory.SetActive(false);
            InventoryPanel = false;
        }
    }

    public void MachinesButton()
    {
        if (OpenMachines == false)
        {
            OpenMachines = true;
            Machines.SetActive(true);

            InventoryPanel = false;
            ShopPanel = false;
            Inventory.SetActive(false);
            Shop.SetActive(false);
        }
        else
        {
            OpenMachines = false;
            Machines.SetActive(false);
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
    public void ExitStats()
    {
        Stats.SetActive(false);
    }
}
