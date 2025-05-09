using System.Collections.Generic;
using UnityEngine;
public class SavedOresCount : MonoBehaviour, IDataPersistence
{
    public List<OresCountList> OresCount = new List<OresCountList>();
    public RNGscript RNGscript;
    public OreStorage OreStorage;

    public void LoadData(GameData data)
    {
        // if the data is smaller then the allOres list, add the difference to the data list
        if (data.OresCount.Count != this.OresCount.Count)
        {
            Debug.Log("Data is smaller than the allOres list or missing");
            int difference = this.OresCount.Count - data.OresCount.Count;
            int dataOresCount = data.OresCount.Count;

            for (int i = 0; i < difference; i++)
            {
                int currentOres = dataOresCount + i;

                data.OresCount = this.OresCount;
                data.OresCount[currentOres].name = this.RNGscript.allOres[currentOres].Name;
                data.OresCount[currentOres].count = 0;
            }
        }

        // if the data is null or empty, create a new list with the same length as the allOres list
        if (data.OresCount == null || data.OresCount.Count == 0)
        {
            Debug.Log("Does this even get called ever?");
            OresCount = new List<OresCountList>(RNGscript.allOres.Count);
        }

        // adds the data to the OresCount list
        this.OresCount = new List<OresCountList>(data.OresCount);

        // sets the storage amount of the ores to the amount in the OresCount list
        for (int i = 0; i < RNGscript.allOres.Count; i++)
        {
            RNGscript.allOres[i].StorageAmount = OresCount[i].count;
        }
    }
    public void UpdateOresCountList()
    {
        Debug.Log("updateOresCountList in SavedOresCount.cs");
    }

    public void SaveData(ref GameData data)
    {
        for (int i = 0; i < RNGscript.allOres.Count; i++)
        {
            OresCount[i].count = RNGscript.allOres[i].StorageAmount;
        }

        data.OresCount = new List<OresCountList>(OresCount);
    }

    void LateUpdate()
    {
        for (int i = 0; i < RNGscript.allOres.Count; i++)
        {
            OresCount[i].count = RNGscript.allOres[i].StorageAmount;
        }
    }
}

[System.Serializable]
public class OresCountList
{
    public string name;
    public int count;
}
