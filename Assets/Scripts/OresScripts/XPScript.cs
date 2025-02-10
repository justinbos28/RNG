using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class XPScript : MonoBehaviour, IDataPersistence
{
    public int XPCount;
    public int LevelCount = 1;
    public int XPMultiplier = 1;
    public float XPLuckMultiplier = 1;

    public float XPNeeded = 10;
    public Text ShowXp;

    public RNGscript RNGscript;

    public void LoadData(GameData data)
    {
        this.XPCount = data.XPCount;
        this.LevelCount = data.LevelCount;
        this.XPNeeded = data.XPNeeded;
        this.XPMultiplier = data.XPMultiplier;
        this.XPLuckMultiplier = data.XPLuckMultiplier;
    }
    public void SaveData(ref GameData data)
    {
        data.XPCount = this.XPCount;
        data.LevelCount = this.LevelCount;
        data.XPNeeded = this.XPNeeded;
        data.XPMultiplier = this.XPMultiplier;
        data.XPLuckMultiplier = this.XPLuckMultiplier;
    }
    private void Update()
    {
        if (LevelCount < 100)
        {
            ShowXp.text = "level " + LevelCount + ": " + XPCount + " / " + XPNeeded.ToString("F0");
        }
        else
        {
             ShowXp.text = "level " + LevelCount + ": " + "Max";
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
        XPNeeded += 10 + (1.1f * LevelCount);
        LevelCount++;
        ShowXp.text = "level " + LevelCount + ": " + XPCount + " / " + XPNeeded.ToString("F0");
        if (LevelCount == 10)
        {
            XPMultiplier = 2;
            XPLuckMultiplier = 1.1f;
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount == 20)
        {
            XPMultiplier = 3;
            XPLuckMultiplier = 1.2f;
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount == 30)
        {
            XPMultiplier = 4;
            XPLuckMultiplier = 1.3f;
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount == 40)
        {
            XPMultiplier = 5;
            XPLuckMultiplier = 1.4f;
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount == 50)
        {
            XPMultiplier = 6;
            XPLuckMultiplier = 1.5f;
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount == 60)
        {
            XPMultiplier = 7;
            XPLuckMultiplier = 1.6f;
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount == 70)
        {
            XPMultiplier = 8;
            XPLuckMultiplier = 1.7f;
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount == 80)
        {
            XPMultiplier = 9;
            XPLuckMultiplier = 1.8f;
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount == 90)
        {
            XPMultiplier = 10;
            XPLuckMultiplier = 1.9f;
            RNGscript.RollSpeed -= 0.01f;
        }
        else if (LevelCount == 100)
        {
            XPMultiplier = 0;
            XPLuckMultiplier = 2;
            RNGscript.RollSpeed -= 0.01f;
            XPCount = 0;
        }
    }
}
