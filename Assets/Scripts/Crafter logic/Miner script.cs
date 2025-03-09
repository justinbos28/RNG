using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Minerscript : MonoBehaviour
{

    public List<OreClass> Materials = new List<OreClass>();
    public List<ButtonClass> UnlockButtons = new List<ButtonClass>();
    public List<GameObject> UnlockPanels = new List<GameObject>();
    public Text DrillNumber;
    public Text DrillPrice;
    public GameObject DrillPanel;

    public RNGscript RNGscript;
    public DrillUnlocked DrillUnlocked;

    public int MaterialAmount;
    public string MaterialName;
    public int savedIndex;

    public Dictionary<string, int> RequiredGems = new Dictionary<string, int>
    {
        { "Stone", 0 },
        { "Coal", 0 },
        { "Sand", 0 },
        { "Wood", 0 },
        { "Stone Gem", 0 },
        { "Rusty Gem", 0 },
        { "Iron", 500 },
        { "Steel", 250 }
    };



    private void LogRequiredGems()
    {
        foreach (var gem in RequiredGems)
        {
            Debug.Log($"Gem: {gem.Key}, Amount: {gem.Value}");
        }
    }
    public void UnlockDrill()
    {  
        if (DrillPanel.activeSelf == false)
        {
            DrillPanel.SetActive(true);
        }
        else
        {
            DrillPanel.SetActive(false);
        }
        for (int i = 0; i < UnlockButtons.Count; i++)
        {
            UnlockButtons[i].button.interactable = false;
        }
        ResetRequirements();
        GetPressedButton();
    }

    private void GetPressedButton()
    {
        for (int i = 0; i < UnlockButtons.Count; i++)
        {
            int index = i;
            UnlockButtons[i].button.onClick.RemoveAllListeners();
            UnlockButtons[i].button.onClick.AddListener(() =>
            {
                savedIndex = index;
                DrillNumber.text = "Unlock drill " + (index + 1);

                NewRequirement();
            });
        }
    }

    private void NewRequirement()
    {
        switch (savedIndex)
        {
            case 0: MaterialAmount = 450; MaterialName = " Stone"; RequiredGems["Stone"] = 450; break;
            case 1: MaterialAmount = 500; MaterialName = " Coal"; RequiredGems["Coal"] = 500; break;
            case 2: MaterialAmount = 350; MaterialName = " Sand"; RequiredGems["Sand"] = 350; break;
            case 3: MaterialAmount = 400; MaterialName = " Wood"; RequiredGems["Wood"] = 400; break;
            case 4: MaterialAmount = 1000; MaterialName = " Iron"; RequiredGems["Iron"] = 500; break;
            case 5: MaterialAmount = 1000; MaterialName = " Stone Gem"; RequiredGems["Stone Gem"] = 1000; break;
            case 6: MaterialAmount = 1000; MaterialName = " Rusty Gem"; RequiredGems["Rusty Gem"] = 1000; break;
        }
        DrillPrice.text = "Price: " + MaterialAmount + MaterialName + "\n"
                + "1 DrillHead \n 1 Generator \n 250 Steel \n 500 Iron \n 30 Wires \n 2 engine";
    }

    private void ResetRequirements()
    {
        for (int i = 0; i < UnlockButtons.Count; i++)
        {
            switch (i)
            {
                case 0: RequiredGems["Stone"] = 0; break;
                case 1: RequiredGems["Coal"] = 0; break;
                case 2: RequiredGems["Sand"] = 0; break;
                case 3: RequiredGems["Wood"] = 0; break;
                case 4: RequiredGems["Iron"] = 500; break;
                case 5: RequiredGems["Stone Gem"] = 0; break;
                case 6: RequiredGems["Rusty Gem"] = 0; break;
            }
        }
    }


    public void BuyDrill()
    {
        var requiredMaterials = new Dictionary<string, int>
        {
            { "DrillHead", 1 }, 
            { "Generator", 1 }, 
            { "Wires", 30 }, 
            { "Engine", 2 } 
        };

        bool hasEnoughMaterials = requiredMaterials.All(material =>
            Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));

        bool HasEnoughGems = RequiredGems.All(Gems =>
            RNGscript.allOres.Any(G => G.Name == Gems.Key && G.StorageAmount >= Gems.Value));

        if (hasEnoughMaterials && HasEnoughGems)
        {
            foreach (var material in requiredMaterials)
            {
                var item = Materials.First(m => m.Name == material.Key);
                item.StorageAmount -= material.Value;
            }
            foreach (var gem in RequiredGems)
            {
                var Ores = RNGscript.allOres.First(G => G.Name == gem.Key);
                Ores.StorageAmount -= gem.Value;
            }

            DrillPanel.SetActive(false);
            DrillUnlocked.DrillList[savedIndex].IsUnlocked = true;
            DrillUnlocked.DrillList[savedIndex].IsActive = true;

            for (int i = 0; i < UnlockButtons.Count; i++)
            {
                UnlockButtons[i].button.interactable = true;
                UnlockPanels[savedIndex].SetActive(false);
            }
        }
        else
        {
            DrillPanel.SetActive(false);
            for (int i = 0; i < UnlockButtons.Count; i++)
            {
                UnlockButtons[i].button.interactable = true;
            }
            DrillUnlocked.FailedRequirements = true;
        }
    }

    private void Start()
    {
        UnlockDrill();
        DrillPanel.SetActive(false);
        for (int i = 0; i < UnlockButtons.Count; i++)
        {
            UnlockButtons[i].ID = i;
            UnlockButtons[i].button.interactable = true;
        }

        for (int i = 0; i < Materials.Count; i++)
        {
            Materials[i].OreID = i;
        }

        ResetRequirements();
    }
}
