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

    public GameObject ErrorPanel;
    public GameObject AutoRollButton;
    public GameObject CooldownPanel;

    public Image UpgradeImage;
    public Image AutoSellButton;
    public Image AutoRollButtonColor;

    public List<Sprite> sprites = new List<Sprite>();
    private List<string> Titles = new List<string> {"Pickaxe speed", "Mining amount", "Pickaxe strenght", "Auto mine"};
    private List<string> InformationText = new List<string> { "Increases the speed of the stone breaking animation", "Increases the amount of gems you mine at once", "Skips parts of the breaking animation", "Automatically mines Gems" };

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
        { "Drill Upgrade 1", new Dictionary<string, int> { {"Small Drill", 1}, {"Coal Generator", 1},{ "Bolts", 5 } } },
        { "Drill Upgrade 2", new Dictionary<string, int> { {"Medium Drill", 1}, {"Small Generator", 1}, { "Bolts", 15},{ "Plastic", 5 } } },
        { "Drill Upgrade 3", new Dictionary<string, int> { {"Standard Drill", 1 }, {"Generator", 1}, {"Bolts", 20}, { "Plastic", 5 } } },
        { "Drill Upgrade 4", new Dictionary<string, int> { {"Tungsten", 1 }, {"Bolts", 25}, {"Plastic", 5}, {"Wires", 15 } } },
        { "Drill Upgrade 5", new Dictionary<string, int> { { "Diamond", 1 }, { "Wires", 30 }, { "Plastic", 5 }, {"Bolts", 50 }, { "Heat Generator", 1 }, { "DrillHead", 1 } } },
        { "Drill Upgrade 6", new Dictionary<string, int> { } }
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
        CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
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
                // roll speed
                switch (BoughtRollSpeed)
                {
                    case 0: UpgradeText.text = "10$"; UpgradeCost = 10; break;
                    case 1: UpgradeText.text = "75$"; UpgradeCost = 75; break;
                    case 2: UpgradeText.text = "250$"; UpgradeCost = 250; break;
                    case 3: UpgradeText.text = "1000$"; UpgradeCost = 1000; break;
                    case 4: UpgradeText.text = "3000$"; UpgradeCost = 3000; break;
                    case 5: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 1:
                // roll amount
                switch (BoughtRollAmount)
                {
                    case 0: UpgradeText.text = "250$"; UpgradeCost = 250; break;
                    case 1: UpgradeText.text = "1000$"; UpgradeCost = 1000; break;
                    case 2: UpgradeText.text = "2000$"; UpgradeCost = 2000; break;
                    case 3: UpgradeText.text = "4000$"; UpgradeCost = 4000; break;
                    case 4: UpgradeText.text = "6000$"; UpgradeCost = 6000; break;
                    case 5: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 2: switch (BoughtRollSkips)
                {
                    case 0: UpgradeText.text = "400$"; UpgradeCost = 400; break;
                    case 1: UpgradeText.text = "1000$"; UpgradeCost = 1000; break;
                    case 2: UpgradeText.text = "2250$"; UpgradeCost = 2250; break;
                    case 3: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
                }
                break;
            case 3:
                switch (BoughtAutoRollUpgrade)
                {
                    case 0: UpgradeText.text = "100$"; UpgradeCost = 100; break;
                    case 1: UpgradeText.text = "350$"; UpgradeCost = 350; break;
                    case 2: UpgradeText.text = "800$"; UpgradeCost = 800; break;
                    case 3: UpgradeText.text = "1500$"; UpgradeCost = 1500; break;
                    case 4: UpgradeText.text = "4500$"; UpgradeCost = 4500; break;
                    case 5: UpgradeText.text = "Purchased"; UpgradeCost = 0; break;
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
            case 2: UpgradeName = StrenghtUpgrades.ElementAt(UpgradeStage).Key; break; // fix this one
            case 3: UpgradeName = AutoDrillUpgrades.ElementAt(UpgradeStage).Key; break;
                //case 4: UpgradeName = LuckPercentUpgrades.ElementAt(UpgradeStage).Key; break;
                //case 5: UpgradeName = LuckMultUpgrades.ElementAt(UpgradeStage).Key; break;
                //case 6: UpgradeName = MoneyUpgrades.ElementAt(UpgradeStage).Key; break;
                //case 7: UpgradeName = StorageUpgrades.ElementAt(UpgradeStage).Key; break;
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

            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtRollSpeed <= 5)
            {
                Money -= UpgradeCost;
                RNGscript.RollSpeed -= 0.05f;
                BoughtRollSpeed++;
                CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
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
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtRollAmount <= 5)
            {
                Money -= UpgradeCost;
                RNGscript.cardLimit++;
                BoughtRollAmount++;
                RNGscript.RollForHand();
                CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
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
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtRollSkips <= 3)
            {
                Money -= UpgradeCost;
                BoughtRollSkips++;
                RNGscript.StoneStatus--;
                RNGscript.RollSkips--;
                CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
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
            if (hasEnoughMaterials && Money >= UpgradeCost && BoughtAutoRollUpgrade <= 5)
            {
                Money -= UpgradeCost;
                if (!AutoRoll && BoughtAutoRollUpgrade >= 0)
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
                        case 1: MaxCooldown = 600; MaxRuntime = 300; break;
                        case 2: MaxCooldown = 900; MaxRuntime = 900; break;
                        case 3: MaxCooldown = 300; MaxRuntime = 1200; break;
                        case 4: MaxCooldown = 300; MaxRuntime = 1500; break;
                        case 5: hasNoCooldown = true; NoCooldown(); break;
                    }
                }
                CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
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
            PurchaseSpeed(UpgradeName);
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
        AutoRollButtonColor.color = Color.green;
    }
    private void Update()
    {
        StaticVariables.cash = Money;
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
        Title.text = Titles[CurrentUpgrade];
        UpgradeText.text = UpgradeCost.ToString() + "$";
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
    // end
    public void ThisIsATest()
    {
        // luck percentage
        if (StaticVariables.cash >= 50 && BoughtLuckPercentage == 0)
        {
            Money -= 50;
            RNGscript.LuckPercentage = 1.1f; // 10%
            BoughtLuckPercentage = 1;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        // luck multiplier
        if (StaticVariables.cash >= 2500 && BoughtLuckMultiplier == 0)
        {
            Money -= 2500;
            RNGscript.LuckMultiplier = 1.5f; // 1.5x luck added on top of the percentage
            BoughtLuckMultiplier = 1;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        // money multiplier
        if (StaticVariables.cash >= 100 && BoughtMoneyMultiplier == 0)
        {
            Money -= 100;
            RNGscript.MoneyMultiplier = 1.25f;
            BoughtMoneyMultiplier = 1;
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        // storage amount
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
    }
}