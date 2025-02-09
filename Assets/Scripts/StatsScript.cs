using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatsScript : MonoBehaviour
{
    public Text StatText;
    public RNGscript RNGscript;
    public OreStorage OreStorage;
    // Update is called once per frame
    void LateUpdate()
    {
        StatText.text = "Roll Amount = " + RNGscript.cardLimit + "\n" 
            + "Roll speed = " + RNGscript.RollSpeed + "\n"
            + "Roll skips = " + RNGscript.RollSkips + "\n"
            + "Luck Percentage = " + RNGscript.LuckPercentage + " (1 = 100%)" + "\n"
            + "Luck Multiplier = " + RNGscript.LuckMultiplier + "\n"
            + "Money Multiplier = " + RNGscript.MoneyMultiplier + "\n" + "\n"
            + "Common ore storage = " + OreStorage.MaxCommonOres + "\n"
            + "Uncommon ore storage = " + OreStorage.MaxUncommonOres + "\n"
            + "Rare ore storage = " + OreStorage.MaxRareOres + "\n"
            + "Epic ore storage = " + OreStorage.MaxEpicOres + "\n"
            + "Legendary ore storage = " + OreStorage.MaxLegendaryOres + "\n"
            + "Mythic ore storage = " + OreStorage.MaxMythicOres + "\n";
    }
}
