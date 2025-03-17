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
        StatText.text = "Mining Amount = " + RNGscript.cardLimit + "\n" 
            + "Mining Speed = " + (RNGscript.RollSpeed * 100).ToString("F0") + " Seconds\n"
            + "Mining Strenght = " + (6 - RNGscript.RollSkips) + "\n"
            + "Luck Percentage = " + (RNGscript.LuckPercentage * 100).ToString("F0") + "%\n"
            + "Luck Multiplier = " + RNGscript.LuckMultiplier + "X\n"
            + "Money Multiplier = " + RNGscript.MoneyMultiplier + "X\n \n"
            + "Xp Multiplier = " + XPScript.XPMultiplier + "X\n"
            + "Xp Luck Multiplier = " + XPScript.XPLuckMultiplier.ToString("F1") + "X\n"
            + "Max Level = " + XPScript.MaxLevel + "\n"
            + "Rebirth = " + XPScript.SavedRebirth;

        StatTextTwo.text = "Common gem storage = " + OreStorage.MaxCommonOres + "\n"
            + "Uncommon gem storage = " + OreStorage.MaxUncommonOres + "\n"
            + "Rare gem storage = " + OreStorage.MaxRareOres + "\n"
            + "Epic gem storage = " + OreStorage.MaxEpicOres + "\n"
            + "Legendary gem storage = " + OreStorage.MaxLegendaryOres + "\n"
            + "Mythic gem storage = " + OreStorage.MaxMythicOres + "\n"
            + "Exotic gem storage = " + OreStorage.MaxExoticOres + "\n"
            + "Divine gem storage = " + OreStorage.MaxDivineOres;

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
