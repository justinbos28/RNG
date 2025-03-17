using System.Collections.Generic;
using UnityEngine;
public class SavedOresCount : MonoBehaviour, IDataPersistence
{
    public List<int> OresCount = new List<int>();
    public RNGscript RNGscript;
    public OreStorage OreStorage;

    public void LoadData(GameData data)
    {
        // if the data is smaller then the allOres list, add the difference to the data list
        if (data.OresCount.Count != this.OresCount.Count)
        {
            int difference = this.OresCount.Count - data.OresCount.Count;
            for (int i = 0; i < difference; i++)
            {
                data.OresCount.Add(0);
            }
        }

        // if the data is null or empty, create a new list with the same length as the allOres list
        if (data.OresCount == null || data.OresCount.Count == 0)
        {
            data.OresCount = new List<int>(new int[RNGscript.allOres.Count]);
        }

        // adds the data to the OresCount list
        this.OresCount = new List<int>(data.OresCount);

        // sets the storage amount of the ores to the amount in the OresCount list
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

