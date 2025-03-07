using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ButtonHovers : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject Hover;

    private bool mouse_over = false;

    void Update()
    {
        if (mouse_over)
        {
            Hover.SetActive(true);
        }
        else
        {
            Hover.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse_over = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse_over = false;
    }
}
