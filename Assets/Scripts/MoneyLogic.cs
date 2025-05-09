using System.Collections.Generic;
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
    public int CurrentUpgrade; //speed, amount, strenght, auto, luck, money, storage
    public int UpgradeStage; // 0, 1, 2, 3, 4, 5, 6, 7
    public int MaxRuntime = 300;
    public int MaxCooldown = 600;

    public float Money;
    public float Cooldown = 600;
    public float Runtime = 300;

    public string UpgradeName = "Speed Upgrade 1";
    public bool AutoRoll;
    public bool BoughtAutoRoll;
    public bool enableRuntime;
    public bool enableCooldown;
    public bool hasNoCooldown;

    public Text AutoDrillText;
    public Text Title;
    public Text Requirements;
    public Text Information;
    public Text UpgradeText;
    public Text CurrentMoney;
    public Text ErrorText;

    public RNGscript RNGscript;
    public OreStorage OreStorage;
    public Minerscript Minerscript;
    public CraftingRecipes CraftingRecipes;
    public XPScript XPScript;
    public TutorialManager TutorialManager;

    public GameObject ErrorPanel;
    public GameObject AutoRollButton;
    public GameObject CooldownPanel;

    public Image UpgradeImage;
    public Image AutoSellButton;
    public Image AutoRollButtonColor;

    public List<Sprite> sprites = new List<Sprite>();
    private List<string> Titles = new List<string> {"Pickaxe speed", "Mining amount", "Pickaxe strenght", "Auto mine", "Luck percentage","Luck multiplier", "Money multiplier", "Storage amount"};
    private List<string> InformationText = new List<string> { "Increases the speed of the stone breaking animation", "Increases the amount of gems you mine at once", "Skips parts of the breaking animation", "Automatically mines Gems", "Increases you luck", "Multiplies your luck percentage","Gives you more money per gem sold", "Increases the amount of gems you can store" };

    // add other upgrades here
    public Dictionary<string, Dictionary<string, int>> SpeedUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Speed Upgrade 1", new Dictionary<string, int> { {"Stone Gem", 5}, {"Rusty Gem", 1} } },
        { "Speed Upgrade 2", new Dictionary<string, int> { {"Stone Gem", 10}, {"Rusty Gem", 5}, { "WoodFrame", 5} } },
        { "Speed Upgrade 3", new Dictionary<string, int> { {"WoodFrame", 10 }, {"Copper", 1}, {"Clean Gem", 1} } },
        { "Speed Upgrade 4", new Dictionary<string, int> { {"Fast Gem", 1 }, {"SteelFrame", 5}, {"Petroleum", 5}, { "Copper", 5 } } },
        { "Speed Upgrade 5", new Dictionary<string, int> { {"CircuitBoard", 1 }, {"SteelFrame", 5}, {"Petroleum", 5}, {"Steel", 10 }, {"Plastic", 10 } } },
        { "Speed Upgrade 6", new Dictionary<string, int> { } }
    };
    public Dictionary<string, Dictionary<string, int>> AmountUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Amount Upgrade 1", new Dictionary<string, int> { {"Iron", 25}, {"Wood", 40} } },
        { "Amount Upgrade 2", new Dictionary<string, int> { {"Steel", 10}, {"Iron", 75}, { "WoodFrame", 15} } },
        { "Amount Upgrade 3", new Dictionary<string, int> { {"Titanium", 2 }, {"SteelFrame", 15}, {"Stone", 250}, { "Petroleum", 10 } } },
        { "Amount Upgrade 4", new Dictionary<string, int> { {"Tungsten", 1 }, {"Titanium", 10}, {"SteelFrame", 20}, {"Stone", 250 } } },
        { "Amount Upgrade 5", new Dictionary<string, int> { { "Tungsten", 1 }, { "Titanium", 25 }, { "Stone", 250 }, {"Duplicator", 1 } } },
        { "Amount Upgrade 6", new Dictionary<string, int> { } }
    };
    public Dictionary<string, Dictionary<string, int>> StrenghtUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Strenght Upgrade 1", new Dictionary<string, int> { {"Hardened Steel", 5}, {"Heat Gem", 10}, { "Iron", 25 } } },
        { "Strenght Upgrade 2", new Dictionary<string, int> { {"Hardened Steel", 10}, {"Titanium", 1}, { "Iron", 50} } },
        { "Strenght Upgrade 3", new Dictionary<string, int> { { "Hardened Steel", 20 }, { "Titanium", 5 }, { "Iron", 75 }, { "Tungsten", 1 } } },
        { "Strenght Upgrade 4", new Dictionary<string, int> { } }
    };
    public Dictionary<string, Dictionary<string, int>> AutoDrillUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Drill Upgrade 1", new Dictionary<string, int> { { "Rusty Drill", 1}, {"Coal Generator", 1},{ "Bolts", 5 } } },
        { "Drill Upgrade 2", new Dictionary<string, int> { { "Handheld Drill", 1}, {"Small Generator", 1}, { "Bolts", 15},{ "Plastic", 5 } } },
        { "Drill Upgrade 3", new Dictionary<string, int> { {"Standing Drill", 1 }, {"Generator", 1}, {"Bolts", 20}, { "Plastic", 5 } } },
        { "Drill Upgrade 4", new Dictionary<string, int> { {"Tungsten", 1 }, {"Bolts", 25}, {"Plastic", 5}, {"Wires", 15 } } },
        { "Drill Upgrade 5", new Dictionary<string, int> { { "Diamond", 1 }, { "Wires", 30 }, { "Plastic", 5 }, {"Bolts", 50 }, { "Nuclear Generator", 1 }, { "DrillHead", 1 } } },
        { "Drill Upgrade 6", new Dictionary<string, int> { } }
    };

    public Dictionary<string, Dictionary<string, int>> LuckPercentUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Luck Percent Upgrade 1", new Dictionary<string, int> { {"Sand", 10}, {"Iron", 5},{ "Coal", 5 } } },
        { "Luck Percent Upgrade 2", new Dictionary<string, int> { {"Glass", 5}, {"Iron", 10}, { "WoodFrame", 5} } },
        { "Luck Percent Upgrade 3", new Dictionary<string, int> { {"WoodFrame", 10 }, {"Stone Gem", 10}, {"Iron", 25}, { "Glass", 10 } } },
        { "Luck Percent Upgrade 4", new Dictionary<string, int> { {"WoodFrame", 20 }, {"Iron", 40}, {"Glass", 10}, {"Shiny Gem", 1 }, { "Steel", 1 } } },
        { "Luck Percent Upgrade 5", new Dictionary<string, int> { { "SteelFrame", 5 }, { "Iron", 10 }, { "Glass", 20 }, {"Shiny Gem", 2 } } },
        { "Luck Percent Upgrade 6", new Dictionary<string, int> { { "SteelFrame", 10 }, { "Iron", 20 }, { "Glass", 25 }, {"Shiny Gem", 5 }, {"Quartz" ,1 } } },
        { "Luck Percent Upgrade 7", new Dictionary<string, int> { { "SteelFrame", 15 }, { "Wires", 5 }, { "Glass", 30 }, {"Shiny Gem", 10 }, { "Quartz", 1 } } },
        { "Luck Percent Upgrade 8", new Dictionary<string, int> { { "SteelFrame", 20 }, { "Wires", 10 }, { "Clear Gem", 1 }, {"Shiny Gem", 20 }, { "Quartz", 5 } } },
        { "Luck Percent Upgrade 9", new Dictionary<string, int> { { "SteelFrame", 30 }, { "Wires", 20 }, { "Uranium", 1 }, {"Shiny Gem", 30 }, { "Quartz", 5 } } },
        { "Luck Percent Upgrade 10", new Dictionary<string, int> { { "SteelFrame", 40 }, { "Wires", 30 }, { "Uranium", 1 }, {"Emerald", 1 }, { "Quartz", 10 } } },
        { "Luck Percent Upgrade 11", new Dictionary<string, int> { } }
    };
    public Dictionary<string, Dictionary<string, int>> LuckMultUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Luck multiplier Upgrade 1", new Dictionary<string, int> { {"Iron", 20}, {"Stone", 50},{ "Coal", 5 } } },
        { "Luck multiplier Upgrade 2", new Dictionary<string, int> { {"Bolts", 15}, {"Iron", 10}, { "Coal", 25}, { "Yellow Gem", 1 } } },
        { "Luck multiplier Upgrade 3", new Dictionary<string, int> { {"Steel", 10 }, {"Iron", 15}, { "Coal", 35 }, { "Yellow Gem", 5 }, { "Bolts", 20 } } },
        { "Luck multiplier Upgrade 4", new Dictionary<string, int> { {"Steel", 20 }, {"Iron", 30}, {"Yellow Gem", 10}, {"Amethyst", 1 } } },
        { "Luck multiplier Upgrade 5", new Dictionary<string, int> { { "Yellow Gem", 15 }, { "Steel", 25 }, { "Iron", 30 }, { "Gold", 1 }, {"Amethyst", 1 } } },
        { "Luck multiplier Upgrade 6", new Dictionary<string, int> { } }
    };
    public Dictionary<string, Dictionary<string, int>> MoneyUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Money multiplier Upgrade 1", new Dictionary<string, int> { {"Iron", 10}, {"Sand", 5},{ "Wood", 15 },{"Coal",3 } } },
        { "Money multiplier Upgrade 2", new Dictionary<string, int> { {"Bolts", 5}, {"Iron", 20}, { "Coal", 10}, { "Rusty Gem", 5 } } },
        { "Money multiplier Upgrade 3", new Dictionary<string, int> { {"Steel", 5 }, {"Iron", 30}, {"Petroleum", 2}, { "Coal", 25 } } },
        { "Money multiplier Upgrade 4", new Dictionary<string, int> { {"Steel", 20 }, {"Iron", 40}, {"Petroleum", 10}, {"Silver", 1 } } },
        { "Money multiplier Upgrade 5", new Dictionary<string, int> { { "Yellow Gem", 5 }, { "Steel", 25 }, { "Iron", 60 }, {"Silver", 5 }, { "Water Gem", 5 } } },
        { "Money multiplier Upgrade 6", new Dictionary<string, int> { } }
    };
    public Dictionary<string, Dictionary<string, int>> StorageUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Storage Upgrade 1", new Dictionary<string, int> { {"WoodFrame", 20}, {"Iron", 20},{ "Steel", 5 } } },
        { "Storage Upgrade 2", new Dictionary<string, int> { {"SteelFrame", 15}, {"WoodFrame", 40}, { "Glass", 20}, { "Titanium", 10 }, { "Tungsten", 5 } } },
        { "Storage Upgrade 3", new Dictionary<string, int> { {"SteelFrame", 30 }, {"Glass", 35}, {"WoodFrame", 10}, { "Titanium", 25 }, { "Tungsten", 10 }, { "Magnetite", 1 } } },
        { "Storage Upgrade 4", new Dictionary<string, int> { {"SteelFrame", 40 }, {"Glass", 50}, {"Hardened Steel", 10}, { "Titanium", 50 }, { "Tungsten", 25 }, { "Magnetite", 1 } } },
        { "Storage Upgrade 5", new Dictionary<string, int> { {"SteelFrame", 50 }, {"Glass", 75}, {"Hardened Steel", 15}, { "Titanium", 100 }, { "Tungsten", 35 }, { "Magnetite", 3 } } },
        { "Storage Upgrade 6", new Dictionary<string, int> { {"SteelFrame", 60 }, {"Glass", 100}, {"Hardened Steel", 25}, { "Titanium", 150 }, { "Tungsten", 50 }, { "Magnetite", 5 }, { "Obsidian", 1 } } },
        { "Storage Upgrade 7", new Dictionary<string, int> { {"Requires Rebirth", 1 } } },
        { "Storage Upgrade 8", new Dictionary<string, int> { {"Requires Rebirth", 2 } } },
        { "Storage Upgrade 9", new Dictionary<string, int> { } }
    };

    // saving data and getting saved data
    public void LoadData(GameData data)
    {
        this.BoughtAutoRollUpgrade = data.BoughtAutoRollUpgrade;
        this.BoughtRollSpeed = data.BoughtRollSpeed;
        this.BoughtMoneyMultiplier = data.BoughtMoneyMultiplier;
        this.BoughtLuckPercentage = data.BoughtLuckPercentage;
        this.BoughtLuckMultiplier = data.BoughtLuckMultiplier;
        this.BoughtRollAmount = data.BoughtRollAmount;
        this.BoughtRollSkips = data.BoughtRollSkips;
        this.BoughtAutoRoll = data.BoughtAutoRoll;
        this.BoughtStorageAmount = data.BoughtStorageAmount;

        this.enableRuntime = data.enableRuntime;
        this.enableCooldown = data.enableCooldown;
        this.hasNoCooldown = data.hasNoCooldown;
        this.MaxRuntime = data.MaxRuntime;
        this.MaxCooldown = data.MaxCooldown;
        this.Runtime = data.Runtime;
        this.Cooldown = data.Cooldown;
        this.Money = data.Money;
        CurrentMoney.text = "$" + Money.ToString("F2");        
        UpgradeName = "Speed Upgrade 1";
        LoadDrill();
    }
    public void SaveData(ref GameData data)
    {
        data.BoughtAutoRollUpgrade = this.BoughtAutoRollUpgrade;
        data.BoughtRollSpeed = this.BoughtRollSpeed;
        data.BoughtMoneyMultiplier = this.BoughtMoneyMultiplier;
        data.BoughtLuckPercentage = this.BoughtLuckPercentage;
        data.BoughtLuckMultiplier = this.BoughtLuckMultiplier;
        data.BoughtRollAmount = this.BoughtRollAmount;
        data.BoughtRollSkips = this.BoughtRollSkips;
        data.BoughtAutoRoll = this.BoughtAutoRoll;
        data.BoughtStorageAmount = this.BoughtStorageAmount;

        data.enableRuntime = this.enableRuntime;
        data.enableCooldown = this.enableCooldown;
        data.hasNoCooldown = this.hasNoCooldown;
        data.MaxRuntime = this.MaxRuntime;
        data.MaxCooldown = this.MaxCooldown;
        data.Runtime = this.Runtime;
        data.Cooldown = this.Cooldown;
        data.Money = this.Money;
    }
    // end saving and getting saved data

    // get all the upgrades
    private void Costs()
    {
        switch (CurrentUpgrade)
        {
            case 0:
                switch (BoughtRollSpeed)
                {
                    case 0: UpgradeText.text = "$25"; UpgradeCost = 25; break;
                    case 1: UpgradeText.text = "$150"; UpgradeCost = 150; break;
                    case 2: UpgradeText.text = "$500"; UpgradeCost = 500; break;
                    case 3: UpgradeText.text = "$5.000"; UpgradeCost = 5000; break;
                    case 4: UpgradeText.text = "$10.000"; UpgradeCost = 10000; break;
                    case 5: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 1:
                switch (BoughtRollAmount)
                {
                    case 0: UpgradeText.text = "$250"; UpgradeCost = 250; break;
                    case 1: UpgradeText.text = "$1.500"; UpgradeCost = 1500; break;
                    case 2: UpgradeText.text = "$5.000"; UpgradeCost = 4000; break;
                    case 3: UpgradeText.text = "$7.500"; UpgradeCost = 7500; break;
                    case 4: UpgradeText.text = "$15.000"; UpgradeCost = 15000; break;
                    case 5: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 2:
                switch (BoughtRollSkips)
                {
                    case 0: UpgradeText.text = "$1.000"; UpgradeCost = 1000; break;
                    case 1: UpgradeText.text = "$2.250"; UpgradeCost = 2250; break;
                    case 2: UpgradeText.text = "$7.500"; UpgradeCost = 7500; break;
                    case 3: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 3:
                switch (BoughtAutoRollUpgrade)
                {
                    case 0: UpgradeText.text = "$250"; UpgradeCost = 250; break;
                    case 1: UpgradeText.text = "$1.000"; UpgradeCost = 1000; break;
                    case 2: UpgradeText.text = "$4.500"; UpgradeCost = 4500; break;
                    case 3: UpgradeText.text = "$9.500"; UpgradeCost = 9500; break;
                    case 4: UpgradeText.text = "$12.500"; UpgradeCost = 12500; break;
                    case 5: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 4:
                switch (BoughtLuckPercentage)
                {
                    case 0: UpgradeText.text = "$20"; UpgradeCost = 20; break;
                    case 1: UpgradeText.text = "$100"; UpgradeCost = 100; break;
                    case 2: UpgradeText.text = "$250"; UpgradeCost = 250; break;
                    case 3: UpgradeText.text = "$500"; UpgradeCost = 500; break;
                    case 4: UpgradeText.text = "$950"; UpgradeCost = 950; break;
                    case 5: UpgradeText.text = "$1.500"; UpgradeCost = 1500; break;
                    case 6: UpgradeText.text = "$3.250"; UpgradeCost = 3250; break;
                    case 7: UpgradeText.text = "$4.500"; UpgradeCost = 4500; break;
                    case 8: UpgradeText.text = "$6.750"; UpgradeCost = 6750; break;
                    case 9: UpgradeText.text = "$10.000"; UpgradeCost = 10000; break;
                    case 10: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 5:
                switch (BoughtLuckMultiplier)
                {
                    case 0: UpgradeText.text = "$250"; UpgradeCost = 250; break;
                    case 1: UpgradeText.text = "$650"; UpgradeCost = 650; break;
                    case 2: UpgradeText.text = "$1.150"; UpgradeCost = 1150; break;
                    case 3: UpgradeText.text = "$3.450"; UpgradeCost = 3450; break;
                    case 4: UpgradeText.text = "$7.550"; UpgradeCost = 7550; break;
                    case 5: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 6:
                switch (BoughtMoneyMultiplier)
                {
                    case 0: UpgradeText.text = "$30"; UpgradeCost = 30; break;
                    case 1: UpgradeText.text = "$125"; UpgradeCost = 125; break;
                    case 2: UpgradeText.text = "$675"; UpgradeCost = 675; break;
                    case 3: UpgradeText.text = "$1.150"; UpgradeCost = 1150; break;
                    case 4: UpgradeText.text = "$3.560"; UpgradeCost = 3560; break;
                    case 5: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 7:
                switch (BoughtStorageAmount)
                {
                    case 0: UpgradeText.text = "$2.500"; UpgradeCost = 2500; break;
                    case 1: UpgradeText.text = "$15.000"; UpgradeCost = 15000; break;
                    case 2: UpgradeText.text = "$350.000"; UpgradeCost = 350000; break;
                    case 3: UpgradeText.text = "$1.5000.000"; UpgradeCost = 1500000; break;
                    case 4: UpgradeText.text = "$45.000.000"; UpgradeCost = 45000000; break;
                    case 5: UpgradeText.text = "$100.000.000"; UpgradeCost = 100000000; break;
                    case 6: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
        }
        UpdateUpgradeUI();
    }

    private void GetName()
    {
        switch (CurrentUpgrade)
        {
            case 0: UpgradeName = SpeedUpgrades.ElementAt(UpgradeStage).Key; break;
            case 1: UpgradeName = AmountUpgrades.ElementAt(UpgradeStage).Key; break;
            case 2: UpgradeName = StrenghtUpgrades.ElementAt(UpgradeStage).Key; break; 
            case 3: UpgradeName = AutoDrillUpgrades.ElementAt(UpgradeStage).Key; break;
            case 4: UpgradeName = LuckPercentUpgrades.ElementAt(UpgradeStage).Key; break;
            case 5: UpgradeName = LuckMultUpgrades.ElementAt(UpgradeStage).Key; break;
            case 6: UpgradeName = MoneyUpgrades.ElementAt(UpgradeStage).Key; break;
            case 7: UpgradeName = StorageUpgrades.ElementAt(UpgradeStage).Key; break;
        }

    }
    public void UpdateEverything()
    {
        GetName();
        UpdateStage();
        Costs();
        UpdateUpgradeUI();
    }
    public void BuyUpgrade()
    {
        UpdateStage();
        CheckUpgrade(UpgradeName);
    }

    private void PurchaseSpeed(string UpgradeName)
    {
        if (SpeedUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = SpeedUpgrades[UpgradeName];

            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));

            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtRollSpeed <= (SpeedUpgrades.Count - 1))
            {
                Money -= UpgradeCost;
                RNGscript.RollSpeed -= 0.05f;
                BoughtRollSpeed++;
                CurrentMoney.text = "$" + Money.ToString("F2");
                UpdateUpgradeUI();
                Costs();
                UpdateStage();
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.green;
                ErrorText.text = "Purchase Successful";
                foreach (var material in Upgrade)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
            }
            else if (hasEnoughMaterials == false)
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough materials";
            }
            else
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough money";
            }
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Upgradename doesn't match";
        }
    }
    private void PurchaseAmount(string UpgradeName)
    {
        if (AmountUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = AmountUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtRollAmount <= (AmountUpgrades.Count - 1))
            {
                Money -= UpgradeCost;
                RNGscript.cardLimit++;
                BoughtRollAmount++;
                RNGscript.RollForHand();
                CurrentMoney.text = "$" + Money.ToString("F2");
                UpdateUpgradeUI();
                Costs();
                UpdateStage();
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.green;
                ErrorText.text = "Purchase Successful";
                foreach (var material in Upgrade)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
            }
            else if (hasEnoughMaterials == false)
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough materials";
            }
            else
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough money";
            }
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Upgradename doesn't match";
        }
    }
    private void PurchaseStrenght(string UpgradeName)
    {
        if (StrenghtUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = StrenghtUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtRollSkips <= (StrenghtUpgrades.Count - 1))
            {
                Money -= UpgradeCost;
                BoughtRollSkips++;
                RNGscript.StoneStatus--;
                RNGscript.RollSkips--;
                CurrentMoney.text = "$" + Money.ToString("F2");
                UpdateUpgradeUI();
                Costs();
                UpdateStage();
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.green;
                ErrorText.text = "Purchase Successful";
                foreach (var material in Upgrade)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
            }
            else if (hasEnoughMaterials == false)
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough materials";
            }
            else
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough money";
            }
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Upgradename doesn't match";
        }
    }
    private void PurchaseAutoDrill(string UpgradeName)
    {
        if (AutoDrillUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = AutoDrillUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtAutoRollUpgrade <= (AutoDrillUpgrades.Count - 1))
            {
                Money -= UpgradeCost;
                if (!AutoRoll && BoughtAutoRollUpgrade == 0)
                {
                    BoughtAutoRollUpgrade++;
                    RNGscript.AutoTimer = false;
                    AutoRoll = true;
                    BoughtAutoRoll = true;
                    AutoRollButton.SetActive(true);
                    RNGscript.RollButton.SetActive(false);
                    enableRuntime = true;
                    AutoRollButtonColor.color = Color.green;
                }
                else
                {
                    BoughtAutoRollUpgrade++;
                    switch (BoughtAutoRollUpgrade)
                    {
                        case 1: MaxCooldown = 600; MaxRuntime = 300; Cooldown = 600; Runtime = 300; break;
                        case 2: MaxCooldown = 900; MaxRuntime = 900; Cooldown = 900; Runtime = 900; break;
                        case 3: MaxCooldown = 600; MaxRuntime = 1200; Cooldown = 600; Runtime = 1200; break;
                        case 4: MaxCooldown = 300; MaxRuntime = 1500; Cooldown = 300; Runtime = 1500; break;
                        case 5: hasNoCooldown = true; NoCooldown(); break;
                    }
                }
                CurrentMoney.text = "$" + Money.ToString("F2");
                UpdateUpgradeUI();
                Costs();
                UpdateStage();
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.green;
                ErrorText.text = "Purchase Successful";
                foreach (var material in Upgrade)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
            }
            else if (hasEnoughMaterials == false)
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough materials";
            }
            else
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough money";
            }
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Upgradename doesn't match";
        }
    }
    private void PurchaseLuckPercent(string UpgradeName)
    {
        if (LuckPercentUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = LuckPercentUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtLuckPercentage <= (LuckPercentUpgrades.Count - 1))
            {
                Money -= UpgradeCost;
                BoughtLuckPercentage++;
                RNGscript.LuckPercentage += 0.15f;
                CurrentMoney.text = "$" + Money.ToString("F2");
                UpdateUpgradeUI();
                Costs();
                UpdateStage();
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.green;
                ErrorText.text = "Purchase Successful";
                foreach (var material in Upgrade)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
            }
            else if (hasEnoughMaterials == false)
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough materials";
            }
            else
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough money";
            }
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Upgradename doesn't match";
        }
    }
    private void PurchaseLuckMultiplier(string UpgradeName)
    {
        if (LuckMultUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = LuckMultUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtLuckMultiplier <= (LuckMultUpgrades.Count - 1))
            {
                Money -= UpgradeCost;
                BoughtLuckMultiplier++;
                RNGscript.LuckMultiplier += 0.2f;
                CurrentMoney.text = "$" + Money.ToString("F2");
                UpdateUpgradeUI();
                Costs();
                UpdateStage();
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.green;
                ErrorText.text = "Purchase Successful";
                foreach (var material in Upgrade)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
            }
            else if (hasEnoughMaterials == false)
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough materials";
            }
            else
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough money";
            }
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Upgradename doesn't match";
        }
    }
    private void PurchaseMoneyMultiplier(string UpgradeName)
    {
        if (MoneyUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = MoneyUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtMoneyMultiplier <= (MoneyUpgrades.Count - 1))
            {
                Money -= UpgradeCost;
                BoughtMoneyMultiplier++;
                RNGscript.MoneyMultiplier += 0.2f;
                CurrentMoney.text = "$" + Money.ToString("F2");
                UpdateUpgradeUI();
                Costs();
                UpdateStage();
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.green;
                ErrorText.text = "Purchase Successful";
                foreach (var material in Upgrade)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
            }
            else if (hasEnoughMaterials == false)
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough materials";
            }
            else
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough money";
            }
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Upgradename doesn't match";
        }
    }
    private void PurchaseStorageAmount(string UpgradeName)
    {
        if (StorageUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = StorageUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtStorageAmount <= (StorageUpgrades.Count - 1))
            {
                Money -= UpgradeCost;
                BoughtStorageAmount++;

                OreStorage.MaxCommonOres += 250;
                OreStorage.MaxUncommonOres += 250;
                OreStorage.MaxRareOres += 125;
                OreStorage.MaxEpicOres += 50;
                OreStorage.MaxLegendaryOres += 40;
                OreStorage.MaxMythicOres += 30;
                OreStorage.MaxExoticOres += 20;
                OreStorage.MaxDivineOres += 10;

                CurrentMoney.text = "$" + Money.ToString("F2");
                UpdateUpgradeUI();
                Costs();
                UpdateStage();
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.green;
                ErrorText.text = "Purchase Successful";
                foreach (var material in Upgrade)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
            }
            else if (hasEnoughMaterials == false)
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough materials";
            }
            else
            {
                ErrorPanel.SetActive(true);
                ErrorPanel.GetComponent<Image>().color = Color.red;
                ErrorText.text = "Not enough money";
            }
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Upgradename doesn't match";
        }
    }


    private void CheckUpgrade(string UpgradeName)
    {
        UpdateStage();
        if (SpeedUpgrades.ContainsKey(UpgradeName))
        {
            if (UpgradeName == "Speed Upgrade 1" && !TutorialManager.ClaimedReward1)
            {
                TutorialManager.TutorialText.text = "Claim your reward first";
            }
            else
            {
                PurchaseSpeed(UpgradeName);
            }
        }
        else if (AmountUpgrades.ContainsKey(UpgradeName))
        {
            PurchaseAmount(UpgradeName);
        }
        else if (StrenghtUpgrades.ContainsKey(UpgradeName))
        {
            PurchaseStrenght(UpgradeName);
        }
        else if (AutoDrillUpgrades.ContainsKey(UpgradeName))
        {
            PurchaseAutoDrill(UpgradeName);
        }
        else if (LuckPercentUpgrades.ContainsKey(UpgradeName))
        {
            PurchaseLuckPercent(UpgradeName);
        }
        else if (LuckMultUpgrades.ContainsKey(UpgradeName))
        {
            PurchaseLuckMultiplier(UpgradeName);
        }
        else if (MoneyUpgrades.ContainsKey(UpgradeName))
        {
            PurchaseMoneyMultiplier(UpgradeName);
        }
        else if (StorageUpgrades.ContainsKey(UpgradeName))
        {
            PurchaseStorageAmount(UpgradeName);
        }
        else if (UpgradeName == "")
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Upgrade is already maxed out";
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Name doesn't match any upgrade";
        }
    }
    private void Start()
    {
        Costs();
    }
    private void LoadDrill()
    {
        if (BoughtAutoRoll)
        {
            AutoRollButton.SetActive(true);
        }
        if (enableCooldown)
        {
            CooldownPanel.SetActive(true);
        }
    }
    private void NoCooldown()
    {
        RNGscript.AutoTimer = RNGscript.StartTimer;
        AutoRoll = true;
        AutoRollButton.SetActive(true);
        RNGscript.RollButton.SetActive(false);
        CooldownPanel.SetActive(false);
        AutoDrillText.text = "Inf";
        enableRuntime = true;
        enableCooldown = false;
        AutoRollButtonColor.color = Color.green;
    }
    private void Update()
    {
        if (!hasNoCooldown)
        {
            if (AutoRoll)
            {
                if (enableRuntime)
                {
                    int minutes = Mathf.FloorToInt(Runtime / 60);
                    int seconds = Mathf.FloorToInt(Runtime % 60);
                    AutoDrillText.text = $"{minutes:D2}:{seconds:D2}";
                    Runtime -= Time.deltaTime;
                    if (Runtime <= 0)
                    {
                        Runtime = MaxRuntime;
                        enableCooldown = true;
                        enableRuntime = false;
                        AutoRoll = false;
                        RNGscript.AutoTimer = !RNGscript.StartTimer;
                        CooldownPanel.SetActive(true);
                        RNGscript.RollButton.SetActive(true);
                    }
                }
            }
            if (enableCooldown)
            {
                int minutes = Mathf.FloorToInt(Cooldown / 60);
                int seconds = Mathf.FloorToInt(Cooldown % 60);
                AutoDrillText.text = $"{minutes:D2}:{seconds:D2}";
                Cooldown -= Time.deltaTime;
                if (Cooldown <= 0)
                {
                    Cooldown = MaxCooldown;
                    enableRuntime = true;
                    enableCooldown = false;
                    CooldownPanel.SetActive(false);
                    RNGscript.AutoTimer = RNGscript.StartTimer;
                    RNGscript.RollButton.SetActive(false);
                    AutoRoll = true;
                    AutoRollButtonColor.color = Color.green;
                }
            }
        }
    }
    public void CheckDrillStatus()
    {
        if (AutoRoll)
        {
            RNGscript.AutoTimer = RNGscript.StartTimer;
        }
    }
    private void UpdateStage()
    {
        if (SpeedUpgrades.ContainsKey(UpgradeName))
        {
            switch (BoughtRollSpeed)
            {
                case 0: UpgradeName = SpeedUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                case 1: UpgradeName = SpeedUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                case 2: UpgradeName = SpeedUpgrades.ElementAt(2).Key; UpgradeStage = 2; break;
                case 3: UpgradeName = SpeedUpgrades.ElementAt(3).Key; UpgradeStage = 3; break;
                case 4: UpgradeName = SpeedUpgrades.ElementAt(4).Key; UpgradeStage = 4; break;
                case 5: UpgradeName = ""; UpgradeStage = 5; break;

            }
        }
        else if (AmountUpgrades.ContainsKey(UpgradeName))
        {
            switch (BoughtRollAmount)
            {
                case 0: UpgradeName = AmountUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                case 1: UpgradeName = AmountUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                case 2: UpgradeName = AmountUpgrades.ElementAt(2).Key; UpgradeStage = 2; break;
                case 3: UpgradeName = AmountUpgrades.ElementAt(3).Key; UpgradeStage = 3; break;
                case 4: UpgradeName = AmountUpgrades.ElementAt(4).Key; UpgradeStage = 4; break;
                case 5: UpgradeName = ""; UpgradeStage = 5; break;
            }
        }
        else if (StrenghtUpgrades.ContainsKey(UpgradeName))
        {
            switch (BoughtRollSkips)
            {
                case 0: UpgradeName = StrenghtUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                case 1: UpgradeName = StrenghtUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                case 2: UpgradeName = StrenghtUpgrades.ElementAt(2).Key; UpgradeStage = 2; break;
                case 3: UpgradeName = ""; UpgradeStage = 3; break;
            }
        }
        else if (AutoDrillUpgrades.ContainsKey(UpgradeName))
        {
            switch (BoughtAutoRollUpgrade)
            {
                case 0: UpgradeName = AutoDrillUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                case 1: UpgradeName = AutoDrillUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                case 2: UpgradeName = AutoDrillUpgrades.ElementAt(2).Key; UpgradeStage = 2; break;
                case 3: UpgradeName = AutoDrillUpgrades.ElementAt(3).Key; UpgradeStage = 3; break;
                case 4: UpgradeName = AutoDrillUpgrades.ElementAt(4).Key; UpgradeStage = 4; break;
                case 5: UpgradeName = ""; UpgradeStage = 5; break;
            }
        }
        else if (LuckPercentUpgrades.ContainsKey(UpgradeName))
        {
            switch (BoughtLuckPercentage)
            {
                case 0: UpgradeName = LuckPercentUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                case 1: UpgradeName = LuckPercentUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                case 2: UpgradeName = LuckPercentUpgrades.ElementAt(2).Key; UpgradeStage = 2; break;
                case 3: UpgradeName = LuckPercentUpgrades.ElementAt(3).Key; UpgradeStage = 3; break;
                case 4: UpgradeName = LuckPercentUpgrades.ElementAt(4).Key; UpgradeStage = 4; break;
                case 5: UpgradeName = LuckPercentUpgrades.ElementAt(5).Key; UpgradeStage = 5; break;
                case 6: UpgradeName = LuckPercentUpgrades.ElementAt(6).Key; UpgradeStage = 6; break;
                case 7: UpgradeName = LuckPercentUpgrades.ElementAt(7).Key; UpgradeStage = 7; break;
                case 8: UpgradeName = LuckPercentUpgrades.ElementAt(8).Key; UpgradeStage = 8; break;
                case 9: UpgradeName = LuckPercentUpgrades.ElementAt(9).Key; UpgradeStage = 9; break;
                case 10: UpgradeName = ""; UpgradeStage = 10; break;
            }
        }
        else if (LuckMultUpgrades.ContainsKey(UpgradeName))
        {
            switch (BoughtLuckMultiplier)
            {
                case 0: UpgradeName = LuckMultUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                case 1: UpgradeName = LuckMultUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                case 2: UpgradeName = LuckMultUpgrades.ElementAt(2).Key; UpgradeStage = 2; break;
                case 3: UpgradeName = LuckMultUpgrades.ElementAt(3).Key; UpgradeStage = 3; break;
                case 4: UpgradeName = LuckMultUpgrades.ElementAt(4).Key; UpgradeStage = 4; break;
                case 5: UpgradeName = ""; UpgradeStage = 5; break;
            }
        }
        else if (MoneyUpgrades.ContainsKey(UpgradeName))
        {
            switch (BoughtMoneyMultiplier)
            {
                case 0: UpgradeName = MoneyUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                case 1: UpgradeName = MoneyUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                case 2: UpgradeName = MoneyUpgrades.ElementAt(2).Key; UpgradeStage = 2; break;
                case 3: UpgradeName = MoneyUpgrades.ElementAt(3).Key; UpgradeStage = 3; break;
                case 4: UpgradeName = MoneyUpgrades.ElementAt(4).Key; UpgradeStage = 4; break;
                case 5: UpgradeName = ""; UpgradeStage = 5; break;
            }
        }
        else if (StorageUpgrades.ContainsKey(UpgradeName))
        {
            if (XPScript.SavedRebirth == 0)
            {
                switch (BoughtStorageAmount)
                {
                    case 0: UpgradeName = StorageUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                    case 1: UpgradeName = StorageUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                    case 2: UpgradeName = "Storage Upgrade 7"; UpgradeStage = 6; break;
                }
            }
            else if (XPScript.SavedRebirth == 1)
            {
                switch (BoughtStorageAmount)
                {
                    case 0: UpgradeName = StorageUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                    case 1: UpgradeName = StorageUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                    case 2: UpgradeName = StorageUpgrades.ElementAt(2).Key; UpgradeStage = 2; break;
                    case 3: UpgradeName = StorageUpgrades.ElementAt(3).Key; UpgradeStage = 3; break;
                    case 4: UpgradeName = "Storage Upgrade 8"; UpgradeStage = 7; break;
                }
            }
            else
            {
                switch (BoughtStorageAmount)
                {
                    case 0: UpgradeName = StorageUpgrades.ElementAt(0).Key; UpgradeStage = 0; break;
                    case 1: UpgradeName = StorageUpgrades.ElementAt(1).Key; UpgradeStage = 1; break;
                    case 2: UpgradeName = StorageUpgrades.ElementAt(2).Key; UpgradeStage = 2; break;
                    case 3: UpgradeName = StorageUpgrades.ElementAt(3).Key; UpgradeStage = 3; break;
                    case 4: UpgradeName = StorageUpgrades.ElementAt(4).Key; UpgradeStage = 4; break;
                    case 5: UpgradeName = StorageUpgrades.ElementAt(5).Key; UpgradeStage = 5; break;
                    case 6: UpgradeName = ""; UpgradeStage = 8; break;
                }
            }
        }
        else
        {
            ErrorPanel.SetActive(true);
            ErrorPanel.GetComponent<Image>().color = Color.red;
            ErrorText.text = "Can't update upgrade stage";
        }
        UpdateUpgradeUI();
    }
    private void UpdateUpgradeUI()
    {
        if (TutorialManager.tutorialStep == 4)
        {
            TutorialManager.UpdateRequirementsText();
        }
        Title.text = Titles[CurrentUpgrade];
        UpgradeText.text = "$" + UpgradeCost.ToString();
        Information.text = InformationText[CurrentUpgrade];
        UpgradeImage.sprite = sprites[CurrentUpgrade];
        switch (CurrentUpgrade)
        {
            case 0:
                SetRequirementsText(SpeedUpgrades);
                break;
            case 1:
                SetRequirementsText(AmountUpgrades);
                break;
            case 2:
                SetRequirementsText(StrenghtUpgrades);
                break;
            case 3:
                SetRequirementsText(AutoDrillUpgrades);
                break;
            case 4:
                SetRequirementsText(LuckPercentUpgrades);
                break;
            case 5:
                SetRequirementsText(LuckMultUpgrades);
                break;
            case 6:
                SetRequirementsText(MoneyUpgrades);
                break;
            case 7:
                SetRequirementsText(StorageUpgrades);
                break;
            default:
                Requirements.text = "All upgrades bought";
                break;
        }
    }
    private void SetRequirementsText(Dictionary<string, Dictionary<string, int>> upgrades)
    {
        var upgrade = upgrades.ElementAt(UpgradeStage).Value;
        if (upgrade.Count == 0)
        {
            Requirements.text = "All upgrades bought";
        }
        else
        {
            Requirements.text = string.Join("\n", upgrade.Select(r => $"{r.Key}: {r.Value}"));
        }
    }
    public void SwitchUpgradeForward()
    {
        CurrentUpgrade++;
        UpgradeStage = 0;
        if (CurrentUpgrade > (Titles.Count - 1))
        {
            CurrentUpgrade = (Titles.Count - 1);
        }
        GetName();
        UpdateStage();
        Costs();
        ErrorPanel.SetActive(false);
    }
    public void SwitchUpgradeBack()
    {
        CurrentUpgrade--;
        UpgradeStage = 0;
        if (CurrentUpgrade < 0)
        {
            CurrentUpgrade = 0;
        }
        GetName();
        UpdateStage();
        Costs();
        ErrorPanel.SetActive(false);
    }
    // save this from the old code
    public void AutoRollToggle()
    {
        if (AutoRoll == false)
        {
            RNGscript.AutoTimer = RNGscript.StartTimer;
            RNGscript.RollButton.SetActive(false);
            AutoRoll = true;
            AutoRollButtonColor.color = Color.green;
            if (hasNoCooldown)
            {
                AutoDrillText.text = "Inf";
            }
        }
        else if (AutoRoll == true)
        {
            RNGscript.AutoTimer = !RNGscript.StartTimer;
            RNGscript.RollButton.SetActive(true);
            AutoRoll = false;
            AutoRollButtonColor.color = Color.red;
        }
    }
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
}