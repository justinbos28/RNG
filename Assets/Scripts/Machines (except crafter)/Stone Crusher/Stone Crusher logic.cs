using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using DG.Tweening;
using System.Threading.Tasks;

public class StoneCrusherlogic : MonoBehaviour, IDataPersistence
{
    [Header("text")]
    public Text StoneName;
    public Text PowderQuantity;
    public Text CrushingRequirement;
    public Text PossibleCrushedOutcomes;
    public Text PanelText;
    public Text PanelTitle;
    public Text ButtonName;

    [Header("Ints, floats and bools")]
    public int currentStoneIndex = 0; // Index of the current stone being processed
    private int PowderCrafted = 0; // Number of powders crafted in the current session
    [SerializeField] private float StartPositionPanel, EndPositionPanel, Duration;

    public bool isPurchased = false;

    [Header("lists and dictionaries")]
    public List<OreClass> StonesList = new List<OreClass>
    {
        new OreClass { Name = "Stone"},
        new OreClass { Name = "Hardstone"},
        new OreClass { Name = "Darkstone"},
        new OreClass { Name = "Redstone"}
    };
    public List<StonePowders> StonePowdersList;
    public List<StonePowders> GottenGems;
    public Dictionary<string, Dictionary<string, int>> StoneCrushingRequirements = new Dictionary<string, Dictionary<string, int>>
    {
        { "Stone" , new Dictionary<string, int>{ { "Stone", 10 }, { "Coal", 10 }, { "Wood", 5} } },   
        { "Hardstone" , new Dictionary<string, int>{ { "Hardstone", 10 }, { "Coal", 20 }, { "Wood", 15 }, { "Petroleum", 5} } },
        { "Darkstone" , new Dictionary<string, int>{ { "Darkstone", 10 }, { "Coal", 100 }, { "Petroleum", 30}, { "Fire Gem", 10 } } },
        { "Redstone" , new Dictionary<string, int>{ { "Redstone", 10 }, { "Petroleum", 100 }, { "Uranium", 10}, { "Power Gem", 100 }, { "Firestone", 1 } } }
    };
    public Dictionary<string, Dictionary<string, int>> PossibleOutcomesList = new Dictionary<string, Dictionary<string, int>>
    {
        { "Stone" , new Dictionary<string, int>{ { "Iron", 50 }, { "Copper", 30}, { "Petroleum", 15 }, { "Steel", 5 } } },
        { "Hardstone" , new Dictionary<string, int>{ { "Titanium", 60 }, { "Silver", 40}, { "Tungsten", 15 }, { "Gold", 5 }, { "Strange Sword Handle", 1 } } },
        { "Darkstone" , new Dictionary<string, int>{ { "Moonstone", 50 }, { "Starlight", 20}, { "Strange Sword Components", 5 }, { "Molten Chest", 1 } } },
        { "Redstone" , new Dictionary<string, int>{ { "Volcanium", 50 }, { "Magma Comet", 25}, { "Strange Sword Blade", 10 }, { "Molten Chest", 5 }, { "Secret Key", 1 } } }
    };
    public Dictionary<string, int> StoneCrusherReparationCost = new Dictionary<string, int>
    {
        { "Generator", 2 },
        { "Hardened Steel", 50 },
        { "Engine", 2 },
        { "Bolts", 60 },
        { "Wires", 15 },
        { "CircuitBoard", 1 },
        { "Steel", 75 },
        { "SteelFrame", 100 }
    };

    [Header("images and panels")]
    public GameObject PowderImage;
    public GameObject SuccessIndicatorPanel;
    public GameObject StoneCrusherPanel;

    [Header("inputfield  and buttons")]
    public InputField QuantityInput;
    public Button CraftStonePowderButton;

    [Header("scripts")]
    public CrafterCameraSwitch CrafterCameraSwitch;
    public CraftingRecipes CraftingRecipes;
    public RNGscript RNGscript;
    public XPScript XPScript;
    public Errormessages Errormessages;
    // end of header

    // data saving
    public void LoadData(GameData data)
    {
        this.isPurchased = data.isPurchased;
        
        if (data.StonePowdersList.Count != this.StonePowdersList.Count)
        {
            int difference = this.StonePowdersList.Count - data.StonePowdersList.Count;
            int dataStonePowdersCount = data.StonePowdersList.Count;
            for (int i = 0; i < difference; i++)
            {
                int currentPowderIndex = dataStonePowdersCount + i;
                // Create a new StonePowders object with the name and quantity
                data.StonePowdersList.Add(new StonePowders { PowderName = StonesList[currentPowderIndex].Name + " Powder", PowderQuantity = 0 });
            }
        }
        

        this.StonePowdersList = new List<StonePowders>(data.StonePowdersList);
    }
    public void SaveData(ref GameData data)
    {
        data.isPurchased = this.isPurchased;

        data.StonePowdersList = new List<StonePowders>(this.StonePowdersList);
    }
    // end of data saving

