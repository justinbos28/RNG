using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class MoneyLogic : MonoBehaviour
{
    public int BoughtRollSpeed;
    public int BoughtRollAmount;
    public int BoughtRollSkips;
    public int BoughtLuckPercentage;
    public int BoughtLuckMultiplier;
    public bool AutoRoll;

    public Text RollSpeedText;
    public Text CurrentMoney;
    public Text RollAmountText;
    public Text RollSkipsText;
    public Text AutoRollText;
    public Text LuckPercentageText;
    public Text LuckMultiplierText;
    public RNGscript RNGscript;
    public GameObject AutoRollButton;
    public Image AutoRollButtonColor;
    public void BuyRollspeed()
    {
        if (StaticVariables.cash >= 25 && BoughtRollSpeed == 0)
        {
            StaticVariables.cash -= 25;
            RNGscript.RollSpeed = 0.45f;
            BoughtRollSpeed = 1;
            RollSpeedText.text = "150$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 150 && BoughtRollSpeed == 1)
        {
            StaticVariables.cash -= 150;
            RNGscript.RollSpeed = 0.4f;
            BoughtRollSpeed = 2;
            RollSpeedText.text = "500$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 500 && BoughtRollSpeed == 2)
        {
            StaticVariables.cash -= 500;
            RNGscript.RollSpeed = 0.35f;
            BoughtRollSpeed = 3;
            RollSpeedText.text = "1700$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 1700 && BoughtRollSpeed == 3)
        {
            StaticVariables.cash -= 1700;
            RNGscript.RollSpeed = 0.3f;
            BoughtRollSpeed = 4;
            RollSpeedText.text = "4500$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 4500 && BoughtRollSpeed == 4)
        {
            StaticVariables.cash -= 4500;
            RNGscript.RollSpeed = 0.25f;
            BoughtRollSpeed = 5;
            RollSpeedText.text = "10.000";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 10000 && BoughtRollSpeed == 5)
        {
            StaticVariables.cash -= 10000;
            RNGscript.RollSpeed = 0.2f;
            BoughtRollSpeed = 6;
            RollSpeedText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash + "$";
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
        if (StaticVariables.cash >= 1000 && AutoRoll == false)
        {
            StaticVariables.cash -= 1000;
            RNGscript.AutoTimer = 0;
            AutoRoll = true;
            AutoRollButton.SetActive(true);
            RNGscript.RollButton.SetActive(false);
            AutoRollButtonColor.color = Color.green;
            AutoRollText.text = "Purchased";
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
            RollAmountText.text = "5000$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 5000 && BoughtRollAmount == 1)
        {
            StaticVariables.cash -= 5000;
            RNGscript.cardLimit = 3;
            BoughtRollAmount = 2;
            RollAmountText.text = "15.000$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 15000 && BoughtRollAmount == 2) 
        {
            StaticVariables.cash -= 15000;
            RNGscript.cardLimit = 4;
            BoughtRollAmount = 3;
            RollAmountText.text = "45.000$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 45000 && BoughtRollAmount == 3)
        {
            StaticVariables.cash -= 45000;
            RNGscript.cardLimit = 5;
            BoughtRollAmount = 4;
            RollAmountText.text = "100.000$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 100000 && BoughtRollAmount == 4)
        {
            StaticVariables.cash -= 100000;
            RNGscript.cardLimit = 6;
            BoughtRollAmount = 5;
            RollAmountText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
    }
    public void BuyRollSkips()
    {
        if (StaticVariables.cash >= 5000 && BoughtRollSkips == 0)
        {
            StaticVariables.cash -= 5000;
            RNGscript.RollSkips = 4;
            BoughtRollSkips = 1;
            RollSkipsText.text = "10.000$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if(StaticVariables.cash >= 10000 && BoughtRollSkips == 1)
        {
            StaticVariables.cash -= 10000;
            RNGscript.RollSkips = 3;
            BoughtRollSkips = 2;
            RollSkipsText.text = "20.000$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if( StaticVariables.cash >= 20000 && BoughtRollSkips == 2)
        {
            StaticVariables.cash -= 20000;
            RNGscript.RollSkips = 2;
            BoughtRollSkips = 3;
            RollSkipsText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
    }
    public void BuyLuckPercentage()
    {
        if (StaticVariables.cash >= 100 && BoughtLuckPercentage == 0)
        {
            StaticVariables.cash -= 100;
            RNGscript.LuckPercentage = 1.1f; // 10%
            BoughtLuckPercentage = 1;
            LuckPercentageText.text = "500";
        }
        else if (StaticVariables.cash >= 500 && BoughtLuckPercentage == 1)
        {
            StaticVariables.cash -= 500;
            RNGscript.LuckPercentage = 2f;
            BoughtLuckPercentage = 2;
            LuckPercentageText.text = "1500";
        }
        else if (StaticVariables.cash >= 1500 && BoughtLuckPercentage >= 2)
        {
            StaticVariables.cash -= 1500;
            RNGscript.LuckPercentage = 3f;
            BoughtLuckPercentage = 3;
            LuckPercentageText.text = "end test";
        }
    }
    public void BuyLuckMulitplier()
    {
        if (StaticVariables.cash >= 10000 && BoughtLuckMultiplier == 0)
        {
            StaticVariables.cash -= 10000;
            RNGscript.LuckMultiplier = 2f; // 2x luck added on top of the percentage
            LuckMultiplierText.text = "Purchased";
        }
    }
    public void GiveMoneyTest()
    {
        StaticVariables.cash += 100000;
        CurrentMoney.text = StaticVariables.cash + "$";
    }
}