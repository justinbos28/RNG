using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DrillUnlocked : MonoBehaviour
{
    public Minerscript Minerscript;
    public RNGscript RNGscript;
    public List<drills> DrillList = new List<drills>();

    public int IndexValue;

    public void Upgrade()
    {
        var requiredMaterials = new Dictionary<string, int>
        {
            { "DrillHead", 1 },
            { "Motor", 5 }
        };

        var requiredMaterials2 = new Dictionary<string, int>
        {
            { "DrillHead", 1 },
            { "Motor", 20 },
            { "Wires", 5 }
        };

        var requiredGems = new Dictionary<string, int>
        {
            { "Titanium", 50 },
            { "Iron", 300 },
            { "Copper", 150 }
        };

        var requiredGems2 = new Dictionary<string, int>
        {
            { "Titanium", 100 },
            { "Diamond", 10 },
            { "Copper", 200 }
        };

        bool hasEnoughMaterials = requiredMaterials.All(material =>
            Minerscript.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));

        bool hasEnoughMaterials2 = requiredMaterials2.All(material =>
            Minerscript.Materials.Any(m => m.Name == material.Key && m.StorageAmount >= material.Value));

        bool hasEnougGems = requiredGems.All(requiredGems =>
            RNGscript.allOres.Any(G => G.Name == requiredGems.Key && G.StorageAmount >= requiredGems.Value));

        bool hasEnougGems2 = requiredGems2.All(requiredGems =>
            RNGscript.allOres.Any(G => G.Name == requiredGems.Key && G.StorageAmount >= requiredGems.Value));
        
        for (int i = 0; i < DrillList.Count; i++)
        {
            int index = i;
            DrillList[i].UpgradeButton.onClick.AddListener(() =>
            {
                IndexValue = index;
            });

            if (hasEnougGems && hasEnoughMaterials)
            {
                foreach (var material in requiredMaterials)
                {
                    var item = Minerscript.Materials.First(m => m.Name == material.Key);
                    item.StorageAmount -= material.Value;
                }
                foreach (var gem in requiredGems)
                {
                    var Ores = RNGscript.allOres.First(G => G.Name == gem.Key);
                    Ores.StorageAmount -= gem.Value;
                }
                DrillList[IndexValue].MaxTime -= 15;
                DrillList[IndexValue].Upgrade += 1;
                Debug.Log("Purchased 1");

            }
            else if (hasEnougGems2 && hasEnoughMaterials2 && DrillList[i].Upgrade == 1)
            {
                foreach (var material in requiredMaterials2)
                {
                    var item = Minerscript.Materials.First(m => m.Name == material.Key);
                    item.StorageAmount -= material.Value;
                }
                foreach (var gem in requiredGems2)
                {
                    var Ores = RNGscript.allOres.First(G => G.Name == gem.Key);
                    Ores.StorageAmount -= gem.Value;
                }
                DrillList[IndexValue].MaxTime -= 15;
                DrillList[IndexValue].Upgrade += 1;
                Debug.Log("Purchased 2");
            }
            else
            {
                Debug.Log("Not enough materials");
            }
        }

    }

    public void Update()
    {
        for (int i = 0; i < DrillList.Count; i++)
        {
            if (DrillList[i].IsActive)
            {
                DrillList[i].Timer -= Time.deltaTime;
                DrillList[i].Text.text = DrillList[i].Timer.ToString("F0");
            }

            if (DrillList[i].Timer <= 0)
            {
                DrillList[i].Timer = DrillList[i].MaxTime;
                RNGscript.allOres[i].StorageAmount += 1;
            }
        }
    }
}
