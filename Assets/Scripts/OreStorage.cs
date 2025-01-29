using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OreStorage : MonoBehaviour
{
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

    public void UpdateInventory()
    {
        for (int i = 0; i < RNGscript.playerHand.Count; i++)
        {
            Name[i].text = RNGscript.playerHand[i].name;
            Color[i].color = RNGscript.playerHand[i].rarityEffectColor;
            Image[i].sprite = RNGscript.playerHand[i].OrePicture;
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
