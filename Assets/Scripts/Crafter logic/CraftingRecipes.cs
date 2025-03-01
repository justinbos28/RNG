using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CraftingRecipes : MonoBehaviour, IDataPersistence
{
    public Minerscript Minerscript;
    public RNGscript RNGscript;
    public Button craftButton;

    public Text MaterialName;
    public Text MaterialAmount;
    public Text MaterialPrice;
    public Text MaterialRequirements;
    public Image MaterialImage;
    public Image IsInvalid;

    public string recipeName;
    public int CurrentRecipe;
    public int CraftingAmount;

    public List<OreClass> Materials = new List<OreClass>();
    public List<int> MaterialCount = new List<int>();
    public Dictionary<string, Dictionary<string, int>> Recipes = new Dictionary<string, Dictionary<string, int>>
    {
        { "DrillHead", new Dictionary<string, int> { {"Steel", 200}, {"Iron", 150}, {"Tungsten", 2} } },
        { "Glass", new Dictionary<string, int> { {"Coal", 10}, {"Sand", 25} } },
        { "Wires", new Dictionary<string, int> { {"Copper", 20}, {"Plastic", 10} } },
        { "Plastic", new Dictionary<string, int> { {"Petroleum", 10}, {"Coal", 20} } },
        { "Motor", new Dictionary<string, int> { {"Steel", 30}, {"Iron", 10}, {"Silver", 1}, {"Petroleum", 2}, {"SteelFrame", 1} } },
        { "SteelFrame", new Dictionary<string, int> { {"Steel", 10}, {"Iron", 50} } },
        { "Engine", new Dictionary<string, int> { {"Motor", 10}, {"Titanium", 2}, {"Iron", 20}, {"Petroleum", 1}, {"Copper", 5}, {"Gold", 1}, {"SteelFrame", 5} } },
        { "Generator", new Dictionary<string, int> { {"Copper", 20}, {"Wires", 10}, {"Motor", 1}, {"Iron", 50}, {"SteelFrame", 10}, {"Power Gem", 1} } },
        { "WoodFrame", new Dictionary<string, int> { {"Wood", 50} } },
        { "CircuitBoard", new Dictionary<string, int> { {"Copper", 10}, {"Gold", 1}, {"Steel", 20}, {"Fast Gem", 15}, {"Power Gem", 5} } }
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
                Minerscript.Materials[CurrentRecipe].StorageAmount += CraftingAmount;
                UpdateUi();
                IsInvalid.color = Color.white;
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
            case 5: CraftingAmount = 5; break;
            case 6: CraftingAmount = 1; break;
            case 7: CraftingAmount = 1; break;
            case 8: CraftingAmount = 10; break;
            case 9: CraftingAmount = 1; break;
            default: CraftingAmount = 1; break;
        }
    }

    public void Backwards()
    {
        if (CurrentRecipe <= 0)
        {
            CurrentRecipe = 0;
        }
        else
        {
            CurrentRecipe--;
        }
        IsInvalid.color = Color.white;
        UpdateUi();
    }
    public void Forward()
    {
        if (CurrentRecipe >= (Recipes.Count - 1))
        {
            CurrentRecipe = (Recipes.Count - 1);
        }
        else
        {
            CurrentRecipe++;
        }
        IsInvalid.color = Color.white;
        UpdateUi();
    }

    public void UpdateUi()
    {
        MaterialName.text = Minerscript.Materials[CurrentRecipe].Name;
        MaterialAmount.text = "Stored: " + Minerscript.Materials[CurrentRecipe].StorageAmount.ToString();
        MaterialPrice.text = Minerscript.Materials[CurrentRecipe].OrePrice.ToString();
        MaterialImage.sprite = Minerscript.Materials[CurrentRecipe].OrePicture;
        MaterialRequirements.text = string.Join("\n", Recipes.ElementAt(CurrentRecipe).Value.Select(r => $"{r.Key}: {r.Value}"));
    }

    private void Start()
    {
        RecipeIndex();
        Materials = Minerscript.Materials.Cast<OreClass>().Concat(RNGscript.allOres).ToList();
    }
}