    public void StoneCrusherButtonOnClick()
    {        
        if (XPScript.SavedRebirth >= 1)
        {
            CrafterCameraSwitch.GoToCrafter("StoneCrusherButton");
            if (!isPurchased)
            {
                PannelAnimationStart(); // Call the purchase method if not purchased
                StoneCrusherPanel.SetActive(true);
                PanelTitle.text = "Crusher Reparation Requirement";
                PanelText.text = "\nThe Stone Crusher is broken. Repair it with the following items:\n" + string.Join("\n", StoneCrusherReparationCost.Select(r => $"{r.Key}: {r.Value}"));
                ButtonName.text = "Purchase";
            }
        }
        else
        {
            CraftStonePowderButton.interactable = true;
            Errormessages.SetErrorMessageMain("You need to rebirth at least once to use the stone crusher!"); // Show error message if player has not rebirthed
        }    
    }

    public async void CrusherPanelButtonOnClick()
    {
        if (!isPurchased)
        {
            PurchaseStoneCrusher(); // Call the purchase method if not purchased
        }
        else
        {
            CraftStonePowderButton.interactable = true;
            await PannelAnimationEnd(); // Wait for the panel animation to finish
            StoneCrusherPanel.SetActive(false); // Close the stone crusher panel if already purchased
        }
    }
    private void PurchaseStoneCrusher()
    {
        if (!isPurchased)
        {
            bool hasEnoughMaterials = StoneCrusherReparationCost.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            // craftingrecipes.materials.name is the key and craftingrecipes.materials.storageamount is the value
            if (hasEnoughMaterials)
            {
                // removing materials from storage
                foreach (var material in StoneCrusherReparationCost)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                }
                isPurchased = true;
                StoneCrusherPanel.SetActive(false); // Close the stone crusher panel after purchase
                Errormessages.SetErrorMessageMachines("You successfully purchased the stone crusher!"); // Show success message after purchase
                CraftStonePowderButton.interactable = true;
            }
            else
            {
                Errormessages.SetErrorMessageMachines("Not enough materials to purchase the stone crusher!"); // Show error message if player does not have enough materials
            }
        }
        else
        {
            Errormessages.SetErrorMessageMachines("You already purchased the stone crusher!");// Show error message if player has already purchased the stone crusher
            StoneCrusherPanel.SetActive(false); // Close the stone crusher panel if already purchased
        }
    }

    public void UpdateCrusherUI()
    {
        print("Updating Stone Crusher UI");
        StoneName.text = StonesList[currentStoneIndex].Name;
        PowderQuantity.text = "Powder: " + StonePowdersList[currentStoneIndex].PowderQuantity.ToString();
        CrushingRequirement.text = "Requirements:\n" + string.Join("\n", StoneCrushingRequirements.ElementAt(currentStoneIndex).Value.Select(r => $"{r.Key}: {r.Value}"));
        PossibleCrushedOutcomes.text = "Possible outcomes:\n2 to 5 stone powders\n" + string.Join("\n", PossibleOutcomesList.ElementAt(currentStoneIndex).Value.Select(r => $"{r.Key}: {r.Value}%"));
        PowderImage.GetComponent<Image>().sprite = StonesList[currentStoneIndex].OrePicture;
        SuccessIndicatorPanel.GetComponent<Image>().color = Color.white;
    }

    public void CraftStonePowderButtonOnClick()
    {
        CraftStonePowder(StonesList[currentStoneIndex].Name);
    }

    private void CraftStonePowder(string StoneName)
    {
        int quantity = 0; // amount being made
        if (int.TryParse(QuantityInput.text, out quantity) && quantity > 0)
        {
            var StoneCrusherRequirements = StoneCrushingRequirements[StoneName]; // get the requirements for the current stone out the dictionary

            bool hasEnoughMaterials = StoneCrusherRequirements.All(material =>
                CraftingRecipes.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value * quantity));
            // craftingrecipes.materials.name is the key and craftingrecipes.materials.storageamount is the value

            if (hasEnoughMaterials)
            {
                // removing materials from storage
                foreach (var material in StoneCrusherRequirements)
                {
                    var ore = CraftingRecipes.Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value * quantity;
                }
                AddPowder(quantity, StoneName);
            }
            else
            {
                Errormessages.SetErrorMessageMachines("Not enough materials to craft " + StoneName + " powder.");
            }
        }
        else
        {
            Errormessages.SetErrorMessageMachines("Invalid quantity input.");
        }
    }

    private void ShowOutcomesFromCrusher()
    {
        PannelAnimationStart();
        StoneCrusherPanel.SetActive(true);
        ButtonName.text = "Close";
        PanelTitle.text = "Stone Crusher Outcomes";
        if (GottenGems.Count > 0)
        {
            string outcomes = "Crafted " + PowderCrafted + " " + StonesList[currentStoneIndex].Name + " powder(s). Gotten:\n";
            foreach (var gem in GottenGems)
            {
                outcomes += $"{gem.PowderQuantity} {gem.PowderName}\n";
            }
            PanelText.text = outcomes;
        }
        else
        {
            PanelText.text = $"Crafted {PowderCrafted} {StonesList[currentStoneIndex].Name} powder(s), but no gems were obtained.";
        }
    }

    private void PannelAnimationStart()
    {
        StoneCrusherPanel.GetComponent<RectTransform>().DOAnchorPosY(EndPositionPanel, Duration);
    }

    private async Task PannelAnimationEnd()
    {
        await StoneCrusherPanel.GetComponent<RectTransform>().DOAnchorPosY(StartPositionPanel, Duration).AsyncWaitForCompletion();
    }

    private void AddPowder(int Quantity, string stoneName)
    {
        GottenGems.Clear();
        PowderCrafted = 0; // Reset the crafted powder count for this session
        int StonePowderOutcome;
        for (int i = 0; i < Quantity; i++)
        {
            StonePowderOutcome = Random.Range(2, 6);
            StonePowdersList[currentStoneIndex].PowderQuantity += StonePowderOutcome;
            PowderCrafted += StonePowderOutcome;

            int GemOutcome = Random.Range(0, 101);

            switch (stoneName)
            {
                case "Stone":
                    if (GemOutcome <= 5)
                    {
                        AddGem("Steel");
                    }
                    else if (GemOutcome <= 15)
                    {
                        AddGem("Petroleum");
                    }
                    else if (GemOutcome <= 30)
                    {
                        AddGem("Copper");
                    }
                    else if (GemOutcome <= 50)
                    {
                        AddGem("Iron");
                    }
                    else
                    {
                        Debug.Log("Stone Crusher: No gem outcome, just stone powder crafted.");
                    }
                        break;
                case "Hardstone":
                    if (GemOutcome <= 1)
                    {
                        AddGem("Strange Sword Handle");
                    }
                    else if (GemOutcome <= 5)
                    {
                        AddGem("Gold");
                    }
                    else if (GemOutcome <= 15)
                    {
                        AddGem("Tungsten");
                    }
                    else if (GemOutcome <= 40)
                    {
                        AddGem("Silver");
                    }
                    else if (GemOutcome <= 60)
                    {
                        AddGem("Titanium");
                    }
                    else
                    {
                        Debug.Log("Stone Crusher: No gem outcome, just stone powder crafted.");
                    }
                    break;
                case "Darkstone":
                    if (GemOutcome <= 1)
                    {
                        AddGem("Molten Chest");
                    }
                    else if (GemOutcome <= 5)
                    {
                        AddGem("Strange Sword Components");
                    }
                    else if (GemOutcome <= 20)
                    {
                        AddGem("Starlight");
                    }
                    else if (GemOutcome <= 50)
                    {
                        AddGem("Moonstone");
                    }
                    else
                    {
                        Debug.Log("Stone Crusher: No gem outcome, just stone powder crafted.");
                    }
                    break;
                case "Redstone":
                    if (GemOutcome <= 1)
                    {
                        AddGem("Secret Key");
                        RNGscript.UnlockedSecret = true; // Unlock the secret
                    }
                    else if (GemOutcome <= 5)
                    {
                        AddGem("Molten Chest");
                    }
                    else if (GemOutcome <= 15)
                    {
                        AddGem("Strange Sword Blade");
                    }
                    else if (GemOutcome <= 40)
                    {
                        AddGem("Magma Comet");
                    }
                    else if (GemOutcome <= 50)
                    {
                        AddGem("Volcanium");
                    }
                    else
                    {
                        Debug.Log("Stone Crusher: No gem outcome, just stone powder crafted.");
                    }
                    break;
            }
        }
        ShowOutcomesFromCrusher();
        UpdateCrusherUI();
    }

    private void AddGem(string GemName)
    {
        CraftingRecipes.Materials.FirstOrDefault(m => m.Name == GemName).StorageAmount += 1;

        bool hasGem = GottenGems.Any(gem => gem.PowderName == GemName);
        if (!hasGem)
        {
            // adds the gem to the list if it doesn't exist
            GottenGems.Add(new StonePowders { PowderName = GemName, PowderQuantity = 1 });
        }
        else
        {
            // increments the quantity of the gem if it already exists
            GottenGems.FirstOrDefault(gem => gem.PowderName == GemName).PowderQuantity += 1;
        }
    }

    public void CrusherLeft()
    {
        currentStoneIndex--;
        if (currentStoneIndex < 0)
        {
            currentStoneIndex = StonesList.Count - 1; // Loop back to the last stone
        }
        UpdateCrusherUI();
    }

    public void CrusherRight()
    {
        currentStoneIndex++;
        if (currentStoneIndex >= StonesList.Count)
        {
            currentStoneIndex = 0; // Loop back to the first stone
        }
        UpdateCrusherUI();
    }
}

[System.Serializable]
public class StonePowders
{
    public string PowderName;
    public int PowderQuantity;
}