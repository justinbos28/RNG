using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionBar : MonoBehaviour
{
    [SerializeField] private RectTransform fillRectTransform;
    public int MaxLength = 322;
    public int MaxPosition = 161;
    public float currentPosition = 5; // current postion is 5 pixels to the right of the left side of the bar
    public float CurrentLength = 0;

    // calculated values to determine how much to increase the length and position of the fill bar
    public float BarDivider;
    public float BarOffset;

    public XPScript XPScript;

    // dividing the max length by the xp needed to get the length increase per xp
    public void UpdateXPNeeded(float Value)
    {
        BarDivider = MaxLength / Value;
        BarOffset = MaxPosition / Value;
    }

    // if the player is at max level, set the bar to full
    public void BarSetMax()
    {
        currentPosition = MaxPosition + 5;
        CurrentLength = MaxLength;
        fillRectTransform.sizeDelta = new Vector2(CurrentLength, fillRectTransform.sizeDelta.y);
        fillRectTransform.anchoredPosition = new Vector2(currentPosition, fillRectTransform.anchoredPosition.y);
    }
    public void UpdateProgressionBar(float XPCount)
    {
        // getting the current length and position based on the xp count
        CurrentLength = BarDivider * XPCount;
        currentPosition = (BarOffset * XPCount) + 5;
        // if the current length is greater than the max length, reset the length and position, used when leveling up
        if (CurrentLength > MaxLength)
        {
            CurrentLength = 0;
            currentPosition = 5;
        }

        // updating the size and position of the fill bar
        fillRectTransform.sizeDelta = new Vector2(CurrentLength, fillRectTransform.sizeDelta.y);
        fillRectTransform.anchoredPosition = new Vector2(currentPosition, fillRectTransform.anchoredPosition.y);
    }
}
