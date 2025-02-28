using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerUiSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera minerCamera;
    public Canvas mainCanvas;
    public Canvas minerCanvas;

    public DrillUnlocked drillUnlocked;
    public Minerscript minerscript;
    public void GoToDrills()
    {
        mainCamera.enabled = false;
        minerCamera.enabled = true;

        mainCanvas.enabled = false;
        minerCanvas.enabled = true;
    }

    public void GoToMain()
    {
        mainCamera.enabled = true;
        minerCamera.enabled = false;

        mainCanvas.enabled = true;
        minerCanvas.enabled = false;
    }

    public void ExitUpgrade()
    {
        drillUnlocked.UpgradePanel.SetActive(false);
    }
    public void ExitPurchase()
    {
        minerscript.DrillPanel.SetActive(false);
        for (int i = 0; i < minerscript.UnlockButtons.Count; i++)
        {
            minerscript.UnlockButtons[i].button.interactable = true;
        }
    }
}
