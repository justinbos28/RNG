using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    public GameObject InformationPanel;
    public bool isOpen = false;
    public void OpenInformation()
    {
        if (isOpen)
        {
            InformationPanel.SetActive(false);
            isOpen = false;
        }
        else
        {
            InformationPanel.SetActive(true);
            isOpen = true;
        }
    }
}
