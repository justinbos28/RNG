using UnityEngine;

public class CrafterCameraSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera crafterCamera;
    public Canvas mainCanvas;
    public Canvas crafterCanvas;

    public Buttonformachines Buttonformachines;
    public CraftingRecipes CraftingRecipes;
    public StoneCrusherlogic stoneCrusherLogic;
    [SerializeField] private RectTransform stoneCrusherPanel;

    public void GoToCrafter(string ButtonName)
    {
        mainCamera.enabled = false;
        crafterCamera.enabled = true;

        mainCanvas.enabled = false;
        crafterCanvas.enabled = true;
        stoneCrusherPanel.anchoredPosition = new Vector2(0, 500); // Reset position of stone crusher panel

        switch (ButtonName)
        {
            case "CrafterButton":
                Buttonformachines.CurrentMachine = "Crafter";
                CraftingRecipes.UpdateUi();
                break;
            case "StoneCrusherButton":
                Buttonformachines.CurrentMachine = "StoneCrusher";
                stoneCrusherLogic.UpdateCrusherUI(); 
                break;
            case "WasherButton":
                Buttonformachines.CurrentMachine = "Washer";
                // Washer logic
                break;
            case "SmelterButton":
                Buttonformachines.CurrentMachine = "Smelter";
                // Smelter logic
                break;
        }

    }

    public void CrafterToMain()
    {
        mainCamera.enabled = true;
        crafterCamera.enabled = false;

        mainCanvas.enabled = true;
        crafterCanvas.enabled = false;
    }
}
