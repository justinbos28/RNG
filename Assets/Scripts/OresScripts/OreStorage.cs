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

    public int MaxCommonOres = 500;
    public int MaxUncommonOres = 250;
    public int MaxRareOres = 125;
    public int MaxEpicOres = 75;
    public int MaxLegendaryOres = 25;
    public int MaxMythicOres = 5;

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
        for(int j = 0; j < InputFields.Count; j++)
        {
            InputFields[j].text = "";
            InputFields[j].textComponent.color = new Vector4(0, 0, 0, 1);
        }
        if (InventoryStatus == 0)
        {
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
        else if (InventoryStatus == 1)
        {
            for (int i = 0; i < RNGscript.UncommonOres.Count; i++)
            {
                Name[i].text = RNGscript.UncommonOres[i].name;
                Color[i].color = RNGscript.UncommonOres[i].Color;
                Image[i].sprite = RNGscript.UncommonOres[i].OrePicture;
                Price[i].text = RNGscript.UncommonOres[i].OrePrice.ToString();
                Storage[i].text = RNGscript.UncommonOres[i].StorageAmount.ToString();
                Description[i].text = RNGscript.UncommonOres[i].description;
                InputFields[i].enabled = true;
            }
            for (int i = RNGscript.UncommonOres.Count; i < Name.Count; i++)
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
        else if (InventoryStatus == 2)
        {
            for (int i = 0; i < RNGscript.RareOres.Count; i++)
            {
                Name[i].text = RNGscript.RareOres[i].name;
                Color[i].color = RNGscript.RareOres[i].Color;
                Image[i].sprite = RNGscript.RareOres[i].OrePicture;
                Price[i].text = RNGscript.RareOres[i].OrePrice.ToString();
                Storage[i].text = RNGscript.RareOres[i].StorageAmount.ToString();
                Description[i].text = RNGscript.RareOres[i].description;
                InputFields[i].enabled = true;
            }
            for (int i = RNGscript.RareOres.Count; i < Name.Count; i++)
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
        else if (InventoryStatus == 3)
        {
            for (int i = 0; i < RNGscript.EpicOres.Count; i++)
            {
                Name[i].text = RNGscript.EpicOres[i].name;
                Color[i].color = RNGscript.EpicOres[i].Color;
                Image[i].sprite = RNGscript.EpicOres[i].OrePicture;
                Price[i].text = RNGscript.EpicOres[i].OrePrice.ToString();
                Storage[i].text = RNGscript.EpicOres[i].StorageAmount.ToString();
                Description[i].text = RNGscript.EpicOres[i].description;
                InputFields[i].enabled = true;
            }
            for (int i = RNGscript.EpicOres.Count; i < Name.Count; i++)
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
        else if (InventoryStatus == 4)
        {
            for (int i = 0; i < RNGscript.LegendaryOres.Count; i++)
            {
                Name[i].text = RNGscript.LegendaryOres[i].name;
                Color[i].color = RNGscript.LegendaryOres[i].Color;
                Image[i].sprite = RNGscript.LegendaryOres[i].OrePicture;
                Price[i].text = RNGscript.LegendaryOres[i].OrePrice.ToString();
                Storage[i].text = RNGscript.LegendaryOres[i].StorageAmount.ToString();
                Description[i].text = RNGscript.LegendaryOres[i].description;
                InputFields[i].enabled = true;
            }
            for (int i = RNGscript.LegendaryOres.Count; i < Name.Count; i++)
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
        else if (InventoryStatus == 5)
        {
            for (int i = 0; i < RNGscript.MythicOres.Count; i++)
            {
                Name[i].text = RNGscript.MythicOres[i].name;
                Color[i].color = RNGscript.MythicOres[i].Color;
                Image[i].sprite = RNGscript.MythicOres[i].OrePicture;
                Price[i].text = RNGscript.MythicOres[i].OrePrice.ToString();
                Storage[i].text = RNGscript.MythicOres[i].StorageAmount.ToString();
                Description[i].text = RNGscript.MythicOres[i].description;
                InventoryStatus = -1;
                InputFields[i].enabled = true;
            }
            for (int i = RNGscript.MythicOres.Count; i < Name.Count; i++)
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
        InventoryStatus++;
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

    public void UpdateInventory()
    {
        if (InventoryStatus == 1)
        {
            for (int i = 0; i < RNGscript.CommonOres.Count; i++)
            {
                Storage[i].text = RNGscript.CommonOres[i].StorageAmount.ToString();
            }
        }
        else if (InventoryStatus == 2)
        {
            for (int i = 0; i < RNGscript.UncommonOres.Count; i++)
            {
                Storage[i].text = RNGscript.UncommonOres[i].StorageAmount.ToString();
            }
        }
        else if (InventoryStatus == 3)
        {
            for (int i = 0; i < RNGscript.RareOres.Count; i++)
            {
                Storage[i].text = RNGscript.RareOres[i].StorageAmount.ToString();
            }
        }
        else if (InventoryStatus == 4)
        {
            for (int i = 0; i < RNGscript.EpicOres.Count; i++)
            {
                Storage[i].text = RNGscript.EpicOres[i].StorageAmount.ToString();
            }
        }
        else if (InventoryStatus == 5)
        {
            for (int i = 0; i < RNGscript.LegendaryOres.Count; i++)
            {
                Storage[i].text = RNGscript.LegendaryOres[i].StorageAmount.ToString();
            }
        }
        else if (InventoryStatus == 0)
        {
            for (int i = 0; i < RNGscript.MythicOres.Count; i++)
            {
                Storage[i].text = RNGscript.MythicOres[i].StorageAmount.ToString();
            }
        }
    }

    public void SellSelectedOres()
    {
        if (InventoryStatus == 1)
        {
            for (int j = 0; j < InputFields.Count; j++) // Loop through SellOres
            {
                int sellAmount;
                if (!int.TryParse(InputFields[j].text, out sellAmount) || sellAmount <= 0)
                {
                    continue; // Skip invalid inputs
                }

                if (sellAmount > RNGscript.CommonOres[j].StorageAmount)
                {
                    InputFields[j].textComponent.color = new Vector4(1, 0, 0, 1);
                }
                else
                {
                    RNGscript.CommonOres[j].StorageAmount -= sellAmount;
                    InputFields[j].textComponent.color = new Vector4(0, 0, 0, 1);
                    InputFields[j].text = "";
                    MoneyLogic.Money += RNGscript.CommonOres[j].OrePrice * RNGscript.MoneyMultiplier * sellAmount;

                }
            }
        }
        if (InventoryStatus == 2)
        {
            for (int j = 0; j < InputFields.Count; j++) // Loop through SellOres
            {
                int sellAmount;
                if (!int.TryParse(InputFields[j].text, out sellAmount) || sellAmount <= 0)
                {
                    continue; // Skip invalid inputs
                }

                if (sellAmount > RNGscript.UncommonOres[j].StorageAmount)
                {
                    InputFields[j].textComponent.color = new Vector4(1, 0, 0, 1);
                }
                else
                {
                    RNGscript.UncommonOres[j].StorageAmount -= sellAmount;
                    InputFields[j].textComponent.color = new Vector4(0, 0, 0, 1);
                    InputFields[j].text = "";
                    MoneyLogic.Money += RNGscript.UncommonOres[j].OrePrice * RNGscript.MoneyMultiplier * sellAmount;
                }
            }
        }
        if (InventoryStatus == 3)
        {
            for (int j = 0; j < InputFields.Count; j++) // Loop through SellOres
            {
                int sellAmount;
                if (!int.TryParse(InputFields[j].text, out sellAmount) || sellAmount <= 0)
                {
                    continue; // Skip invalid inputs
                }

                if (sellAmount > RNGscript.RareOres[j].StorageAmount)
                {
                    InputFields[j].textComponent.color = new Vector4(1, 0, 0, 1);
                }
                else
                {
                    RNGscript.RareOres[j].StorageAmount -= sellAmount;
                    InputFields[j].textComponent.color = new Vector4(0, 0, 0, 1);
                    InputFields[j].text = "";
                    MoneyLogic.Money += RNGscript.RareOres[j].OrePrice * RNGscript.MoneyMultiplier * sellAmount;

                }
            }
        }
        if (InventoryStatus == 4)
        {
            for (int j = 0; j < InputFields.Count; j++) // Loop through SellOres
            {
                int sellAmount;
                if (!int.TryParse(InputFields[j].text, out sellAmount) || sellAmount <= 0)
                {
                    continue; // Skip invalid inputs
                }

                if (sellAmount > RNGscript.EpicOres[j].StorageAmount)
                {
                    InputFields[j].textComponent.color = new Vector4(1, 0, 0, 1);
                }
                else
                {
                    RNGscript.EpicOres[j].StorageAmount -= sellAmount;
                    InputFields[j].textComponent.color = new Vector4(0, 0, 0, 1);
                    InputFields[j].text = "";
                    MoneyLogic.Money += RNGscript.EpicOres[j].OrePrice * RNGscript.MoneyMultiplier * sellAmount;
                }
            }
        }
        if (InventoryStatus == 5)
        {
            for (int j = 0; j < InputFields.Count; j++) // Loop through SellOres
            {
                int sellAmount;
                if (!int.TryParse(InputFields[j].text, out sellAmount) || sellAmount <= 0)
                {
                    continue; // Skip invalid inputs
                }

                if (sellAmount > RNGscript.LegendaryOres[j].StorageAmount)
                {
                    InputFields[j].textComponent.color = new Vector4(1, 0, 0, 1);
                }
                else
                {
                    RNGscript.LegendaryOres[j].StorageAmount -= sellAmount;
                    InputFields[j].textComponent.color = new Vector4(0, 0, 0, 1);
                    InputFields[j].text = "";
                    MoneyLogic.Money += RNGscript.LegendaryOres[j].OrePrice * RNGscript.MoneyMultiplier * sellAmount;
                }
            }
        }
        if (InventoryStatus == 0)
        {
            for (int j = 0; j < InputFields.Count; j++) // Loop through SellOres
            {
                int sellAmount;
                if (!int.TryParse(InputFields[j].text, out sellAmount) || sellAmount <= 0)
                {
                    continue; // Skip invalid inputs
                }

                if (sellAmount > RNGscript.MythicOres[j].StorageAmount)
                {
                    InputFields[j].textComponent.color = new Vector4(1, 0, 0, 1);
                }
                else
                {
                    RNGscript.MythicOres[j].StorageAmount -= sellAmount;
                    InputFields[j].textComponent.color = new Vector4(0, 0, 0, 1);
                    InputFields[j].text = "";
                    MoneyLogic.Money += RNGscript.MythicOres[j].OrePrice * RNGscript.MoneyMultiplier * sellAmount;
                }
            }
        }
    }

}
