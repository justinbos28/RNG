using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyLogic : MonoBehaviour
{
    public RNGscript RNGscript;
    public void BuyRollspeed()
    {
        if (StaticVariables.cash >= 25 )
        {
            StaticVariables.cash -= 25;
            RNGscript.RollSpeed = 0.4f;
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
}