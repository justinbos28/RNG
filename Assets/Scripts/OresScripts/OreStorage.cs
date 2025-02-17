using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OreStorage : MonoBehaviour
{
    [Header("storage")]
    public int InventoryStatus = 0;
    public int InventoryOres = 5;

    public int MaxCommonOres = 500;
    public int MaxUncommonOres = 250;
    public int MaxRareOres = 125;
    public int MaxEpicOres = 75;
    public int MaxLegendaryOres = 25;
    public int MaxMythicOres = 5;
    public int MaxExoticOres = 2;
    public int MaxDivineOres = 1;

    [Header("lists")]
    public List<Text> Storage = new List<Text>();
    public List<Text> Description = new List<Text>();
    public List<Image> Image = new List<Image>();
    public List<Image> Color = new List<Image>();
    public List<Text> Name = new List<Text>();
    public List<Text> Price = new List<Text>();

    [Header("selling")]
    public List<InputField> InputFields = new List<InputField>();
    public RNGscript RNGscript;
    public MoneyLogic MoneyLogic;

    public void SwitchInventory()
    {
        // reset the input fields
        ResetInputFields();

        List<OreClass> currentOres = GetCurrentOres();
        UpdateUIWithOres(currentOres);

        InventoryStatus++;
        if (InventoryStatus > InventoryOres)
        {
            InventoryStatus = 0;
        }
    }
    public void UpdateInventory()
    {
        List<OreClass> currentOres = GetCurrentInventory();
        UpdateUiInventory(currentOres);
    }
    public void SellSelectedOres()
    {
        List<OreClass> currentOres = GetCurrentInventory();
        SellSelectedOresFromUI(currentOres);
    }
    private void ResetInputFields()
    {
        for (int j = 0; j < InputFields.Count; j++)
        {
            InputFields[j].text = "";
            InputFields[j].textComponent.color = new Vector4(0, 0, 0, 1);
        }
    }

    private List<OreClass> GetCurrentInventory()
    {
        switch (InventoryStatus)
        {
            case 1: return RNGscript.CommonOres;
            case 2: return RNGscript.UncommonOres;
            case 3: return RNGscript.RareOres;
            case 4: return RNGscript.EpicOres;
            case 5: return RNGscript.LegendaryOres;
            case 6: return RNGscript.MythicOres;
            case 7: return RNGscript.ExoticOres;
            case 0: return RNGscript.DivineOres;
            default: return new List<OreClass>();
        }
    }
    private List<OreClass> GetCurrentOres()
    {
        switch (InventoryStatus)
        {
            case 0: return RNGscript.CommonOres;
            case 1: return RNGscript.UncommonOres;
            case 2: return RNGscript.RareOres;
            case 3: return RNGscript.EpicOres;
            case 4: return RNGscript.LegendaryOres;
            case 5: return RNGscript.MythicOres;
            case 6: return RNGscript.ExoticOres;
            case 7: return RNGscript.DivineOres;
            default: return new List<OreClass>();
        }
    }

    private void UpdateUIWithOres(List<OreClass> ores)
    {
        for (int i = 0; i < ores.Count; i++)
        {
            Name[i].text = ores[i].name;
            Color[i].color = ores[i].Color;
            Image[i].sprite = ores[i].OrePicture;
            Price[i].text = ores[i].OrePrice.ToString();
            Storage[i].text = ores[i].StorageAmount.ToString();
            Description[i].text = ores[i].description;
            InputFields[i].enabled = true;
        }

        for (int i = ores.Count; i < Name.Count; i++)
        {
            Name[i].text = "Empty";
            Color[i].color = RNGscript.CommonOres[0].Color;
            Image[i].sprite = null;
            Price[i].text = "0";
            Storage[i].text = "0";
            Description[i].text = "Empty";
            InputFields[i].enabled = false;
        }
    }

    public void SetToDefault()
    {
        InventoryStatus = 1;
        for (int i = 0; i < RNGscript.CommonOres.Count; i++)
        {
            Name[i].text = RNGscript.CommonOres[i].name;
            Color[i].color = RNGscript.CommonOres[i].Color;
            Image[i].sprite = RNGscript.CommonOres[i].OrePicture;
            Price[i].text = RNGscript.CommonOres[i].OrePrice.ToString();
            Storage[i].text = RNGscript.CommonOres[i].StorageAmount.ToString();
            Description[i].text = RNGscript.CommonOres[i].description;
            InputFields[i].enabled = true;
        }

        for (int i = RNGscript.CommonOres.Count; i < Name.Count; i++)
        {
            Name[i].text = "Empty";
            Color[i].color = RNGscript.CommonOres[i].Color;
            Image[i].sprite = null;
            Price[i].text = "0";
            Storage[i].text = "0";
            Description[i].text = "Empty";
            InputFields[i].enabled = false;
        }
    }

    

    private void UpdateUiInventory(List<OreClass> ores)
    {
        for (int i = 0; i < ores.Count; i++)
        {
            Storage[i].text = ores[i].StorageAmount.ToString();
        }
    }

    private void SellSelectedOresFromUI(List<OreClass> ores)
    {
        for (int i = 0; i < InputFields.Count; i++) // Loop through SellOres
        {
            int sellAmount;
            if (!int.TryParse(InputFields[i].text, out sellAmount) || sellAmount <= 0)
            {
                continue; // Skip invalid inputs
            }

            if (sellAmount > ores[i].StorageAmount)
            {
                InputFields[i].textComponent.color = new Vector4(1, 0, 0, 1);
            }
            else
            {
                ores[i].StorageAmount -= sellAmount;
                InputFields[i].textComponent.color = new Vector4(0, 0, 0, 1);
                InputFields[i].text = "";
                MoneyLogic.Money += ores[i].OrePrice * RNGscript.MoneyMultiplier * sellAmount;

            }
        }
    }
}
