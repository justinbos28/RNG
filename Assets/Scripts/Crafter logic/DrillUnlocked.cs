using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DrillUnlocked : MonoBehaviour, IDataPersistence
{
    public Minerscript Minerscript;
    public RNGscript RNGscript;
    public MoneyLogic MoneyLogic;
    public OreStorage OreStorage;
    public List<drills> DrillList = new List<drills>();

    public Text Title;
    public Text Requirement;
    public Text NotEnough;
    public GameObject UpgradePanel;

    public bool IsUpgrade1;
    public bool FailedRequirements;
    public bool HasNoUpgrades;
    public int IndexValue;
    public int IndexValueActivate;
    public int IndexValueAutosell;
    public float timer;

    public void LoadData(GameData data)
    {
        this.DrillList = data.DrillList;
        LoadAllData();
    }
    
    public void SaveData(ref GameData data)
    {
        data.DrillList = this.DrillList;
    }
    public void GetUpgrade()
    {
        for (int i = 0; i < DrillList.Count; i++)
        {
            int index = i;
            DrillList[index].UpgradeButton.onClick.RemoveAllListeners();
            DrillList[index].UpgradeButton.onClick.AddListener(() =>
            {
                IndexValue = index;
                if (DrillList[IndexValue].Upgrade == 1)
                {
                    UpgradePanel.SetActive(true);
                    Title.text = "Upgrade Drill " + (index + 1);
                    Requirement.text = "DrillHead x1\nMotor x20\nWires x5\nTitanium x100\nDiamond x10\nCopper x200";
                    IsUpgrade1 = false;
                }
                else if (DrillList[IndexValue].Upgrade == 0)
                {
                    UpgradePanel.SetActive(true);
                    Title.text = "Upgrade Drill " + (index + 1);
                    Requirement.text = "DrillHead x1\nMotor x5\nTitanium x50\nIron x300\nCopper x150";
                    IsUpgrade1 = true;
                }
                else
                {
                    HasNoUpgrades = true;
                }
            });
        }
    }

    public void Upgrade()
    {
        if (IsUpgrade1)
        {
            CheckUpgrade();
        }
        else
        {
            CheckUpgrade2();
        }
    }

    private void CheckUpgrade()
    {

        var requiredMaterials = new Dictionary<string, int>
        {
            { "DrillHead", 1 },
            { "Motor", 5 }
        };

        var requiredGems = new Dictionary<string, int>
        {
            { "Titanium", 50 },
            { "Iron", 300 },
            { "Copper", 150 }
        };

        bool hasEnoughMaterials = requiredMaterials.All(material =>
            Minerscript.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));

        

        bool hasEnougGems = requiredGems.All(requiredGems =>
            RNGscript.allOres.Any(G => G.Name == requiredGems.Key && G.StorageAmount >= requiredGems.Value));


        if (hasEnougGems && hasEnoughMaterials && DrillList[IndexValue].Upgrade == 0)
        {
            foreach (var material in requiredMaterials)
            {
                var item = Minerscript.Materials.First(m => m.Name == material.Key);
                item.StorageAmount -= material.Value;
            }
            foreach (var gem in requiredGems)
            {
                var Ores = RNGscript.allOres.First(G => G.Name == gem.Key);
                Ores.StorageAmount -= gem.Value;
            }
            DrillList[IndexValue].MaxTime -= 15;
            DrillList[IndexValue].Upgrade += 1;
            UpgradePanel.SetActive(false);
        }
        else
        {
            FailedRequirements = true;
            UpgradePanel.SetActive(false);
        }
    }

    private void CheckUpgrade2()
    {
        var requiredMaterials2 = new Dictionary<string, int>
        {
            { "DrillHead", 1 },
            { "Motor", 20 },
            { "Wires", 5 }
        };

        var requiredGems2 = new Dictionary<string, int>
        {
            { "Titanium", 100 },
            { "Diamond", 10 },
            { "Copper", 200 }
        };

        bool hasEnoughMaterials2 = requiredMaterials2.All(material =>
            Minerscript.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));

        bool hasEnougGems2 = requiredGems2.All(requiredGems =>
            RNGscript.allOres.Any(G => G.Name == requiredGems.Key && G.StorageAmount >= requiredGems.Value));

        if (hasEnougGems2 && hasEnoughMaterials2 && DrillList[IndexValue].Upgrade == 1)
        {
            foreach (var material in requiredMaterials2)
            {
                var item = Minerscript.Materials.First(m => m.Name == material.Key);
                item.StorageAmount -= material.Value;
            }
            foreach (var gem in requiredGems2)
            {
                var Ores = RNGscript.allOres.First(G => G.Name == gem.Key);
                Ores.StorageAmount -= gem.Value;
            }
            DrillList[IndexValue].MaxTime -= 15;
            DrillList[IndexValue].Upgrade += 1;
            UpgradePanel.SetActive(false);
        }
        else
        {
            FailedRequirements = true;
            UpgradePanel.SetActive(false);
        }
    }
    public void Update()
    {
        for (int i = 0; i < DrillList.Count; i++)
        {
            if (DrillList[i].IsActive)
            {
                DrillList[i].Timer -= Time.deltaTime;
                DrillList[i].Text.text = DrillList[i].Timer.ToString("F0");
            }

            if (DrillList[i].Timer <= 0)
            {
                if (DrillList[i].AutoSell)
                {
                    MoneyLogic.Money += DrillList[i].OrePrice * (RNGscript.MoneyMultiplier * 1.5f);
                }
                else
                {
                    RNGscript.allOres[i].StorageAmount += 1;
                    OreStorage.UpdateInventory();
                }
                DrillList[i].Timer = DrillList[i].MaxTime;
            }

        }

        if (FailedRequirements)
        {
            NotEnough.text = "Not enough materials";
            timer += Time.deltaTime;
        }

        if (HasNoUpgrades)
        {
            NotEnough.text = "No upgrades available";
            timer += Time.deltaTime;
        }

        if (timer >= 5)
        {
            NotEnough.text = "";
            FailedRequirements = false;
            HasNoUpgrades = false;
            timer = 0;
        }

       
    }

    private void LoadAllData()
    {
        for (int i = 0; i < DrillList.Count; i++)
        {
            if (DrillList[i].IsUnlocked)
            {
                Minerscript.UnlockPanels[i].SetActive(false);
            }

            if (DrillList[i].AutoSell)
            {
                DrillList[i].Active.text = "on";
            }

            if (DrillList[i].IsActive == false)
            {
                DrillList[i].Text.text = "Drill off";
            }  
        }
    }

    public void ActivateDrill()
    {
        for (int i = 0; i < DrillList.Count; i++)
        {
            int index = i;
            DrillList[index].Activate.onClick.RemoveAllListeners();
            DrillList[index].Activate.onClick.AddListener(() =>
            {
                IndexValueActivate = index;
                if (DrillList[IndexValueActivate].IsActive)
                {
                    DrillList[IndexValueActivate].IsActive = false;
                    DrillList[IndexValueActivate].Text.text = "Drill off";
                }
                else
                {
                    DrillList[IndexValueActivate].IsActive = true;
                }
            });
        }  
    }

    public void AutoSell()
    {
        for (int i = 0; i < DrillList.Count; i++)
        {
            int index = i;
            DrillList[index].AutoSellButton.onClick.RemoveAllListeners();
            DrillList[index].AutoSellButton.onClick.AddListener(() =>
            {
                IndexValueAutosell = index;
                if (DrillList[IndexValueAutosell].AutoSell)
                {
                    DrillList[IndexValueAutosell].AutoSell = false;
                    DrillList[IndexValueAutosell].Active.text = "off";
                }
                else
                {
                    DrillList[IndexValueAutosell].Active.text = "on";
                    DrillList[IndexValueAutosell].AutoSell = true;
                }
            });
        }
    }


    private void Start()
    {
        GetUpgrade();
        ActivateDrill();
        AutoSell();
    }
}
