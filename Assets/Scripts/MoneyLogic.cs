using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
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

    public float Money;
    public bool AutoRoll;
    public bool BoughtAutoRoll;

    public Text RollSpeedText;
    public Text CurrentMoney;
    public Text RollAmountText;
    public Text RollSkipsText;
    public Text AutoRollText;
    public Text LuckPercentageText;
    public Text LuckMultiplierText;
    public Text MoneyMultiplier;
    public Text StorageAmount;

    public RNGscript RNGscript;
    public OreStorage OreStorage;
    public GameObject AutoRollButton;
    public XPScript XPScript;

    public Image AutoSellButton;
    public Image AutoRollButtonColor;


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
    public void LateUpdate()
    {
        StaticVariables.cash = Money;
        // roll speed
        if (BoughtRollSpeed == 1)
        {
            RollSpeedText.text = "150$";
        }
        else if (BoughtRollSpeed == 2)
        {
            RollSpeedText.text = "350$";
        }
        else if (BoughtRollSpeed == 3)
        {
            RollSpeedText.text = "750$";
        }
        else if (BoughtRollSpeed == 4)
        {
            RollSpeedText.text = "2.000$";
        }
        else if (BoughtRollSpeed == 5)
        {
            RollSpeedText.text = "4.500$";
        }
        else if (BoughtRollSpeed == 6)
        {
            RollSpeedText.text = "Purchased";
        }
        // auto roll
        if (BoughtAutoRoll == true)
        {
            AutoRollButton.SetActive(true);
            AutoRollText.text = "Purchased";
        }
        // roll amount
        if (BoughtRollAmount == 1)
        {
            RollAmountText.text = "3.500$";
        }
        else if (BoughtRollAmount == 2)
        {
            RollAmountText.text = "5.000$";
        }
        else if (BoughtRollAmount == 3)
        {
            RollAmountText.text = "12.000$";
        }
        else if (BoughtRollAmount == 4)
        {
            RollAmountText.text = "25.000$";
        }
        else if (BoughtRollAmount == 5)
        {
            RollAmountText.text = "Purchased";
        }
        // roll skips
        if (BoughtRollSkips == 1)
        {
            RollSkipsText.text = "5.500$";
        }
        else if (BoughtRollSkips == 2)
        {
            RollSkipsText.text = "15.000$";
        }
        else if (BoughtRollSkips == 3)
        {
            RollSkipsText.text = "Purchased";
        }
        // luck percentage
        if (BoughtLuckPercentage == 1)
        {
            LuckPercentageText.text = "100$";
        }
        else if (BoughtLuckPercentage == 2)
        {
            LuckPercentageText.text = "250$";
        }
        else if (BoughtLuckPercentage == 3)
        {
            LuckPercentageText.text = "500$";
        }
        else if (BoughtLuckPercentage == 4)
        {
            LuckPercentageText.text = "1.500$";
        }
        else if (BoughtLuckPercentage == 5)
        {
            LuckPercentageText.text = "3.500$";
        }
        else if (BoughtLuckPercentage == 6)
        {
            LuckPercentageText.text = "8.000$";
        }
        else if (BoughtLuckPercentage == 7)
        {
            LuckPercentageText.text = "12.500$";
        }
        else if (BoughtLuckPercentage == 8)
        {
            LuckPercentageText.text = "20.000$";
        }
        else if (BoughtLuckPercentage == 9)
        {
            LuckPercentageText.text = "35.000$";
        }
        else if (BoughtLuckPercentage == 10)
        {
            LuckPercentageText.text = "Purchased";
        }
        // luck multiplier
        if (BoughtLuckMultiplier == 1)
        {
            LuckMultiplierText.text = "5.500";
        }
        else if (BoughtLuckMultiplier == 2)
        {
            LuckMultiplierText.text = "10.000";
        }
        else if (BoughtLuckMultiplier == 3)
        {
            LuckMultiplierText.text = "22.000";
        }
        else if (BoughtLuckMultiplier == 4)
        {
            LuckMultiplierText.text = "45.000";
        }
        else if (BoughtLuckMultiplier == 5)
        {
            LuckMultiplierText.text = "Purchased";
        }
        // money multiplier
        if (BoughtMoneyMultiplier == 1)
        {
            MoneyMultiplier.text = "500$";
        }
        else if (BoughtMoneyMultiplier == 2)
        {
            MoneyMultiplier.text = "1.500$";
        }
        else if (BoughtMoneyMultiplier == 3)
        {
            MoneyMultiplier.text = "3.000$";
        }
        else if (BoughtMoneyMultiplier == 4)
        {
            MoneyMultiplier.text = "5.000$";
        }
        else if (BoughtMoneyMultiplier == 5)
        {
            MoneyMultiplier.text = "7.500$";
        }
        else if (BoughtMoneyMultiplier == 6)
        {
            MoneyMultiplier.text = "Purchased";
        }
        if (BoughtStorageAmount == 1)
        {
            OreStorage.MaxCommonOres = 1000;
            OreStorage.MaxUncommonOres = 500;
            OreStorage.MaxRareOres = 250;
            OreStorage.MaxEpicOres = 150;
            OreStorage.MaxLegendaryOres = 50;
            OreStorage.MaxMythicOres = 10;
            OreStorage.MaxExoticOres = 5;
            OreStorage.MaxDivineOres = 2;
            StorageAmount.text = "100.000$";
        }
        else if (BoughtStorageAmount == 2)
        {
            OreStorage.MaxCommonOres = 2000;
            OreStorage.MaxUncommonOres = 1000;
            OreStorage.MaxRareOres = 500;
            OreStorage.MaxEpicOres = 300;
            OreStorage.MaxLegendaryOres = 100;
            OreStorage.MaxMythicOres = 20;
            OreStorage.MaxExoticOres = 10;
            OreStorage.MaxDivineOres = 5;
            if (XPScript.SavedRebirth >= 1)
            {
                StorageAmount.text = "1.000.000$";
            }
            else
            {
                StorageAmount.text = "Purchased";
            }
        }
        else if (BoughtStorageAmount == 3)
        {
            OreStorage.MaxCommonOres = 3000;
            OreStorage.MaxUncommonOres = 1500;
            OreStorage.MaxRareOres = 750;
            OreStorage.MaxEpicOres = 450;
            OreStorage.MaxLegendaryOres = 150;
            OreStorage.MaxMythicOres = 30;
            OreStorage.MaxExoticOres = 20;
            OreStorage.MaxDivineOres = 10;
            StorageAmount.text = "5.0000.000";
        }
        else if (BoughtStorageAmount == 4)
        {
            OreStorage.MaxCommonOres = 4000;
            OreStorage.MaxUncommonOres = 3000;
            OreStorage.MaxRareOres = 1000;
            OreStorage.MaxEpicOres = 750;
            OreStorage.MaxLegendaryOres = 300;
            OreStorage.MaxMythicOres = 60;
            OreStorage.MaxExoticOres = 40;
            OreStorage.MaxDivineOres = 20;
            if (XPScript.SavedRebirth >= 2)
            {
                StorageAmount.text = "10.000.000$";
            }
            else
            {
                StorageAmount.text = "Purchased";
            }
        }
        else if (BoughtStorageAmount == 5)
        {
            OreStorage.MaxCommonOres = 5000;
            OreStorage.MaxUncommonOres = 4000;
            OreStorage.MaxRareOres = 2000;
            OreStorage.MaxEpicOres = 1000;
            OreStorage.MaxLegendaryOres = 600;
            OreStorage.MaxMythicOres = 100;
            OreStorage.MaxExoticOres = 80;
            OreStorage.MaxDivineOres = 40;
            StorageAmount.text = "50.000.000$";
        }
        else if (BoughtStorageAmount == 6)
        {
            OreStorage.MaxCommonOres = 10000;
            OreStorage.MaxUncommonOres = 5000;
            OreStorage.MaxRareOres = 4000;
            OreStorage.MaxEpicOres = 1500;
            OreStorage.MaxLegendaryOres = 800;
            OreStorage.MaxMythicOres = 150;
            OreStorage.MaxExoticOres = 100;
            OreStorage.MaxDivineOres = 50;
            StorageAmount.text = "Purchased";
        }
    }
    public void BuyRollspeed()
    {
        if (StaticVariables.cash >= 25 && BoughtRollSpeed == 0)
        {
            Money -= 25;
            RNGscript.RollSpeed -= 0.05f;
            BoughtRollSpeed = 1;
            RollSpeedText.text = "150$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 150 && BoughtRollSpeed == 1) // zie hier 150 = 1
        {
            Money -= 150;
            RNGscript.RollSpeed -= 0.05f;
            BoughtRollSpeed = 2;
            RollSpeedText.text = "350$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 350 && BoughtRollSpeed == 2)
        {
            Money -= 350;
            RNGscript.RollSpeed -= 0.05f;
            BoughtRollSpeed = 3;
            RollSpeedText.text = "750$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 750 && BoughtRollSpeed == 3)
        {
            Money -= 750;
            RNGscript.RollSpeed -= 0.05f;
            BoughtRollSpeed = 4;
            RollSpeedText.text = "2.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 2000 && BoughtRollSpeed == 4)
        {
            Money -= 2000;
            RNGscript.RollSpeed -= 0.05f;
            BoughtRollSpeed = 5;
            RollSpeedText.text = "4.500";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 4500 && BoughtRollSpeed == 5)
        {
            Money -= 4500;
            RNGscript.RollSpeed -= 0.05f;
            BoughtRollSpeed = 6;
            RollSpeedText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }

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
            AutoRollText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
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
    public void BuyRollAmount()
    {
        if (StaticVariables.cash >= 1500 && BoughtRollAmount == 0)
        {
            Money -= 1500;
            RNGscript.cardLimit = 2;
            BoughtRollAmount = 1;
            RollAmountText.text = "3.500$";
            RNGscript.RollForHand();
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 3500 && BoughtRollAmount == 1)
        {
            Money -= 3500;
            RNGscript.cardLimit = 3;
            BoughtRollAmount = 2;
            RollAmountText.text = "5.000$";
            RNGscript.RollForHand();
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5000 && BoughtRollAmount == 2)
        {
            Money -= 5000;
            RNGscript.cardLimit = 4;
            BoughtRollAmount = 3;
            RollAmountText.text = "12.000$";
            RNGscript.RollForHand();
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 12000 && BoughtRollAmount == 3)
        {
            Money -= 12000;
            RNGscript.cardLimit = 5;
            BoughtRollAmount = 4;
            RollAmountText.text = "25.000$";
            RNGscript.RollForHand();
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 25000 && BoughtRollAmount == 4)
        {
            Money -= 25000;
            RNGscript.cardLimit = 6;
            BoughtRollAmount = 5;
            RollAmountText.text = "Purchased"; 
            RNGscript.RollForHand();
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
    public void BuyRollSkips()
    {
        if (StaticVariables.cash >= 1000 && BoughtRollSkips == 0)
        {
            Money -= 1000;
            RNGscript.RollSkips = 4;
            RNGscript.StoneStatus = 4;
            BoughtRollSkips = 1;
            RollSkipsText.text = "5.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5500 && BoughtRollSkips == 1)
        {
            Money -= 5500;
            RNGscript.RollSkips = 3;
            RNGscript.StoneStatus = 3;
            BoughtRollSkips = 2;
            RollSkipsText.text = "15.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 15000 && BoughtRollSkips == 2)
        {
            Money -= 15000;
            RNGscript.RollSkips = 2;
            RNGscript.StoneStatus = 2;
            BoughtRollSkips = 3;
            RollSkipsText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
    public void BuyLuckPercentage()
    {
        if (StaticVariables.cash >= 50 && BoughtLuckPercentage == 0)
        {
            Money -= 50;
            RNGscript.LuckPercentage = 1.1f; // 10%
            BoughtLuckPercentage = 1;
            LuckPercentageText.text = "100$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 100 && BoughtLuckPercentage == 1)
        {
            Money -= 100;
            RNGscript.LuckPercentage = 1.2f;
            BoughtLuckPercentage = 2;
            LuckPercentageText.text = "250$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 250 && BoughtLuckPercentage == 2)
        {
            Money -= 250;
            RNGscript.LuckPercentage = 1.3f;
            BoughtLuckPercentage = 3;
            LuckPercentageText.text = "500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 500 && BoughtLuckPercentage == 3)
        {
            Money -= 500;
            RNGscript.LuckPercentage = 1.4f;
            BoughtLuckPercentage = 4;
            LuckPercentageText.text = "1.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 1500 && BoughtLuckPercentage == 4)
        {
            Money -= 1500;
            RNGscript.LuckPercentage = 1.5f;
            BoughtLuckPercentage = 5;
            LuckPercentageText.text = "3.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 3500 && BoughtLuckPercentage == 5)
        {
            Money -= 3500;
            RNGscript.LuckPercentage = 1.65f;
            BoughtLuckPercentage = 6;
            LuckPercentageText.text = "8.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 8000 && BoughtLuckPercentage == 6)
        {
            Money -= 8000;
            RNGscript.LuckPercentage = 1.8f;
            BoughtLuckPercentage = 7;
            LuckPercentageText.text = "12.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 12500 && BoughtLuckPercentage == 7)
        {
            Money -= 12500;
            RNGscript.LuckPercentage = 2f;
            BoughtLuckPercentage = 8;
            LuckPercentageText.text = "20.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 20000 && BoughtLuckPercentage == 8)
        {
            Money -= 20000;
            RNGscript.LuckPercentage = 2.2f;
            BoughtLuckPercentage = 9;
            LuckPercentageText.text = "35.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 35000 && BoughtLuckPercentage == 9)
        {
            Money -= 35000;
            RNGscript.LuckPercentage = 2.5f;
            BoughtLuckPercentage = 10;
            LuckPercentageText.text = "Purchased";
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
            LuckMultiplierText.text = "5.500";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5500 && BoughtLuckMultiplier == 1)
        {
            Money -= 5500;
            RNGscript.LuckMultiplier = 2f;
            BoughtLuckMultiplier = 2;
            LuckMultiplierText.text = "10.000";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 10000 && BoughtLuckMultiplier == 2)
        {
            Money -= 10000;
            RNGscript.LuckMultiplier = 2.5f;
            BoughtLuckMultiplier = 3;
            LuckMultiplierText.text = "22.000";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 22000 && BoughtLuckMultiplier == 3)
        {
            Money -= 22000;
            RNGscript.LuckMultiplier = 3f;
            BoughtLuckMultiplier = 4;
            LuckMultiplierText.text = "45.000";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 45000 && BoughtLuckMultiplier == 4)
        {
            Money -= 45000;
            RNGscript.LuckMultiplier = 4f;
            BoughtLuckMultiplier = 5;
            LuckMultiplierText.text = "Purchased";
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
            MoneyMultiplier.text = "500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 500 && BoughtMoneyMultiplier == 1)
        {
            Money -= 500;
            RNGscript.MoneyMultiplier = 1.5f;
            BoughtMoneyMultiplier = 2;
            MoneyMultiplier.text = "1.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 1500 && BoughtMoneyMultiplier == 2)
        {
            Money -= 1500;
            RNGscript.MoneyMultiplier = 1.75f;
            BoughtMoneyMultiplier = 3;
            MoneyMultiplier.text = "3.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 3000 && BoughtMoneyMultiplier == 3)
        {
            Money -= 3000;
            RNGscript.MoneyMultiplier = 2f;
            BoughtMoneyMultiplier = 4;
            MoneyMultiplier.text = "5.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5000 && BoughtMoneyMultiplier == 4)
        {
            Money -= 5000;
            RNGscript.MoneyMultiplier = 2.5f;
            BoughtMoneyMultiplier = 5;
            MoneyMultiplier.text = "7.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 7500 && BoughtMoneyMultiplier == 5)
        {
            Money -= 7500;
            RNGscript.MoneyMultiplier = 3f;
            BoughtMoneyMultiplier = 6;
            MoneyMultiplier.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
    public void GiveMoneyTest()
    {
        Money += 100000;
        CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
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
            StorageAmount.text = "100.000$";
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