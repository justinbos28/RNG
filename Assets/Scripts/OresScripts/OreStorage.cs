using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreStorage : MonoBehaviour, IDataPersistence
{
    [Header("storage")]
    public int InventoryStatus = 0;
    public int InventoryOres = 5;

    public int MaxCommonOres = 500;
    public int MaxUncommonOres = 250;
    public int MaxRareOres = 125;
    public int MaxEpicOres = 75;
    public int MaxLegendaryOres = 40;
    public int MaxMythicOres = 20;
    public int MaxExoticOres = 10;
    public int MaxDivineOres = 5;

    [Header("lists")]
    public List<Text> Storage = new List<Text>();
    public List<Text> Description = new List<Text>();
    public List<Image> Image = new List<Image>();
    public List<Image> Color = new List<Image>();
    public List<Text> Name = new List<Text>();
    public List<Text> Price = new List<Text>();

    public List<OreClass> lastOre = new List<OreClass>();

    [Header("selling")]
    public List<InputField> InputFields = new List<InputField>();
    public RNGscript RNGscript;
    public MoneyLogic MoneyLogic;
    public XPScript XPScript;

    public void LoadData(GameData data)
    {
        this.MaxCommonOres = data.MaxCommonOres;
        this.MaxUncommonOres = data.MaxUncommonOres;
        this.MaxRareOres = data.MaxRareOres;
        this.MaxEpicOres = data.MaxEpicOres;
        this.MaxLegendaryOres = data.MaxLegendaryOres;
        this.MaxMythicOres = data.MaxMythicOres;
        this.MaxExoticOres = data.MaxExoticOres;
        this.MaxDivineOres = data.MaxDivineOres;
    }
    public void SaveData(ref GameData data)
    {
        data.MaxCommonOres = this.MaxCommonOres;
        data.MaxUncommonOres = this.MaxUncommonOres;
        data.MaxRareOres = this.MaxRareOres;
        data.MaxEpicOres = this.MaxEpicOres;
        data.MaxLegendaryOres = this.MaxLegendaryOres;
        data.MaxMythicOres = this.MaxMythicOres;
        data.MaxExoticOres = this.MaxExoticOres;
        data.MaxDivineOres = this.MaxDivineOres;
    }

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

    private void Update()
    {
        if(XPScript.Rebirth == 0)
        {
            lastOre = RNGscript.MythicOres;
        }
        else if (XPScript.Rebirth == 1)
        {
            lastOre = RNGscript.ExoticOres;
        }
        else if (XPScript.Rebirth >= 2)
        {
            lastOre = RNGscript.DivineOres;
        }
    }

    private List<OreClass> GetCurrentInventory()
    {

        switch (InventoryStatus)
        {
            case 0: return lastOre;
            case 1: return RNGscript.CommonOres;
            case 2: return RNGscript.UncommonOres;
            case 3: return RNGscript.RareOres;
            case 4: return RNGscript.EpicOres;
            case 5: return RNGscript.LegendaryOres;
            case 6: return RNGscript.MythicOres;
            case 7: return RNGscript.ExoticOres;
            case 8: return RNGscript.DivineOres;
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
            Name[i].text = ores[i].Name;
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
            Name[i].text = RNGscript.CommonOres[i].Name;
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
