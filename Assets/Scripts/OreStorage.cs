using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreStorage : MonoBehaviour
{
    public int MaxCommonOres = 500;
    public int MaxUncommonOres = 250;
    public int MaxRareOres = 125;
    public int MaxEpicOres = 75;
    public int MaxLegendaryOres = 25;
    public int MaxMythicalOres = 1;

    public RNGscript RNGscript;
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
