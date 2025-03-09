using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MoneyLogic : MonoBehaviour, IDataPersistence
{
    public int BoughtRollSpeed;
    public int BoughtRollAmount;
    public int BoughtRollSkips;
    public int BoughtLuckPercentage;
    public int BoughtLuckMultiplier;
    public int BoughtMoneyMultiplier;
    public int BoughtStorageAmount;
    public int BoughtAutoRollUpgrade;

    public int UpgradeCost;
    public int CurrentUpgrade;
    public int UpgradeStage;

    public float Money;
    public string UpgradeName;
    public bool AutoRoll;
    public bool BoughtAutoRoll;

    public Text UpgradeText;
    public Text CurrentMoney;

    public RNGscript RNGscript;
    public OreStorage OreStorage;
    public Minerscript Minerscript;
    public CraftingRecipes CraftingRecipes;
    public GameObject AutoRollButton;
    public XPScript XPScript;

    public Image AutoSellButton;
    public Image AutoRollButtonColor;

    // add other upgrades here
    public Dictionary<string, Dictionary<string, int>> SpeedUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Speed Upgrade 1", new Dictionary<string, int> { {"Stone Gem", 5}, {"Rusty Gem", 1} } },
        { "Speed Upgrade 2", new Dictionary<string, int> { {"Stone Gem", 10}, {"Rusty Gem", 5}, { "WoodFrame", 5} } },
        { "Speed Upgrade 3", new Dictionary<string, int> { {"WoodFrame", 10 }, {"Copper", 1}, {"Clean Gem", 1} } },
        { "Speed Upgrade 4", new Dictionary<string, int> { {"Fast Gem", 1 }, {"SteelFrame", 5}, {"Petroleum", 5}, { "Copper", 5 } } },
        { "Speed Upgrade 5", new Dictionary<string, int> { {"CircuitBoard", 1 }, {"SteelFrame", 5}, {"Petroleum", 5}, {"Steel", 10 }, {"Plastic", 10 } } }
    };


    // saving data and getting saved data
    public void LoadData(GameData data)
    {
        this.BoughtRollSpeed = data.BoughtRollSpeed;
        this.BoughtMoneyMultiplier = data.BoughtMoneyMultiplier;
        this.BoughtLuckPercentage = data.BoughtLuckPercentage;
        this.BoughtLuckMultiplier = data.BoughtLuckMultiplier;
        this.BoughtRollAmount = data.BoughtRollAmount;
        this.BoughtRollSkips = data.BoughtRollSkips;
        this.BoughtAutoRoll = data.BoughtAutoRoll;
        this.BoughtStorageAmount = data.BoughtStorageAmount;

        this.Money = data.Money;
    }
    public void SaveData(ref GameData data)
    {
        data.BoughtRollSpeed = this.BoughtRollSpeed;
        data.BoughtMoneyMultiplier = this.BoughtMoneyMultiplier;
        data.BoughtLuckPercentage = this.BoughtLuckPercentage;
        data.BoughtLuckMultiplier = this.BoughtLuckMultiplier;
        data.BoughtRollAmount = this.BoughtRollAmount;
        data.BoughtRollSkips = this.BoughtRollSkips;
        data.BoughtAutoRoll = this.BoughtAutoRoll;
        data.BoughtStorageAmount = this.BoughtStorageAmount;

        data.Money = this.Money;
    }
    // end saving and getting saved data

    // get all the upgrades
    private void SpeedCosts()
    {
        switch (BoughtRollSpeed)
        {
            case 0: UpgradeText.text = "10$"; UpgradeCost = 10; break;
            case 1: UpgradeText.text = "75$"; UpgradeCost = 75; break;
            case 2: UpgradeText.text = "250$"; UpgradeCost = 250; break;
            case 3: UpgradeText.text = "1500$"; UpgradeCost = 1500; break;
            case 4: UpgradeText.text = "5000$"; UpgradeCost = 5000; break;
            case 5: UpgradeText.text = "Purchased"; break;
        }
    }

    private void GetName()
    {
        switch (CurrentUpgrade)
        {
            case 0: UpgradeName = SpeedUpgrades.ElementAt(UpgradeStage).Key; break;
                //case 1: UpgradeName = AmountUpgrade.ElementAt(UpgradeStage).Key; break;
                //case 2: UpgradeName = StrenghtUpgrade.ElementAt(UpgradeStage).Key; break;
                //case 3: UpgradeName = AutoDrillUpgrade.ElementAt(UpgradeStage).Key; break;
                //case 4: UpgradeName = LuckPercentUpgrade.ElementAt(UpgradeStage).Key; break;
                //case 5: UpgradeName = LuckMultUpgrade.ElementAt(UpgradeStage).Key; break;
                //case 6: UpgradeName = MoneyUpgrade.ElementAt(UpgradeStage).Key; break;
                //case 7: UpgradeName = StorageUpgrade.ElementAt(UpgradeStage).Key; break;
        }
        CheckUpgrade(UpgradeName);
    }
    public void BuyUpgrade()
    {
        GetName();
    }

    private void CheckUpgrade(string UpgradeName)
    {
        if (SpeedUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = SpeedUpgrades[UpgradeName];

            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));

            if (hasEnoughMaterials && Money >= UpgradeCost)
            {
                Money -= UpgradeCost;
                RNGscript.RollSpeed -= 0.05f;
                BoughtRollSpeed++;
                CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
                SpeedCosts();
                foreach (var material in Upgrade)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
            }
            else if (hasEnoughMaterials == false)
            {
                Debug.Log("Not enough materials");
            }
            else
            {
                Debug.Log("Upgrade not found");
            }
        }
        else
        {
            Debug.Log("Upgrade not found");
        }
    }

    public void SwitchUpgrade()
    {
        CurrentUpgrade++;
        if (CurrentUpgrade > 7)
        {
            CurrentUpgrade = 0;
        }
    }

    // auto roll
    public void BuyAutoRoll()
    {
        if (StaticVariables.cash >= 500 && BoughtAutoRoll == false)
        {
            Money -= 500;
            RNGscript.AutoTimer = 0;
            AutoRoll = true;
            BoughtAutoRoll = true;
            AutoRollButton.SetActive(true);
            RNGscript.RollButton.SetActive(false);
            AutoRollButtonColor.color = Color.green;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
    // end
    public void BuyLuckPercentage()
    {
        if (StaticVariables.cash >= 50 && BoughtLuckPercentage == 0)
        {
            Money -= 50;
            RNGscript.LuckPercentage = 1.1f; // 10%
            BoughtLuckPercentage = 1;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 100 && BoughtLuckPercentage == 1)
        {
            Money -= 100;
            RNGscript.LuckPercentage = 1.2f;
            BoughtLuckPercentage = 2;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 250 && BoughtLuckPercentage == 2)
        {
            Money -= 250;
            RNGscript.LuckPercentage = 1.3f;
            BoughtLuckPercentage = 3;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 500 && BoughtLuckPercentage == 3)
        {
            Money -= 500;
            RNGscript.LuckPercentage = 1.4f;
            BoughtLuckPercentage = 4;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 1500 && BoughtLuckPercentage == 4)
        {
            Money -= 1500;
            RNGscript.LuckPercentage = 1.5f;
            BoughtLuckPercentage = 5;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 3500 && BoughtLuckPercentage == 5)
        {
            Money -= 3500;
            RNGscript.LuckPercentage = 1.65f;
            BoughtLuckPercentage = 6;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 8000 && BoughtLuckPercentage == 6)
        {
            Money -= 8000;
            RNGscript.LuckPercentage = 1.8f;
            BoughtLuckPercentage = 7;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 12500 && BoughtLuckPercentage == 7)
        {
            Money -= 12500;
            RNGscript.LuckPercentage = 2f;
            BoughtLuckPercentage = 8;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 20000 && BoughtLuckPercentage == 8)
        {
            Money -= 20000;
            RNGscript.LuckPercentage = 2.2f;
            BoughtLuckPercentage = 9;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 35000 && BoughtLuckPercentage == 9)
        {
            Money -= 35000;
            RNGscript.LuckPercentage = 2.5f;
            BoughtLuckPercentage = 10;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
    public void BuyLuckMulitplier()
    {
        if (StaticVariables.cash >= 2500 && BoughtLuckMultiplier == 0)
        {
            Money -= 2500;
            RNGscript.LuckMultiplier = 1.5f; // 1.5x luck added on top of the percentage
            BoughtLuckMultiplier = 1;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5500 && BoughtLuckMultiplier == 1)
        {
            Money -= 5500;
            RNGscript.LuckMultiplier = 2f;
            BoughtLuckMultiplier = 2;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 10000 && BoughtLuckMultiplier == 2)
        {
            Money -= 10000;
            RNGscript.LuckMultiplier = 2.5f;
            BoughtLuckMultiplier = 3;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 22000 && BoughtLuckMultiplier == 3)
        {
            Money -= 22000;
            RNGscript.LuckMultiplier = 3f;
            BoughtLuckMultiplier = 4;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 45000 && BoughtLuckMultiplier == 4)
        {
            Money -= 45000;
            RNGscript.LuckMultiplier = 4f;
            BoughtLuckMultiplier = 5;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
    public void BuyMoneyMultiplier()
    {
        if (StaticVariables.cash >= 100 && BoughtMoneyMultiplier == 0)
        {
            Money -= 100;
            RNGscript.MoneyMultiplier = 1.25f;
            BoughtMoneyMultiplier = 1;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 500 && BoughtMoneyMultiplier == 1)
        {
            Money -= 500;
            RNGscript.MoneyMultiplier = 1.5f;
            BoughtMoneyMultiplier = 2;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 1500 && BoughtMoneyMultiplier == 2)
        {
            Money -= 1500;
            RNGscript.MoneyMultiplier = 1.75f;
            BoughtMoneyMultiplier = 3;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 3000 && BoughtMoneyMultiplier == 3)
        {
            Money -= 3000;
            RNGscript.MoneyMultiplier = 2f;
            BoughtMoneyMultiplier = 4;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5000 && BoughtMoneyMultiplier == 4)
        {
            Money -= 5000;
            RNGscript.MoneyMultiplier = 2.5f;
            BoughtMoneyMultiplier = 5;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 7500 && BoughtMoneyMultiplier == 5)
        {
            Money -= 7500;
            RNGscript.MoneyMultiplier = 3f;
            BoughtMoneyMultiplier = 6;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
    public void GiveMoneyTest()
    {
        Money += 100000;
        CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
    }
    // save
    public void AutoSellActive()
    {
        if (RNGscript.AutoSell == false)
        {
            RNGscript.AutoSell = true;
            AutoSellButton.color = Color.green;
        }
        else
        {
            RNGscript.AutoSell = false;
            AutoSellButton.color = Color.red;
        }
    }
    // end
    public void BuyStorageAmount()
    {
        if (StaticVariables.cash >= 10000 && BoughtStorageAmount == 0)
        {
            Money -= 10000;
            BoughtStorageAmount = 1;
            OreStorage.MaxCommonOres = 1000;
            OreStorage.MaxUncommonOres = 500;
            OreStorage.MaxRareOres = 250;
            OreStorage.MaxEpicOres = 150;
            OreStorage.MaxLegendaryOres = 50;
            OreStorage.MaxMythicOres = 10;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 100000 && BoughtStorageAmount == 1)
        {
            Money -= 100000;
            BoughtStorageAmount = 2;
            OreStorage.MaxCommonOres = 2000;
            OreStorage.MaxUncommonOres = 1000;
            OreStorage.MaxRareOres = 500;
            OreStorage.MaxEpicOres = 300;
            OreStorage.MaxLegendaryOres = 100;
            OreStorage.MaxMythicOres = 20;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 1000000 && BoughtStorageAmount == 2 && XPScript.SavedRebirth >= 1)
        {
            Money -= 1000000;
            BoughtStorageAmount = 3;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5000000 && BoughtStorageAmount == 3 && XPScript.SavedRebirth >= 1)
        {
            Money -= 5000000;
            BoughtStorageAmount = 4;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 10000000 && BoughtStorageAmount == 4 && XPScript.SavedRebirth >= 2)
        {
            Money -= 10000000;
            BoughtStorageAmount = 5;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 50000000 && BoughtStorageAmount == 5 && XPScript.SavedRebirth >= 2)
        {
            Money -= 50000000;
            BoughtStorageAmount = 6;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
}