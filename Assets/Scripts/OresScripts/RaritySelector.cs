using UnityEngine;

public class RaritySelector : MonoBehaviour
{
    [SerializeField] private int rarity;
    public void GetButton()
    {
        OreStorage oreStorage = FindObjectOfType<OreStorage>();
        IndexManager indexManager = FindObjectOfType<IndexManager>();
        indexManager.IndexCount = rarity;
        indexManager.SwitchIndex();
        oreStorage.CurrentRarity = rarity;
        oreStorage.SelectRarity();
    }
}
