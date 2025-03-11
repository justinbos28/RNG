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

    public RNGscript RNGscript;
    public OreStorage OreStorage;
    public Minerscript Minerscript;
    public CraftingRecipes CraftingRecipes;
    public GameObject AutoRollButton;
    public GameObject CooldownPanel;
    public XPScript XPScript;

    public Image UpgradeImage;
    public Image AutoSellButton;
    public Image AutoRollButtonColor;

    public List<Sprite> sprites = new List<Sprite>();
    private List<string> Titles = new List<string> {"Pickaxe speed", "Mining amount", "Pickaxe strenght", "Auto mine"};

    // add other upgrades here
    public Dictionary<string, Dictionary<string, int>> SpeedUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Speed Upgrade 1", new Dictionary<string, int> { {"Stone Gem", 5}, {"Rusty Gem", 1} } },
        { "Speed Upgrade 2", new Dictionary<string, int> { {"Stone Gem", 10}, {"Rusty Gem", 5}, { "WoodFrame", 5} } },
        { "Speed Upgrade 3", new Dictionary<string, int> { {"WoodFrame", 10 }, {"Copper", 1}, {"Clean Gem", 1} } },
        { "Speed Upgrade 4", new Dictionary<string, int> { {"Fast Gem", 1 }, {"SteelFrame", 5}, {"Petroleum", 5}, { "Copper", 5 } } },
        { "Speed Upgrade 5", new Dictionary<string, int> { {"CircuitBoard", 1 }, {"SteelFrame", 5}, {"Petroleum", 5}, {"Steel", 10 }, {"Plastic", 10 } } }
    };

    public Dictionary<string, Dictionary<string, int>> AmountUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Amount Upgrade 1", new Dictionary<string, int> { {"Iron", 50}, {"Wood", 100} } },
        { "Amount Upgrade 2", new Dictionary<string, int> { {"Steel", 10}, {"Iron", 75}, { "WoodFrame", 15} } },
        { "Amount Upgrade 3", new Dictionary<string, int> { {"Titanium", 2 }, {"SteelFrame", 15}, {"Stone", 250}, { "Petroleum", 10 } } },
        { "Amount Upgrade 4", new Dictionary<string, int> { {"Tungsten", 1 }, {"Titanium", 10}, {"SteelFrame", 20}, {"Stone", 250 } } },
        { "Amount Upgrade 5", new Dictionary<string, int> { { "Tungsten", 1 }, { "Titanium", 25 }, { "Stone", 250 }, {"Duplicator", 1 } } }
    };

    public Dictionary<string, Dictionary<string, int>> StrenghtUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Strenght Upgrade 1", new Dictionary<string, int> { {"Hardened Steel", 5}, {"Heat Gem", 10}, { "Iron", 25 } } },
        { "Strenght Upgrade 2", new Dictionary<string, int> { {"Hardened Steel", 10}, {"Titanium", 1}, { "Iron", 50} } },
        { "Strenght Upgrade 3", new Dictionary<string, int> { { "Hardened Steel", 20 }, { "Titanium", 5 }, { "Iron", 75 }, { "Tungsten", 1 } } }
    };

    public Dictionary<string, Dictionary<string, int>> AutoDrillUpgrades = new Dictionary<string, Dictionary<string, int>>
    {
        { "Drill Upgrade 1", new Dictionary<string, int> { {"Small Drill", 1}, {"Coal Generator", 1},{ "Bolts", 5 } } },
        { "Drill Upgrade 2", new Dictionary<string, int> { {"Medium Drill", 1}, {"Small Generator", 1}, { "Bolts", 15},{ "Plastic", 5 } } },
        { "Drill Upgrade 3", new Dictionary<string, int> { {"Standard Drill", 1 }, {"Generator", 1}, {"Bolts", 20}, { "Plastic", 5 } } },
        { "Drill Upgrade 4", new Dictionary<string, int> { {"Tungsten", 1 }, {"Bolts", 25}, {"Plastic", 5}, {"Wires", 15 } } },
        { "Drill Upgrade 5", new Dictionary<string, int> { { "Diamond", 1 }, { "Wires", 30 }, { "Plastic", 5 }, {"Bolts", 50 }, { "Heat Generator", 1 }, { "DrillHead", 1 } } }
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
                    case 3: UpgradeText.text = "1500$"; UpgradeCost = 1500; break;
                    case 4: UpgradeText.text = "5000$"; UpgradeCost = 5000; break;
                    case 5: UpgradeText.text = "Purchased"; break;
                }
                break;
            case 1:
                // roll amount
                switch (BoughtRollAmount)
                {
                    case 0: UpgradeText.text = "250$"; UpgradeCost = 250; break;
                    case 1: UpgradeText.text = "1000$"; UpgradeCost = 1000; break;
                    case 2: UpgradeText.text = "2500$"; UpgradeCost = 2500; break;
                    case 3: UpgradeText.text = "5000$"; UpgradeCost = 5000; break;
                    case 4: UpgradeText.text = "15000$"; UpgradeCost = 15000; break;
                    case 5: UpgradeText.text = "Purchased"; break;
                }
                break;
            case 2: switch (BoughtRollSkips)
                {
                    case 0: UpgradeText.text = "500$"; UpgradeCost = 500; break;
                    case 1: UpgradeText.text = "1000$"; UpgradeCost = 1000; break;
                    case 2: UpgradeText.text = "2500$"; UpgradeCost = 2500; break;
                    case 3: UpgradeText.text = "Purchased"; break;
                }
                break;
            case 3:
                switch (BoughtAutoRollUpgrade)
                {
                    case 0: UpgradeText.text = "500$"; UpgradeCost = 500; break;
                    case 1: UpgradeText.text = "1500$"; UpgradeCost = 1500; break;
                    case 2: UpgradeText.text = "3000$"; UpgradeCost = 3000; break;
                    case 3: UpgradeText.text = "5000$"; UpgradeCost = 5000; break;
                    case 4: UpgradeText.text = "15000$"; UpgradeCost = 15000; break;
                    case 5: UpgradeText.text = "Purchased"; break;
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
                //case 4: UpgradeName = LuckPercentUpgrades.ElementAt(UpgradeStage).Key; break;
                //case 5: UpgradeName = LuckMultUpgrades.ElementAt(UpgradeStage).Key; break;
                //case 6: UpgradeName = MoneyUpgrades.ElementAt(UpgradeStage).Key; break;
                //case 7: UpgradeName = StorageUpgrades.ElementAt(UpgradeStage).Key; break;
        }

    }
    public void UpdateEverything()
    {
        UpdateStage();
        Costs();
        UpdateUpgradeUI();
    }
    public void BuyUpgrade()
    {
        CheckUpgrade(UpgradeName);
        UpdateStage();
    }

    private void PurchaseSpeed(string UpgradeName)
    {
        Debug.Log("Speed selected");
        if (SpeedUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = SpeedUpgrades[UpgradeName];

            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));

            Debug.Log(hasEnoughMaterials);
            if (hasEnoughMaterials && Money >= UpgradeCost)
            {
                Money -= UpgradeCost;
                RNGscript.RollSpeed -= 0.05f;
                BoughtRollSpeed++;
                CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
                Costs();
                UpdateUpgradeUI();
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
                Debug.Log("Not Enough Cash");
            }
        }
        else
        {
            Debug.Log("Upgrade not found");
        }
    }
    private void PurchaseAmount(string UpgradeName)
    {
        if (AmountUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = AmountUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost)
            {
                Money -= UpgradeCost;
                RNGscript.cardLimit++;
                BoughtRollAmount++;
                RNGscript.RollForHand();
                CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
                Costs();
                UpdateUpgradeUI();
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
    private void PurchaseStrenght(string UpgradeName)
    {
        if (StrenghtUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = StrenghtUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost)
            {
                Money -= UpgradeCost;
                BoughtRollSkips--;
                RNGscript.StoneStatus--;
                CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
                Costs();
                UpdateUpgradeUI();
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
    private void PurchaseAutoDrill(string UpgradeName)
    {
        if (AutoDrillUpgrades.ContainsKey(UpgradeName))
        {
            var Upgrade = AutoDrillUpgrades[UpgradeName];
            bool hasEnoughMaterials = Upgrade.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            if (hasEnoughMaterials && Money >= UpgradeCost)
            {
                Money -= UpgradeCost;
                if (!AutoRoll && BoughtAutoRollUpgrade >= 0)
                {
                    BoughtAutoRollUpgrade++;
                    RNGscript.AutoTimer = 0;
                    AutoRoll = true;
                    BoughtAutoRoll = true;
                    AutoRollButton.SetActive(true);
                    RNGscript.RollButton.SetActive(false);
                    enableRuntime = true;
                    AutoRollButtonColor.color = Color.green;
                }
                else
                {
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
                Costs();
                UpdateUpgradeUI();
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

    private void CheckUpgrade(string UpgradeName)
    {
        UpdateStage();
        if (SpeedUpgrades.ContainsKey(UpgradeName))
        {
            Debug.Log("Speed upgrade");
            PurchaseSpeed(UpgradeName);
        }
        else if (AmountUpgrades.ContainsKey(UpgradeName))
        {
            Debug.Log("Amount upgrade");
            PurchaseAmount(UpgradeName);
        }
        else if (StrenghtUpgrades.ContainsKey(UpgradeName))
        {
            Debug.Log("Strenght upgrade");
            PurchaseStrenght(UpgradeName);
        }
        else if (AutoDrillUpgrades.ContainsKey(UpgradeName))
        {
            Debug.Log("AutoDrill upgrade");
            PurchaseAutoDrill(UpgradeName);
        }
        else
        {
            Debug.Log("Upgrade not found");
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
        RNGscript.AutoTimer = 0;
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
                        RNGscript.AutoTimer = 1;
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
                    RNGscript.AutoTimer = 0;
                    RNGscript.RollButton.SetActive(false);
                    AutoRoll = true;
                    AutoRollButtonColor.color = Color.green;
                }
            }
        }
    }
    private void UpdateStage()
    {
        if (SpeedUpgrades.ContainsKey(UpgradeName))
        {
            Debug.Log("Speed Update");
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
        UpdateUpgradeUI();
    }

    private void UpdateUpgradeUI()
    {
        Title.text = Titles[CurrentUpgrade];
        UpgradeText.text = UpgradeCost.ToString() + "$";
        Information.text = "";
        UpgradeImage.sprite = sprites[CurrentUpgrade];
        switch (CurrentUpgrade)
        {
            case 0: Requirements.text = string.Join("\n", SpeedUpgrades.ElementAt(UpgradeStage).Value.Select(r => $"{r.Key}: {r.Value}")); break;
            case 1: Requirements.text = string.Join("\n", AmountUpgrades.ElementAt(UpgradeStage).Value.Select(r => $"{r.Key}: {r.Value}")); break;
            case 2: Requirements.text = string.Join("\n", StrenghtUpgrades.ElementAt(UpgradeStage).Value.Select(r => $"{r.Key}: {r.Value}")); break;
            case 3: Requirements.text = string.Join("\n", AutoDrillUpgrades.ElementAt(UpgradeStage).Value.Select(r => $"{r.Key}: {r.Value}")); break;
            default: Requirements.text = ""; break;
        }
    }

    public void SwitchUpgradeForward()
    {
        CurrentUpgrade++;
        if (CurrentUpgrade > (Titles.Count - 1))
        {
            CurrentUpgrade = (Titles.Count - 1);
        }
        GetName();
        UpdateStage();
        Costs();
    }

    public void SwitchUpgradeBack()
    {
        CurrentUpgrade--;
        if (CurrentUpgrade < 0)
        {
            CurrentUpgrade = 0;
        }
        GetName();
        UpdateStage();
        Costs();
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
    public void AutoRollToggle()
    {
        if (AutoRoll == false)
        {
            RNGscript.AutoTimer = 0;
            RNGscript.RollButton.SetActive(false);
            AutoRoll = true;
            AutoRollButtonColor.color = Color.green;
        }
        else if (AutoRoll == true)
        {
            RNGscript.AutoTimer = 1;
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