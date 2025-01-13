using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class MoneyLogic : MonoBehaviour
{
    public bool BoughtRollSpeed1;
    public bool BoughtRollSpeed2;
    public bool BoughtRollSpeed3;
    public bool BoughtRollSpeed4;
    public bool BoughtRollSpeed5;
    public bool BoughtRollSpeed6;
    public RNGscript RNGscript;
    public Text RollSpeedText;
    public Text CurrentMoney;
    public void BuyRollspeed()
    {
        if (StaticVariables.cash >= 25 && BoughtRollSpeed1 == false)
        {
            StaticVariables.cash -= 25;
            RNGscript.RollSpeed = 0.45f;
            BoughtRollSpeed1 = true;
            RollSpeedText.text = "250$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 250 && BoughtRollSpeed2 == false)
        {
            StaticVariables.cash -= 250;
            RNGscript.RollSpeed = 0.4f;
            BoughtRollSpeed2 = true;
            RollSpeedText.text = "1000$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 1000 && BoughtRollSpeed3 == false)
        {
            StaticVariables.cash -= 1000;
            RNGscript.RollSpeed = 0.35f;
            BoughtRollSpeed3 = true;
            RollSpeedText.text = "2500$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 2500 && BoughtRollSpeed4 == false)
        {
            StaticVariables.cash -= 2500;
            RNGscript.RollSpeed = 0.3f;
            BoughtRollSpeed4 = true;
            RollSpeedText.text = "7500$";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 7500 && BoughtRollSpeed5 == false)
        {
            StaticVariables.cash -= 7500;
            RNGscript.RollSpeed = 0.25f;
            BoughtRollSpeed5 = true;
            RollSpeedText.text = "20.000";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        else if (StaticVariables.cash >= 20000 && BoughtRollSpeed6 == false)
        {
            StaticVariables.cash -= 20000;
            RNGscript.RollSpeed = 0.2f;
            BoughtRollSpeed6 = true;
            RollSpeedText.text = "Purchased";
            CurrentMoney.text = StaticVariables.cash + "$";
        }
    }

    public void BuyAutoRoll()
    {
        if (StaticVariables.cash >= 5)
        {
            StaticVariables.cash -= 5;
            RNGscript.AutoTimer = 0;
        }
    }
    public void BuyRollAmount()
    {
        if (StaticVariables.cash >= 500)
        {
            StaticVariables.cash -= 500;
            RNGscript.cardLimit += 1;
        }
    }
    public void RollAmountTest()
    {
        RNGscript.cardLimit += 1;
    }
    public void GiveMoneyTest()
    {
        StaticVariables.cash += 1000;
        CurrentMoney.text = StaticVariables.cash + "$";
    }
}