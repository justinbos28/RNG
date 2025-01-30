using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreStorage : MonoBehaviour
{
    public int InventoryStatus = 0;

    public int MaxCommonOres = 500;
    public int MaxUncommonOres = 250;
    public int MaxRareOres = 125;
    public int MaxEpicOres = 75;
    public int MaxLegendaryOres = 25;
    public int MaxMythicalOres = 1;

    public List<Text> Storage = new List<Text>();
    public List<Text> Description = new List<Text>();
    public List<Image> Image = new List<Image>();
    public List<Image> Color = new List<Image>();
    public List<Text> Name = new List<Text>();
    public List<Text> Price = new List<Text>();

    public RNGscript RNGscript;

    public void SwitchInventory()
    {

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
            }

            for (int i = RNGscript.CommonOres.Count; i < Name.Count; i++)
            {
                Name[i].text = "Empty";
                Color[i].color = RNGscript.CommonOres[i].Color;
                Image[i].sprite = null;
                Price[i].text = "0";
                Storage[i].text = "0";
                Description[i].text = "Empty";
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
            }
            for (int i = RNGscript.UncommonOres.Count; i < Name.Count; i++)
            {
                Name[i].text = "Empty";
                Color[i].color = RNGscript.CommonOres[i].Color;
                Image[i].sprite = null;
                Price[i].text = "0";
                Storage[i].text = "0";
                Description[i].text = "Empty";
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
            }
            for (int i = RNGscript.RareOres.Count; i < Name.Count; i++)
            {
                Name[i].text = "Empty";
                Color[i].color = RNGscript.CommonOres[i].Color;
                Image[i].sprite = null;
                Price[i].text = "0";
                Storage[i].text = "0";
                Description[i].text = "Empty";
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
            }
            for (int i = RNGscript.EpicOres.Count; i < Name.Count; i++)
            {
                Name[i].text = "Empty";
                Color[i].color = RNGscript.CommonOres[i].Color;
                Image[i].sprite = null;
                Price[i].text = "0";
                Storage[i].text = "0";
                Description[i].text = "Empty";
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
            }
            for (int i = RNGscript.LegendaryOres.Count; i < Name.Count; i++)
            {
                Name[i].text = "Empty";
                Color[i].color = RNGscript.CommonOres[i].Color;
                Image[i].sprite = null;
                Price[i].text = "0";
                Storage[i].text = "0";
                Description[i].text = "Empty";
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
            }
            for (int i = RNGscript.MythicOres.Count; i < Name.Count; i++)
            {
                Name[i].text = "Empty";
                Color[i].color = RNGscript.CommonOres[i].Color;
                Image[i].sprite = null;
                Price[i].text = "0";
                Storage[i].text = "0";
                Description[i].text = "Empty";
            }
        }
        InventoryStatus++;
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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (StaticVariables.ore1 >= MaxCommonOres)
        //{
        //    StaticVariables.ore1 = MaxCommonOres;

        //}
    }
}
