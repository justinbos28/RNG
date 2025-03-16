using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using System.Collections.Generic;

public class XPScript : MonoBehaviour, IDataPersistence
{
    public int XPCount;
    public int MaxLevel = 100;
    public int Rebirth;
    public int SavedRebirth;
    public int LevelCount = 1;
    public int XPMultiplier = 1;
    public float XPLuckMultiplier = 1;
    public float XPNeeded = 10;
    
    public Text ShowXp;
    public List<WorldClass> Worlds = new List<WorldClass>();

    public RNGscript RNGscript;
    public MoneyLogic MoneyLogic;
    public OreStorage OreStorage;
    public SavedOresCount SavedOres;
    public DataPersistence DataPersistence;
    public IndexManager IndexManager;
    public CraftingRecipes CraftingRecipes;
    public DrillUnlocked DrillUnlocked;

    public GameObject RebirthButton;


    public void LoadData(GameData data)
    {
        this.SavedRebirth = data.SavedRebirth;
        this.MaxLevel = data.MaxLevel;
        this.XPCount = data.XPCount;
        this.LevelCount = data.LevelCount;
        this.XPNeeded = data.XPNeeded;
        this.XPMultiplier = data.XPMultiplier;
        this.XPLuckMultiplier = data.XPLuckMultiplier;
    }
    public void SaveData(ref GameData data)
    {
        data.SavedRebirth = this.SavedRebirth;
        data.MaxLevel = this.MaxLevel;
        data.XPCount = this.XPCount;
        data.LevelCount = this.LevelCount;
        data.XPNeeded = this.XPNeeded;
        data.XPMultiplier = this.XPMultiplier;
        data.XPLuckMultiplier = this.XPLuckMultiplier;
    }
    private void Update()
    {
        if (LevelCount < MaxLevel)
        {
            ShowXp.text = "level " + LevelCount + ": " + XPCount + " / " + XPNeeded.ToString("F0");
        }
        else
        {
            XPMultiplier = 0;
            RebirthButton.SetActive(true);
            ShowXp.text = "level " + LevelCount + ": " + "Max";
        }
    }
    private void Start()
    {
        if (SavedRebirth == 1)
        {
            OreStorage.InventoryOres = 6;
            IndexManager.MaxIndexCount = 6;
        }
        else if (SavedRebirth >= 2)
        {
            OreStorage.InventoryOres = 7;
            IndexManager.MaxIndexCount = 7;
        }

        Rebirth = SavedRebirth;

        SelectWorld();
    }
    public void UpdateXP()
    {
        for (int i = 0; i < RNGscript.playerHand.Count; i++)
        {
            XPCount += RNGscript.playerHand[i].XP * XPMultiplier;
        }
        if (XPCount >= XPNeeded)
        {
            XPCount = 0;
            LeveledUp();
        }
    }
    public void LeveledUp()
    {
        XPNeeded += (2 * 1.1f) * LevelCount;
        LevelCount++;
        ShowXp.text = "level " + LevelCount + ": " + XPCount + " / " + XPNeeded.ToString("F0");
        ApplyLevelBonuses();
    }
    private void ApplyLevelBonuses()
    {
        if (LevelCount % 10 == 0 && LevelCount <= 100)
        {
            XPMultiplier = LevelCount / 10 + 1;
            XPLuckMultiplier = 1 + (LevelCount / 100.0f);
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount % 10 == 0 && LevelCount > 100)
        {
            XPMultiplier = LevelCount / 10 + 1;
            XPLuckMultiplier = 1 + (LevelCount / 100.0f);
        }
    }

    public void SelectWorld()
    {
        
        for (int i = 0; i < Worlds.Count; i++)
        {
            Worlds[i].IsSelected = false;
            Worlds[i].text.color = Color.white;
            int index = i;
            Worlds[i].button.onClick.AddListener(() =>
            {
                Worlds[index].IsSelected = true;
                Worlds[index].text.color = Color.green;
            });
        }
    }

    public void LoadWorld()
    {
        for (int i = 0; i < Worlds.Count; i++)
        {
            if (Worlds[i].IsSelected)
            {
                if (Worlds[i].ID >= SavedRebirth)
                {
                    Rebirth = SavedRebirth;
                }
                else
                {
                    Rebirth = Worlds[i].ID;
                }    
            }
        }
    }

    public void PerformRebirth()
    {
        // all xp reset
        RebirthButton.SetActive(false);
        LevelCount = 1;
        XPCount = 0;
        XPLuckMultiplier = 1;
        XPMultiplier = 1;
        XPNeeded = 10;
        MaxLevel += 50;
        SavedRebirth++;

        // rngscript reset
        RNGscript.RollSkips = 5;
        RNGscript.RollSpeed = 0.5f;
        RNGscript.cardLimit = 1;
        RNGscript.LuckMultiplier = 1;
        RNGscript.LuckPercentage = 1;
        RNGscript.MoneyMultiplier = 1;

        for (int i = 0; i < RNGscript.allOres.Count; i++)
        {
            RNGscript.allOres[i].StorageAmount = 0;
        }
        for (int i = 0; i < CraftingRecipes.Materials.Count; i++)
        {
            CraftingRecipes.Materials[i].StorageAmount = 0;
        }

        // money logic reset
        MoneyLogic.BoughtAutoRoll = false;
        MoneyLogic.BoughtLuckMultiplier = 0;
        MoneyLogic.BoughtMoneyMultiplier = 0;
        MoneyLogic.BoughtLuckPercentage = 0;
        MoneyLogic.BoughtRollAmount = 0;
        MoneyLogic.BoughtRollSkips = 0;
        MoneyLogic.BoughtRollSpeed = 0;
        MoneyLogic.BoughtStorageAmount = 0;
        MoneyLogic.BoughtAutoRollUpgrade = 0;
        MoneyLogic.Money = 0;

        MoneyLogic.enableRuntime = false;
        MoneyLogic.enableCooldown = false;
        MoneyLogic.hasNoCooldown = false;
        MoneyLogic.MaxRuntime = 300;
        MoneyLogic.MaxCooldown = 600;
        MoneyLogic.Runtime = 300;
        MoneyLogic.Cooldown = 600;

        // orecount reset
        OreStorage.MaxCommonOres = 500;
        OreStorage.MaxUncommonOres = 250;
        OreStorage.MaxRareOres = 125;
        OreStorage.MaxEpicOres = 75;
        OreStorage.MaxLegendaryOres = 40;
        OreStorage.MaxMythicOres = 20;
        OreStorage.MaxExoticOres = 10;
        OreStorage.MaxDivineOres = 5;
        for (int i = 0; i < DrillUnlocked.SavedDrillData.Count; i++)
        {
            DrillUnlocked.SavedDrillData[i].Upgrade = 0;
            DrillUnlocked.SavedDrillData[i].Unlocked = false;
        }

        DataPersistence.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
