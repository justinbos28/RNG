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
    public Uimanager Uimanager;
    public string Mine;
    public Image RebirthImage;
    public List <Sprite> RebirthImages;
    public List <Material> Skyboxes;
    public Skybox Skybox;   

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
            + "Rebirth = " + XPScript.SavedRebirth;

        StatTextTwo.text = "Common ore storage = " + OreStorage.MaxCommonOres + "\n"
            + "Uncommon ore storage = " + OreStorage.MaxUncommonOres + "\n"
            + "Rare ore storage = " + OreStorage.MaxRareOres + "\n"
            + "Epic ore storage = " + OreStorage.MaxEpicOres + "\n"
            + "Legendary ore storage = " + OreStorage.MaxLegendaryOres + "\n"
            + "Mythic ore storage = " + OreStorage.MaxMythicOres + "\n"
            + "Exotic ore storage = " + OreStorage.MaxExoticOres + "\n"
            + "Divine ore storage = " + OreStorage.MaxDivineOres;

        if (XPScript.SavedRebirth == 0)
        {
            Mine = "The Forgotten Mine";
        }
        else if (XPScript.SavedRebirth == 1)
        {
            Mine = "The Inferno";
        }
        else
        {
            Mine = "No new mine";
        }

        if (XPScript.Rebirth > 1)
        {
            Skybox.material = Skyboxes[2];
        }
        else
        {
            Skybox.material = Skyboxes[XPScript.Rebirth];
        }
    }

    public void OpenRebirthMenu()
    {
        if (Uimanager.OpenPortal == true || Uimanager.Stats == true)
        {
            Uimanager.OpenPortal = false;
            Uimanager.Teleporter.SetActive(false);

            Uimanager.OpenStats = false;
            Uimanager.Stats.SetActive(false);
        }
        RebirthMenu.SetActive(true);
        if (XPScript.SavedRebirth > 1)
        {
            RebirthImage.sprite = RebirthImages[1];
            Skybox.material = Skyboxes[1];
        }
        else
        {
            RebirthImage.sprite = RebirthImages[XPScript.Rebirth];
            Skybox.material = Skyboxes[XPScript.Rebirth];
        }

        RebirthText.text = "Rebirth " + (XPScript.SavedRebirth + 1) + "\n"
          + "Max Level " + (XPScript.MaxLevel + 50) + "\n"
          + "Mine: " + Mine;
    }
    public void ExitRebirth()
    {
        RebirthMenu.SetActive(false);
    }
}
