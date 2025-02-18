using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class XPScript : MonoBehaviour, IDataPersistence
{
    public int XPCount;
    public int MaxLevel = 100;
    public int Rebirth;
    public int LevelCount = 1;
    public int XPMultiplier = 1;
    public float XPLuckMultiplier = 1;

    public float XPNeeded = 10;
    public Text ShowXp;

    public RNGscript RNGscript;
    public MoneyLogic MoneyLogic;
    public OreStorage OreStorage;
    public SavedOresCount SavedOres;
    public DataPersistence DataPersistence;
    public IndexManager IndexManager;

    public GameObject RebirthButton;


    public void LoadData(GameData data)
    {
        this.Rebirth = data.Rebirth;
        this.MaxLevel = data.MaxLevel;
        this.XPCount = data.XPCount;
        this.LevelCount = data.LevelCount;
        this.XPNeeded = data.XPNeeded;
        this.XPMultiplier = data.XPMultiplier;
        this.XPLuckMultiplier = data.XPLuckMultiplier;
    }
    public void SaveData(ref GameData data)
    {
        data.Rebirth = this.Rebirth;
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
        if (Rebirth == 1)
        {
            OreStorage.InventoryOres = 6;
            IndexManager.MaxIndexCount = 6;
        }
        else if (Rebirth >= 2)
        {
            OreStorage.InventoryOres = 7;
            IndexManager.MaxIndexCount = 7;
        }
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
        Rebirth++;

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
        
        // money logic reset
        MoneyLogic.BoughtAutoRoll = false;
        MoneyLogic.BoughtLuckMultiplier = 0;
        MoneyLogic.BoughtMoneyMultiplier = 0;
        MoneyLogic.BoughtLuckPercentage = 0;
        MoneyLogic.BoughtRollAmount = 0;
        MoneyLogic.BoughtRollSkips = 0;
        MoneyLogic.BoughtRollSpeed = 0;
        MoneyLogic.BoughtStorageAmount = 0;
        MoneyLogic.Money = 0;

        // orecount reset
        DataPersistence.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
