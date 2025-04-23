using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CraftingRecipes : MonoBehaviour, IDataPersistence
{
    public Minerscript Minerscript;
    public RNGscript RNGscript;
    public TutorialManager TutorialManager;
    public Button craftButton;

    public Text MaterialName;
    public Text MaterialAmount;
    public Text MaterialPrice;
    public Text MaterialRequirements;
    public Text Output;
    public Image MaterialImage;
    public Image IsInvalid;

    public string recipeName;
    public int CurrentRecipe;
    public int CraftingAmount = 1;

    public List<OreClass> Materials = new List<OreClass>();
    public List<int> MaterialCount = new List<int>();
    public Dictionary<string, Dictionary<string, int>> Recipes = new Dictionary<string, Dictionary<string, int>>
    {
        { "DrillHead", new Dictionary<string, int> { {"Steel", 100}, {"Iron", 300}, {"Tungsten", 2} } },
        { "Glass", new Dictionary<string, int> { {"Coal", 10}, {"Sand", 25} } },
        { "Wires", new Dictionary<string, int> { {"Copper", 20}, {"Plastic", 10} } },
        { "Plastic", new Dictionary<string, int> { {"Petroleum", 10}, {"Coal", 20} } },
        { "Motor", new Dictionary<string, int> { {"Steel", 10}, {"Iron", 30}, {"Silver", 1}, {"Petroleum", 2}, {"SteelFrame", 1} } },
        { "SteelFrame", new Dictionary<string, int> { {"Steel", 10}, {"Iron", 25} } },
        { "Engine", new Dictionary<string, int> { {"Motor", 10}, {"Titanium", 2}, {"Iron", 20}, {"Petroleum", 1}, {"Copper", 5}, {"Gold", 1}, {"SteelFrame", 5} } },
        { "Generator", new Dictionary<string, int> { {"Copper", 20}, {"Wires", 10}, {"Motor", 1}, {"Iron", 50}, {"SteelFrame", 10} } },
        { "WoodFrame", new Dictionary<string, int> { {"Wood", 10} } },
        { "CircuitBoard", new Dictionary<string, int> { {"Copper", 10}, {"Silver", 1}, {"Steel", 20}, {"Fast Gem", 5} } },
        { "Clean Gem", new Dictionary<string, int> { {"Rusty Gem", 10}, { "Stone Gem", 5 }, { "Water Gem", 1 }, { "Wood", 1} } },
        { "Hardened Steel", new Dictionary<string, int> { { "Petroleum", 10 }, { "Steel", 5 }, { "Heat Gem", 1 } } },
        { "Rusty Drill", new Dictionary<string, int> { {"WoodFrame", 5}, {"Iron", 6 } } },
        { "Coal Generator", new Dictionary<string, int> { {"Wood", 15}, { "Coal", 10 }, { "Iron", 10 } } },
        { "Bolts", new Dictionary<string, int> { {"Iron", 6} } },
        { "Medium Drill", new Dictionary<string, int> { {"Steel", 5}, { "Iron", 20 }, { "WoodFrame", 10 } } },
        { "Small Generator", new Dictionary<string, int> { {"SteelFrame", 1}, { "Petroleum", 10 }, { "Plastic", 10 }, { "Heat Gem", 1 } } },
        { "Standard Drill", new Dictionary<string, int> { {"Iron", 50}, { "Steel", 25 }, { "Wires", 5 }, { "Titanium", 5 } } },
        { "Heat Generator", new Dictionary<string, int> { {"Generator", 1}, { "Hardened Steel", 10 }, { "Heat Gem", 50 }, { "Fire Gem", 5 }, { "Quartz", 10 } } },
        { "Duplicator", new Dictionary<string, int> { {"Generator", 1}, { "Motor", 1 }, { "Copper", 10 }, { "Wires", 5 }, { "SteelFrame", 10 } } },
        { "steel", new Dictionary<string, int> { {"Coal", 10}, {"Iron", 5 } } }
    };

    // start of saving and loading data
    public void LoadData(GameData data)
    {
        // if the data is smaller then the materials list, add the difference to the data list
        if (data.MaterialCount.Count != this.MaterialCount.Count)
        {
            int difference = this.MaterialCount.Count - data.MaterialCount.Count;
            for (int i = 0; i < difference; i++)
            {
                data.MaterialCount.Add(0);
            }
        }

        if (data.MaterialCount.Count != this.Minerscript.Materials.Count)
        {
            int difference = this.Minerscript.Materials.Count - data.MaterialCount.Count;
            for (int i = 0; i < difference; i++)
            {
                data.MaterialCount.Add(0);
            }
        }

        // if the data is null or empty, create a new list with the same length as the materials list
        if (data.MaterialCount == null || data.MaterialCount.Count == 0)
        {
            data.MaterialCount = new List<int>(new int[Minerscript.Materials.Count]);
        }

        // adds the data to the materials list
        this.MaterialCount = new List<int>(data.MaterialCount);

        // sets the storage amount of the ores to the amount in the materials list
        for (int i = 0; i < Minerscript.Materials.Count; i++)
        {
            Minerscript.Materials[i].StorageAmount = MaterialCount[i];
        }
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < Minerscript.Materials.Count; i++)
        {
            MaterialCount[i] = Minerscript.Materials[i].StorageAmount;
        }

        data.MaterialCount = new List<int>(MaterialCount);
    }

    void LateUpdate()
    {
        for (int i = 0; i < Minerscript.Materials.Count; i++)
        {
            MaterialCount[i] = Minerscript.Materials[i].StorageAmount;
        }
    }
    // end of saving and loading data

    public void RecipeIndex()
    {
        craftButton.onClick.RemoveAllListeners();
        craftButton.onClick.AddListener(() =>
        {
            recipeName = Recipes.ElementAt(CurrentRecipe).Key;
            SetAmount();
            GetRecipe(recipeName);
        });
    }

    private void GetRecipe(string recipeName)
    {
        if (Recipes.ContainsKey(recipeName))
        {
            var recipe = Recipes[recipeName];

            bool hasEnoughMaterials = recipe.All(material =>
                Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));

            if (hasEnoughMaterials)
            {
                foreach (var material in recipe)
                {
                    var ore = Materials.FirstOrDefault(m => m.Name == material.Key);
                    ore.StorageAmount -= material.Value;
                    
                }

                if (CurrentRecipe == 20)
                {
                    RNGscript.allOres[13].StorageAmount += CraftingAmount;
                }
                else
                {
                    Minerscript.Materials[CurrentRecipe].StorageAmount += CraftingAmount;
                }
                UpdateUi();
                IsInvalid.color = Color.white;
                TutorialManager.UpdateRequirementsText();
            }
            else
            {
                IsInvalid.color = Color.red;            
            }
        }
        else
        {
            Debug.Log("Recipe not found");
        }
    }

    private void SetAmount()
    {
        switch (CurrentRecipe)
        {
            case 0: CraftingAmount = 1; break;
            case 1: CraftingAmount = 10; break;
            case 2: CraftingAmount = 5; break;
            case 3: CraftingAmount = 10; break;
            case 4: CraftingAmount = 1; break;
            case 5: CraftingAmount = 10; break;
            case 6: CraftingAmount = 1; break;
            case 7: CraftingAmount = 1; break;
            case 8: CraftingAmount = 10; break;
            case 9: CraftingAmount = 1; break;
            case 10: CraftingAmount = 1; break;
            case 11: CraftingAmount = 5; break;
            case 12: CraftingAmount = 1; break;
            case 13: CraftingAmount = 1; break;
            case 14: CraftingAmount = 6; break;
            case 15: CraftingAmount = 1; break;
            case 16: CraftingAmount = 1; break;
            case 17: CraftingAmount = 1; break;
            case 18: CraftingAmount = 1; break;
            case 19: CraftingAmount = 1; break;
            case 20: CraftingAmount = 2; break;
            default: CraftingAmount = 1; break;
        }
    }

    public void Backwards()
    {
        if (CurrentRecipe <= 0)
        {
            CurrentRecipe = Recipes.Count - 1;
        }
        else
        {
            CurrentRecipe--;
        }
        IsInvalid.color = Color.white;
        SetAmount();
        UpdateUi();
    }
    public void Forward()
    {
        if (CurrentRecipe >= (Recipes.Count - 1))
        {
            CurrentRecipe = 0;
        }
        else
        {
            CurrentRecipe++;
        }
        IsInvalid.color = Color.white;
        SetAmount();
        UpdateUi();
    }

    public void UpdateUi()
    {
        if (CurrentRecipe == 20)
        {
            MaterialName.text = Minerscript.Materials[CurrentRecipe].Name;
            MaterialAmount.text = "Stored: " + RNGscript.allOres[13].StorageAmount.ToString();
            MaterialPrice.text = Minerscript.Materials[CurrentRecipe].OrePrice.ToString();
            MaterialImage.sprite = Minerscript.Materials[CurrentRecipe].OrePicture;
            MaterialRequirements.text = string.Join("\n", Recipes.ElementAt(CurrentRecipe).Value.Select(r => $"{r.Key}: {r.Value}"));
            Output.text = "Output: " + CraftingAmount.ToString();
        } 
        else
        {
            MaterialName.text = Minerscript.Materials[CurrentRecipe].Name;
            MaterialAmount.text = "Stored: " + Minerscript.Materials[CurrentRecipe].StorageAmount.ToString();
            MaterialPrice.text = Minerscript.Materials[CurrentRecipe].OrePrice.ToString();
            MaterialImage.sprite = Minerscript.Materials[CurrentRecipe].OrePicture;
            MaterialRequirements.text = string.Join("\n", Recipes.ElementAt(CurrentRecipe).Value.Select(r => $"{r.Key}: {r.Value}"));
            Output.text = "Output: " + CraftingAmount.ToString();
        }
    }

    private void Start()
    {
        TutorialManager = FindObjectOfType<TutorialManager>();
        RecipeIndex();
        Materials = Minerscript.Materials.Cast<OreClass>().Concat(RNGscript.allOres).ToList();
    }
}
