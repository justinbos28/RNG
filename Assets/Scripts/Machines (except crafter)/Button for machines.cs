using UnityEngine;
using UnityEngine.UI;

public class Buttonformachines : MonoBehaviour
{
    public CraftingRecipes craftingRecipes;
    public StoneCrusherlogic stoneCrusherLogic;
    public Button Button;
    public string CurrentMachine;
    public void OnclickButton()
    {
        switch (CurrentMachine)
        {
            case "Crafter":
                craftingRecipes.RecipeIndex();
                break;
            case "StoneCrusher":
                stoneCrusherLogic.CraftStonePowderButtonOnClick();
                break;
            case "Washer":
                // craft button
                break;
            case "Smelter":
                // craft button
                break;
            default:
                Debug.LogWarning("Unknown machine type: " + CurrentMachine);
                break;
        }
    }

    public void ArrowButtonLeft()
    {
        switch (CurrentMachine)
        {
            case "Crafter":
                craftingRecipes.CraftingLeft();
                break;
            case "StoneCrusher":
                stoneCrusherLogic.CrusherLeft();
                break;
            case "Washer":
                // craft button
                break;
            case "Smelter":
                // craft button
                break;
            default:
                Debug.LogWarning("Unknown machine type: " + CurrentMachine);
                break;
        }
    }

    public void ArrowButtonRight()
    {
        switch (CurrentMachine)
        {
            case "Crafter":
                craftingRecipes.CraftingRight();
                break;
            case "StoneCrusher":
                stoneCrusherLogic.CrusherRight();
                break;
            case "Washer":
                // craft button
                break;
            case "Smelter":
                // craft button
                break;
            default:
                Debug.LogWarning("Unknown machine type: " + CurrentMachine);
                break;
        }
    }
}
