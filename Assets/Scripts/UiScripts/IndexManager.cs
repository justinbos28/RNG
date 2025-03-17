using System.Collections.Generic;
using UnityEngine;

public class IndexManager : MonoBehaviour
{
    public List<IndexClass> IndexList = new List<IndexClass>();
    public RNGscript RNGscript;
    public int IndexCount;
    public int MaxIndexCount = 5;

    public void OpenIndex()
    {
        List<OreClass> ores;
        switch (IndexCount)
        {
            case 0:
                ores = RNGscript.CommonOres;
                break;
            case 1:
                ores = RNGscript.UncommonOres;
                break;
            case 2:
                ores = RNGscript.RareOres;
                break;
            case 3:
                ores = RNGscript.EpicOres;
                break;
            case 4:
                ores = RNGscript.LegendaryOres;
                break;
            case 5:
                ores = RNGscript.MythicOres;
                break;
            case 6:
                ores = RNGscript.ExoticOres;
                break;
            case 7:
                ores = RNGscript.DivineOres;
                break;
            default:
                ores = new List<OreClass>();
                break;
        }

        for (int i = 0; i < IndexList.Count; i++)
        {
            if (i < ores.Count)
            {
                IndexList[i].Name.text = ores[i].Name;
                IndexList[i].Description.text = ores[i].description;
                IndexList[i].Percentage.text = ores[i].Percentage.ToString() + "%";
                IndexList[i].Price.text = "Ore Price = " + ores[i].OrePrice.ToString();
                IndexList[i].Rarity.text = "1 / " + ores[i].chance.ToString();
                IndexList[i].Image.sprite = ores[i].OrePicture;
                IndexList[i].background.color = ores[i].Color;
            }
            else
            {
                IndexList[i].Name.text = "Empty";
                IndexList[i].Description.text = "No Description";
                IndexList[i].Percentage.text = "0%";
                IndexList[i].Price.text = "Ore Price = 0";
                IndexList[i].Rarity.text = "0 / 0";
                IndexList[i].Image.sprite = null;
            }
        }

        if (IndexCount == MaxIndexCount) { IndexCount = -1; }
        IndexCount++;
    }

    public void SetIndexDefault()
    {
        IndexCount = 0;
        for (int i = 0; i < IndexList.Count; i++)
        {
            if (i < RNGscript.allOres.Count)
            {
                IndexList[i].Name.text = RNGscript.CommonOres[i].Name;
                IndexList[i].Description.text = RNGscript.CommonOres[i].description;
                IndexList[i].Percentage.text = RNGscript.CommonOres[i].Percentage.ToString() + "%";
                IndexList[i].Price.text = "Ore Price = " + RNGscript.CommonOres[i].OrePrice.ToString();
                IndexList[i].Rarity.text = "1 / " + RNGscript.CommonOres[i].chance.ToString();
                IndexList[i].Image.sprite = RNGscript.CommonOres[i].OrePicture;
                IndexList[i].background.color = RNGscript.CommonOres[i].Color;
            }
            else
            {
                IndexList[i].Name.text = "Empty";
                IndexList[i].Description.text = "No Description";
                IndexList[i].Percentage.text = "0%";
                IndexList[i].Price.text = "Ore Price = 0";
                IndexList[i].Rarity.text = "0 / 0";
                IndexList[i].Image.sprite = null;
            }
        }
    }
}
