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
    public RNGscript RNGscript;
    public GameObject AutoRollButton;
    public Image AutoRollButtonColor;

    
    public void Update()
    {
        StaticVariables.cash = Money;
    }
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

        data.Money = this.Money;
    }
    // end saving and getting saved data
    public void Start()
    {
        if (BoughtRollSpeed == 1)
        {
            RollSpeedText.text = "350$"; // 1 is 150 niet 350!
        }
        else if (BoughtRollSpeed == 2)
        {
            RollSpeedText.text = "750$";
        }
        else if (BoughtRollSpeed == 3)
        {
            RollSpeedText.text = "2.000$";
        }
        else if (BoughtRollSpeed == 4)
        {
            RollSpeedText.text = "4.500$";
        }
        else if (BoughtRollSpeed == 5)
        {
            RollSpeedText.text = "Purchased";
        }

        if (BoughtAutoRoll == true)
        {
            AutoRollButton.SetActive(true);
            RNGscript.RollButton.SetActive(false);
            AutoRollButtonColor.color = Color.green;
            AutoRollText.text = "Purchased";
        }

        if (BoughtRollAmount == 1)
        {
            RollAmountText.text = "5.000$";
        }
        else if (BoughtRollAmount == 2)
        {
            RollAmountText.text = "12.000$";
        }
        else if (BoughtRollAmount == 3)
        {
            RollAmountText.text = "25.000$";
        }
        else if (BoughtRollAmount == 4)
        {
            RollAmountText.text = "Purchased";
        }

        if (BoughtRollSkips == 1)
        {
            RollSkipsText.text = "15.000$";
        }
        else if (BoughtRollSkips == 2)
        {
            RollSkipsText.text = "Purchased";
        }

        if (BoughtLuckPercentage == 1)
        {
            RollSkipsText.text = "250$";
        }
        else if (BoughtLuckPercentage == 2)
        {
            RollSkipsText.text = "500$";
        }
        else if (BoughtLuckPercentage == 3)
        {
            RollSkipsText.text = "1.500$";
        }
        else if (BoughtLuckPercentage == 4)
        {
            RollSkipsText.text = "3.500$";
        }
        else if (BoughtLuckPercentage == 5)
        {
            RollSkipsText.text = "8.000$";
        }
        else if (BoughtLuckPercentage == 6)
        {
            RollSkipsText.text = "12.500$";
        }
        else if (BoughtLuckPercentage == 7)
        {
            RollSkipsText.text = "20.000$";
        }
        else if (BoughtLuckPercentage == 8)
        {
            RollSkipsText.text = "35.000$";
        }
        else if (BoughtLuckPercentage == 9)
        {
            RollSkipsText.text = "Purchased";
        }

        if (BoughtLuckMultiplier == 1)
        {
            LuckMultiplierText.text = "10.000";
        }
        else if (BoughtLuckMultiplier == 2)
        {
            LuckMultiplierText.text = "22.000";
        }
        else if (BoughtLuckMultiplier == 3)
        {
            LuckMultiplierText.text = "45.000";
        }
        else if (BoughtLuckMultiplier == 4)
        {
            LuckMultiplierText.text = "Purchased";
        }

        if (BoughtMoneyMultiplier == 1)
        {
            MoneyMultiplier.text = "1500$";
        }
        else if (BoughtMoneyMultiplier == 2)
        {
            MoneyMultiplier.text = "3000$";
        }
        else if (BoughtMoneyMultiplier == 3)
        {
            MoneyMultiplier.text = "5.000$";
        }
        else if (BoughtMoneyMultiplier == 4)
        {
            MoneyMultiplier.text = "7.500$";
        }
        else if (BoughtMoneyMultiplier == 5)
        {
            MoneyMultiplier.text = "Purchased";
        }
    }
    public void BuyRollspeed()
    {
        if (StaticVariables.cash >= 25 && BoughtRollSpeed == 0)
        {
            StaticVariables.cash -= 25;
            RNGscript.RollSpeed = 0.45f;
            BoughtRollSpeed = 1;
            RollSpeedText.text = "150$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 150 && BoughtRollSpeed == 1) // zie hier 150 = 1
        {
            StaticVariables.cash -= 150;
            RNGscript.RollSpeed = 0.4f;
            BoughtRollSpeed = 2;
            RollSpeedText.text = "350$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 350 && BoughtRollSpeed == 2)
        {
            StaticVariables.cash -= 350;
            RNGscript.RollSpeed = 0.35f;
            BoughtRollSpeed = 3;
            RollSpeedText.text = "750$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 750 && BoughtRollSpeed == 3)
        {
            StaticVariables.cash -= 750;
            RNGscript.RollSpeed = 0.3f;
            BoughtRollSpeed = 4;
            RollSpeedText.text = "2000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 2000 && BoughtRollSpeed == 4)
        {
            StaticVariables.cash -= 2000;
            RNGscript.RollSpeed = 0.25f;
            BoughtRollSpeed = 5;
            RollSpeedText.text = "4.500";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 4500 && BoughtRollSpeed == 5)
        {
            StaticVariables.cash -= 4500;
            RNGscript.RollSpeed = 0.2f;
            BoughtRollSpeed = 6;
            RollSpeedText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        //else if (StaticVariables.cash >= 25000 && BoughtRollSpeed == 6)
        //{
        //    StaticVariables.cash -= 25000;
        //    RNGscript.RollSpeed = 0.15f;
        //    BoughtRollSpeed = 7;
        //    RollSpeedText.text = "50.000$";
        //    CurrentMoney.text = StaticVariables.cash + "$";
        //}
        //else if (StaticVariables.cash >= 50000 && BoughtRollSpeed == 7)
        //{
        //    StaticVariables.cash -= 50000;
        //    RNGscript.RollSpeed = 0.1f;
        //    BoughtRollSpeed = 8;
        //    RollSpeedText.text = "Purchased";
        //    CurrentMoney.text = StaticVariables.cash + "$";
        //}
    }

    public void BuyAutoRoll()
    {
        if (StaticVariables.cash >= 500 && BoughtAutoRoll == false)
        {
            StaticVariables.cash -= 500;
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
        else if (AutoRoll ==  true)
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
            StaticVariables.cash -= 1500;
            RNGscript.cardLimit = 2;
            BoughtRollAmount = 1;
            RollAmountText.text = "3500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 3500 && BoughtRollAmount == 1)
        {
            StaticVariables.cash -= 3500;
            RNGscript.cardLimit = 3;
            BoughtRollAmount = 2;
            RollAmountText.text = "5.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5000 && BoughtRollAmount == 2) 
        {
            StaticVariables.cash -= 5000;
            RNGscript.cardLimit = 4;
            BoughtRollAmount = 3;
            RollAmountText.text = "12.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 12000 && BoughtRollAmount == 3)
        {
            StaticVariables.cash -= 12000;
            RNGscript.cardLimit = 5;
            BoughtRollAmount = 4;
            RollAmountText.text = "25.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 25000 && BoughtRollAmount == 4)
        {
            StaticVariables.cash -= 25000;
            RNGscript.cardLimit = 6;
            BoughtRollAmount = 5;
            RollAmountText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
    public void BuyRollSkips()
    {
        if (StaticVariables.cash >= 1000 && BoughtRollSkips == 0)
        {
            StaticVariables.cash -= 1500;
            RNGscript.RollSkips = 4;
            BoughtRollSkips = 1;
            RollSkipsText.text = "5.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if(StaticVariables.cash >= 5500 && BoughtRollSkips == 1)
        {
            StaticVariables.cash -= 5500;
            RNGscript.RollSkips = 3;
            BoughtRollSkips = 2;
            RollSkipsText.text = "15.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if( StaticVariables.cash >= 15000 && BoughtRollSkips == 2)
        {
            StaticVariables.cash -= 15000;
            RNGscript.RollSkips = 2;
            BoughtRollSkips = 3;
            RollSkipsText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
    }
    public void BuyLuckPercentage()
    {
        if (StaticVariables.cash >= 50 && BoughtLuckPercentage == 0)
        {
            StaticVariables.cash -= 50;
            RNGscript.LuckPercentage = 1.1f; // 10%
            BoughtLuckPercentage = 1;
            LuckPercentageText.text = "100$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 100 && BoughtLuckPercentage == 1)
        {
            StaticVariables.cash -= 100;
            RNGscript.LuckPercentage = 1.2f;
            BoughtLuckPercentage = 2;
            LuckPercentageText.text = "250$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 250 && BoughtLuckPercentage == 2)
        {
            StaticVariables.cash -= 250;
            RNGscript.LuckPercentage = 1.3f;
            BoughtLuckPercentage = 3;
            LuckPercentageText.text = "500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 500 && BoughtLuckPercentage == 3)
        {
            StaticVariables.cash -= 500;
            RNGscript.LuckPercentage = 1.4f;
            BoughtLuckPercentage = 4;
            LuckPercentageText.text = "1.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 1500 && BoughtLuckPercentage == 4)
        {
            StaticVariables.cash -= 1500;
            RNGscript.LuckPercentage = 1.5f;
            BoughtLuckPercentage = 5;
            LuckPercentageText.text = "3.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 3500 && BoughtLuckPercentage == 5)
        {
            StaticVariables.cash -= 3500;
            RNGscript.LuckPercentage = 1.65f;
            BoughtLuckPercentage = 6;
            LuckPercentageText.text = "8.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 8000 && BoughtLuckPercentage == 6)
        {
            StaticVariables.cash -= 8000;
            RNGscript.LuckPercentage = 1.8f;
            BoughtLuckPercentage = 7;
            LuckPercentageText.text = "12.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 12500 && BoughtLuckPercentage == 7)
        {
            StaticVariables.cash -= 12500;
            RNGscript.LuckPercentage = 2f;
            BoughtLuckPercentage = 8;
            LuckPercentageText.text = "20.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 20000 && BoughtLuckPercentage == 8)
        {
            StaticVariables.cash -= 20000;
            RNGscript.LuckPercentage = 2.2f;
            BoughtLuckPercentage = 9;
            LuckPercentageText.text = "35.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 35000 && BoughtLuckPercentage == 9)
        {
            StaticVariables.cash -= 35000;
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
            StaticVariables.cash -= 2500;
            RNGscript.LuckMultiplier = 1.5f; // 2x luck added on top of the percentage
            BoughtLuckMultiplier = 1;
            LuckMultiplierText.text = "5500";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5500 && BoughtLuckMultiplier == 1)
        {
            StaticVariables.cash -= 5500;
            RNGscript.LuckMultiplier = 2f;
            BoughtLuckMultiplier = 2;
            LuckMultiplierText.text = "10.000";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 10000 && BoughtLuckMultiplier == 2)
        {
            StaticVariables.cash -= 10000;
            RNGscript.LuckMultiplier = 2.5f;
            BoughtLuckMultiplier = 3;
            LuckMultiplierText.text = "22.000";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 22000 && BoughtLuckMultiplier == 3)
        {
            StaticVariables.cash -= 22000;
            RNGscript.LuckMultiplier = 3f;
            BoughtLuckMultiplier = 4;
            LuckMultiplierText.text = "45.000";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 45000 && BoughtLuckMultiplier == 4)
        {
            StaticVariables.cash -= 45000;
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
            StaticVariables.cash -= 100;
            RNGscript.MoneyMultiplier = 1.25f;
            BoughtMoneyMultiplier = 1;
            MoneyMultiplier.text = "500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 500 && BoughtMoneyMultiplier == 1)
        {
            StaticVariables.cash -= 500;
            RNGscript.MoneyMultiplier = 1.5f;
            BoughtMoneyMultiplier = 2;
            MoneyMultiplier.text = "1500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 1500 && BoughtMoneyMultiplier == 2)
        {
            StaticVariables.cash -= 1500;
            RNGscript.MoneyMultiplier = 1.75f;
            BoughtMoneyMultiplier = 3;
            MoneyMultiplier.text = "3000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 3000 && BoughtMoneyMultiplier == 3)
        {
            StaticVariables.cash -= 3000;
            RNGscript.MoneyMultiplier = 2f;
            BoughtMoneyMultiplier = 4;
            MoneyMultiplier.text = "5.000$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 5000 && BoughtMoneyMultiplier == 4)
        {
            StaticVariables.cash -= 5000;
            RNGscript.MoneyMultiplier = 2.5f;
            BoughtMoneyMultiplier = 5;
            MoneyMultiplier.text = "7.500$";
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else if (StaticVariables.cash >= 7500 && BoughtMoneyMultiplier == 5)
        {
            StaticVariables.cash -= 7500;
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
}