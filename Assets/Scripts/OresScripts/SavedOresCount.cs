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
        if (data.OresCount == null || data.OresCount.Count == 0)
        {
            data.OresCount = new List<int>(new int[RNGscript.allOres.Count]);
        }

        this.OresCount = new List<int>(data.OresCount);

        for (int i = 0; i < RNGscript.allOres.Count; i++)
        {
            RNGscript.allOres[i].StorageAmount = OresCount[i];
        }
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < RNGscript.allOres.Count; i++)
        {
            OresCount[i] = RNGscript.allOres[i].StorageAmount;
        }

        data.OresCount = new List<int>(OresCount);
    }

    void LateUpdate()
    {
        for (int i = 0; i < RNGscript.allOres.Count; i++)
        {
            OresCount[i] = RNGscript.allOres[i].StorageAmount;
        }
    }
}

