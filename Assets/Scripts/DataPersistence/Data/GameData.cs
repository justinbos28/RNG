using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("Values from RNG script")]
    public float Money;

    public int CardLimit;
    public int RollSkips;

    public bool BoughtAutoRoll;

    public float RollSpeed;
    public float LuckPercentage;
    public float LuckMultiplier;
    public float MoneyMultiplier;
    public bool Test = false;

    [Header("Values from MoneyLogic")]
    public int BoughtRollSpeed;
    public int BoughtRollAmount;
    public int BoughtRollSkips;
    public int BoughtLuckPercentage;
    public int BoughtLuckMultiplier;
    public int BoughtMoneyMultiplier;
    public int BoughtStorageAmount;

    [Header("Values from Xpscript")]
    public int XPCount;
    public int LevelCount;
    public int XPMultiplier = 1;
    public int MaxLevel = 100;
    public int SavedRebirth;

    public float XPNeeded;
    public float XPLuckMultiplier = 1;

    public List<int> OresCount = new List<int>();

    public GameData()
    {
        this.Money = 0;

        this.CardLimit = 1;
        this.RollSkips = 5;
        this.RollSpeed = 0.5f;
        this.LuckPercentage = 1;
        this.LuckMultiplier = 1;
        this.MoneyMultiplier = 1;
        
        this.BoughtLuckMultiplier = 0;
        this.BoughtMoneyMultiplier = 0;
        this.BoughtLuckPercentage = 0;
        this.BoughtRollAmount = 0;
        this.BoughtRollSkips = 0;
        this.BoughtRollSpeed = 0;
        this.XPCount = 0;
        this.LevelCount = 1;
        this.XPNeeded = 10;
        this.XPMultiplier = 1;
        this.XPLuckMultiplier = 1;
        this.MaxLevel = 100;
        this.SavedRebirth = 0;
        this.Test = false;

        this.OresCount = new List<int>();  
    }
}
