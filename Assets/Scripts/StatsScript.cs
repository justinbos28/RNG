using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatsScript : MonoBehaviour
{
    public Text StatText;
    public Text StatTextTwo;
    public RNGscript RNGscript;
    public OreStorage OreStorage;
    public XPScript XPScript;
    // Update is called once per frame
    void LateUpdate()
    {
        StatText.text = "Roll Amount = " + RNGscript.cardLimit + "\n" 
            + "Roll speed = " + RNGscript.RollSpeed.ToString("F2") + "\n"
            + "Roll skips = " + RNGscript.RollSkips + "\n"
            + "Luck Percentage = " + RNGscript.LuckPercentage.ToString("F1") + " (1 = 100%)" + "\n"
            + "Luck Multiplier = " + RNGscript.LuckMultiplier + "\n"
            + "Money Multiplier = " + RNGscript.MoneyMultiplier + "\n" + "\n"
            + "Common ore storage = " + OreStorage.MaxCommonOres + "\n"
            + "Uncommon ore storage = " + OreStorage.MaxUncommonOres + "\n"
            + "Rare ore storage = " + OreStorage.MaxRareOres + "\n"
            + "Epic ore storage = " + OreStorage.MaxEpicOres + "\n"
            + "Legendary ore storage = " + OreStorage.MaxLegendaryOres + "\n"
            + "Mythic ore storage = " + OreStorage.MaxMythicOres + "\n";

        StatTextTwo.text = "Xp Multiplier = " + XPScript.XPMultiplier + "\n"
            + "Xp Luck Multiplier = " + XPScript.XPLuckMultiplier.ToString("F1") + "\n";
    }
}
