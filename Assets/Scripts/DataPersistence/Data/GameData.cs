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

    [Header("Values from MoneyLogic")]
    public int BoughtRollSpeed;
    public int BoughtRollAmount;
    public int BoughtRollSkips;
    public int BoughtLuckPercentage;
    public int BoughtLuckMultiplier;
    public int BoughtMoneyMultiplier;

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
    }
}
