using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SavedOresCount : MonoBehaviour, IDataPersistence
{
    public List<int> OresCount = new List<int>();
    public RNGscript RNGscript;
    public OreStorage OreStorage;

    public void LoadData(GameData data)
    {
        this.OresCount = data.OresCount;

        for (int i = 0; i < RNGscript.allOres.Count; i++)
        {
            RNGscript.allOres[i].StorageAmount = OresCount[i];
        }
    }

    public void SaveData(ref GameData data)
    {
        data.OresCount = this.OresCount;
    }

    void Update()
    {
        for (int i = 0; i < OresCount.Count; i++)
        {
            OresCount[i] = RNGscript.allOres[i].StorageAmount;
        }
    }
}

