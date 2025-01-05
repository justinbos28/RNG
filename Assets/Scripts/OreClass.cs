using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OreClass
{
    public string name;
    public string decription;
    public string rarityTitle;
    public Sprite OrePicture;
    // rarity index
    // example 1 = 1 to common
    public int rarity;
    public int rarityCount;
    public int OreID;
    // chance is displayed
    // if chance is 1 it displays 1 in 1
    public int chance;
    public float OrePrice;
    public Color rarityEffectColor;
}
public class ShopItems
{
    public string ItemName;
    public float Price;
    public Sprite ItemImage;
}