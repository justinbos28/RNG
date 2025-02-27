using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerUiSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Camera minerCamera;
    public Canvas mainCanvas;
    public Canvas minerCanvas;
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
}
