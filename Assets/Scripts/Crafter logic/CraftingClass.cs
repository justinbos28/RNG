using System.Collections.Generic;
using System.Drawing;
using UnityEngine.UI;

[System.Serializable]
public class CraftingClass
{
    public string Name;
    public int Price;
    public int StorageAmount;
    public Image Image;
    public int Xp;
    public Color color;
    public int ID;
}

[System.Serializable]
public class ButtonClass
{
    public Button button;
    public int ID;
}

[System.Serializable]
public class Requirements 
{
    public int MaterialAmount;
    public string MaterialName;
}
[System.Serializable]
public class drills
{
    public int ID;
    public float Timer;
    public int MaxTime;
    public int Upgrade;
    public bool IsActive;
    public bool AutoSell;
    public bool IsUnlocked;
    public Text Text;
    public Button UpgradeButton;
    public Button AutoSellButton;
    public Button Activate;
}