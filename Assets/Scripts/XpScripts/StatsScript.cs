using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StatsScript : MonoBehaviour
{
    public Text StatText;
    public Text StatTextTwo;
    public Text RebirthText;
    public RNGscript RNGscript;
    public OreStorage OreStorage;
    public XPScript XPScript;
    public string Mine;
    public Image RebirthImage;
    public List <Sprite> RebirthImages;

    public GameObject RebirthMenu;
    // Update is called once per frame
    void LateUpdate()
    {
        StatText.text = "Roll Amount = " + RNGscript.cardLimit + "\n" 
            + "Roll speed = " + RNGscript.RollSpeed.ToString("F2") + "\n"
            + "Roll skips = " + RNGscript.RollSkips + "\n"
            + "Luck Percentage = " + RNGscript.LuckPercentage.ToString("F1") + " (1 = 100%)" + "\n"
            + "Luck Multiplier = " + RNGscript.LuckMultiplier + "\n"
            + "Money Multiplier = " + RNGscript.MoneyMultiplier + "\n" + "\n"
            + "Xp Multiplier = " + XPScript.XPMultiplier + "\n"
            + "Xp Luck Multiplier = " + XPScript.XPLuckMultiplier.ToString("F1") + "\n"
            + "Max Level = " + XPScript.MaxLevel + "\n"
            + "Rebirth = " + XPScript.Rebirth;

        StatTextTwo.text = "Common ore storage = " + OreStorage.MaxCommonOres + "\n"
            + "Uncommon ore storage = " + OreStorage.MaxUncommonOres + "\n"
            + "Rare ore storage = " + OreStorage.MaxRareOres + "\n"
            + "Epic ore storage = " + OreStorage.MaxEpicOres + "\n"
            + "Legendary ore storage = " + OreStorage.MaxLegendaryOres + "\n"
            + "Mythic ore storage = " + OreStorage.MaxMythicOres + "\n";

        if (XPScript.Rebirth == 0)
        {
            Mine = "The Forgotten Mine";
        }
        else if (XPScript.Rebirth == 1)
        {
            Mine = "The Inferno";
        }
        else
        {
            Mine = "No new mine";
        }
    }

    public void OpenRebirthMenu()
    {
        RebirthMenu.SetActive(true);
        if (XPScript.Rebirth > 1)
        {
            RebirthImage.sprite = RebirthImages[1];
        }
        else
        {
            RebirthImage.sprite = RebirthImages[XPScript.Rebirth];
        }

        RebirthText.text = "Rebirth " + (XPScript.Rebirth + 1) + "\n"
          + "Max Level " + (XPScript.MaxLevel + 50) + "\n"
          + "Mine: " + Mine;
    }
    public void ExitRebirth()
    {
        RebirthMenu.SetActive(false);
    }
}
