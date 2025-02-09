using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OreClass
{
    public string name;
    public string description;
    public string rarityTitle;
    public Sprite OrePicture;
    // rarity index
    // example 1 = 1 to common
    public int XP;
    public int OreID;
    // chance is displayed
    // if chance is 1 it displays 1 in 1
    public int chance;
    public int StorageAmount;
    public float Percentage;
    public float OrePrice;
    public Color rarityEffectColor;
    public Color Color;
}
