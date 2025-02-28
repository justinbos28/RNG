using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class CraftingRecipes : MonoBehaviour
{
    public Minerscript Minerscript;
    public RNGscript RNGscript;
    public Button craftButton;
    public string recipeName;
    public int CurrentRecipe;
    public List<OreClass> Materials = new List<OreClass>();


    public Dictionary<string, Dictionary<string, int>> Recipes = new Dictionary<string, Dictionary<string, int>>
    {
        { "DrillHead", new Dictionary<string, int> { {"Steel", 200}, {"Iron", 150}, {"Tungsten", 2} } },
        { "Glass", new Dictionary<string, int> { {"Coal", 10}, {"Sand", 25} } },
        { "Wires", new Dictionary<string, int> { {"Copper", 20}, {"Plastic", 10} } },
        { "Plastic", new Dictionary<string, int> { {"Petroleum", 10}, {"Coal", 20} } },
        { "Motor", new Dictionary<string, int> { {"Steel", 30}, {"Iron", 10}, {"Silver", 1}, {"Petroleum", 2}, {"SteelFrame", 1} } },
        { "SteelFrame", new Dictionary<string, int> { {"Steel", 10}, {"Iron", 50} } },
        { "Engine", new Dictionary<string, int> { {"Motor", 10}, {"Titanium", 2}, {"Iron", 20}, {"Petroleum", 1}, {"Copper", 5}, {"Gold", 1}, {"SteelFrame", 5} } },
        { "Generator", new Dictionary<string, int> { {"Copper", 20}, {"Wire", 10}, {"Motor", 1}, {"Iron", 50}, {"SteelFrame", 10}, {"Power Gem", 1} } },
        { "WoodFrame", new Dictionary<string, int> { {"Wood", 50} } },
        { "CircuitBoard", new Dictionary<string, int> { {"Copper", 10}, {"Gold", 1}, {"Steel", 20}, {"Fast Gem", 15}, {"Power Gem", 5} } }
    };

    public void RecipeIndex()
    {
        craftButton.onClick.RemoveAllListeners();
        craftButton.onClick.AddListener(() =>
        {
            recipeName = Recipes.ElementAt(CurrentRecipe).Key;
            GetRecipe(recipeName);
        });
    }
    private void GetRecipe(string recipeName)
    {
        Debug.Log($"Recipe: {recipeName}");
        if (Recipes.ContainsKey(recipeName))
        {
            var recipe = Recipes[recipeName];
            bool hasEnoughMaterials = recipe.All(material =>
                Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));
            print(hasEnoughMaterials);
        }
        else
        {
            Debug.Log("Recipe not found");
        }
    }




    // fusing of the rng ores and the miner ores
    private void Start()
    {
        RecipeIndex();
        Materials = Minerscript.Materials.Cast<OreClass>().Concat(RNGscript.allOres).ToList();
    }
}
