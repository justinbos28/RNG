using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class RNGscript : MonoBehaviour, IDataPersistence
{
    public List<OreClass> allOres = new List<OreClass>();
    public List<OreClass> CommonOres = new List<OreClass>();
    public List<OreClass> UncommonOres = new List<OreClass>();
    public List<OreClass> RareOres = new List<OreClass>();
    public List<OreClass> EpicOres = new List<OreClass>();
    public List<OreClass> LegendaryOres = new List<OreClass>();
    public List<OreClass> MythicOres = new List<OreClass>();
    public List<OreClass> ExoticOres = new List<OreClass>();
    public List<OreClass> DivineOres = new List<OreClass>();

    public List<OreClass> playerHand = new List<OreClass>();
    public int cardLimit = 1;

    public float timer = 0;
    public float timer2 = 0;

    public float RollSpeed = 0.5f;
    public float LuckPercentage = 1;
    public float LuckMultiplier = 1;
    public float MoneyMultiplier = 1;

    public int RollStatus;
    public int RollSkips = 5;

    public int StartTimer;
    public int AutoTimer = 1;
    public bool AutoSell = false;
    public bool Timer2Active = false;
    public bool AutoRollTimer = false;

    public Text RollingText;
    public Text CurrentMoney;

    public MoneyLogic MoneyLogic;
    public XPScript XPScript;
    public OreStorage OreStorage;

    public GameObject RollButton;
    public GameObject RollButton1;
    public GameObject RollButton2;
    public GameObject RollButton3;
    public GameObject RollButton4;
    public GameObject RollButton5;
    public GameObject RollButton6;
    public GameObject RarityEffectObject;
    public GameObject RarityEffectObject2;
    public GameObject RarityEffectObject3;
    public GameObject RarityEffectObject4;
    public GameObject RarityEffectObject5;
    public GameObject RarityEffectObject6;
    
    public SpriteRenderer RarityEffectSprite;
    public SpriteRenderer RarityEffectSprite2;
    public SpriteRenderer RarityEffectSprite3;
    public SpriteRenderer RarityEffectSprite4;
    public SpriteRenderer RarityEffectSprite5;
    public SpriteRenderer RarityEffectSprite6;
    public SpriteRenderer RarityEffectSprite7;
    public SpriteRenderer RarityEffectSprite8;
    public SpriteRenderer RarityEffectSprite9;
    public SpriteRenderer RarityEffectSprite10;
    public SpriteRenderer RarityEffectSprite11;
    public SpriteRenderer RarityEffectSprite12;

    // Displays the cards
    public List<Text> titles = new List<Text>();
    public List<Text> effects = new List<Text>();
    public List<Text> rarity = new List<Text>();
    public List<Image> OrePicture = new List<Image>();
    public List<SpriteRenderer> OreSpriteRenderer = new List<SpriteRenderer>();

    // saving data and getting saved data
    public void LoadData(GameData data)
    {
        this.cardLimit = data.CardLimit;
        this.RollSkips = data.RollSkips;
        this.RollSpeed = data.RollSpeed;
        this.LuckMultiplier = data.LuckMultiplier;
        this.LuckPercentage = data.LuckPercentage;
        this.MoneyMultiplier = data.MoneyMultiplier;
    }
    public void SaveData(ref GameData data)
    {
        data.CardLimit = this.cardLimit;
        data.LuckMultiplier = this.LuckMultiplier;
        data.LuckPercentage = this.LuckPercentage;
        data.MoneyMultiplier = this.MoneyMultiplier;
        data.RollSkips = this.RollSkips;
        data.RollSpeed = this.RollSpeed;
    }
    // end saving and getting saved data


    public static bool CalulateRNGPercent(float percent)
    {
        float temp;
        temp = Random.Range(0f, 100f);
        if (temp > 0 && temp < percent)
        {
            //print(temp);
            return true;
        }
        else
        {
            return false;
        }
    }

    // assign the ored to list by level
    public void AssignRarity()
    {
        for (int i = 0; i < allOres.Count; i++)
        {
            allOres[i].OreID = i + 1;
        }
        foreach (OreClass card in allOres)
        {
            switch (card.OreID)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                    CommonOres.Add(card);
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    UncommonOres.Add(card);
                    break;
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                    RareOres.Add(card);
                    break;
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                    EpicOres.Add(card);
                    break;
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                    LegendaryOres.Add(card);
                    break;
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                    MythicOres.Add(card);
                    break;
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                    ExoticOres.Add(card);
                    break;
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                    DivineOres.Add(card);
                    break;
            }
        }
    }

    // roll for a new hand
    public void RollForHand()
    {
        playerHand.Clear();
        List<int> hand = new List<int>();

        for (int i = 0; i < cardLimit; i++) // Ensure we don't go over cardLimit
        {
            Dictionary<int, float> oreRarityChances = new Dictionary<int, float>
        {
            { 42, 0.00001f }, { 41, 0.000013f }, { 40, 0.000017f }, { 39, 0.000025f }, { 38, 0.000037f }, { 37, 0.000067f }, { 36, 0.0001f }, // mythic ores
            { 35, 0.00011f }, { 34, 0.00015f }, { 33, 0.00018f }, { 32, 0.00022f }, { 31, 0.00028f }, { 30, 0.0004f }, { 29, 0.00067f }, // legendary ores
            { 28, 0.001f }, { 27, 0.0011f }, { 26, 0.0013f }, { 25, 0.0015f }, { 24, 0.0022f }, { 23, 0.0035f }, { 22, 0.0067f }, // epic ores
            { 21, 0.01f }, { 20, 0.013f }, { 19, 0.015f }, { 18, 0.018f }, { 17, 0.022f }, { 16, 0.03f }, { 15, 0.074f }, // rare ores
            { 14, 0.121f }, { 13, 0.15f }, { 12, 0.18f }, { 11, 0.25f }, { 10, 0.4f }, { 9, 0.66f }, { 8, 0.9f }, // uncommon ores
            { 7, 1.4f }, { 6, 1.8f }, { 5, 4.8f }, { 4, 9.1f }, { 3, 14.3f }, { 2, 20f }, { 1, 50f } // common ores
        };

            Dictionary<int, bool> oreRarityResults = oreRarityChances.ToDictionary(
                kvp => kvp.Key,
                kvp => CalulateRNGPercent(kvp.Value * LuckPercentage * LuckMultiplier * XPScript.XPLuckMultiplier)
            );

            void AddOreToHand(List<OreClass> oreList, List<int> hand)
            {
                if (hand.Count >= cardLimit) return; // Prevent exceeding limit

                if (oreList.Count == 0)
                {
                    Debug.LogError("No ores in the list");
                }
                else
                {
                    hand.Add(oreList[Random.Range(0, oreList.Count)].OreID);
                }
            }

            bool oreAdded = false;

            foreach (var oreRarityResult in oreRarityResults)
            {
                if (oreRarityResult.Value)
                {
                    // assign basic ores if the player hasn't rebirthed
                    if (XPScript.Rebirth == 0)
                    {
                        switch (oreRarityResult.Key)
                        {
                            case >= 36 and <= 42:
                                AddOreToHand(MythicOres, hand);
                                break;
                            case >= 29 and <= 35:
                                AddOreToHand(LegendaryOres, hand);
                                break;
                            case >= 22 and <= 28:
                                AddOreToHand(EpicOres, hand);
                                break;
                            case >= 15 and <= 21:
                                AddOreToHand(RareOres, hand);
                                break;
                            case >= 8 and <= 14:
                                AddOreToHand(UncommonOres, hand);
                                break;
                            case >= 1 and <= 7:
                                AddOreToHand(CommonOres, hand);
                                break;
                        }
                    }
                    // assign new ores if the player has rebirthed
                    else if (XPScript.Rebirth == 1)
                    {
                        switch (oreRarityResult.Key)
                        {
                            case >= 36 and <= 42:
                                AddOreToHand(ExoticOres, hand);
                                break;
                            case >= 29 and <= 35:
                                AddOreToHand(MythicOres, hand);
                                break;
                            case >= 22 and <= 28:
                                AddOreToHand(LegendaryOres, hand);
                                break;
                            case >= 15 and <= 21:
                                AddOreToHand(EpicOres, hand);
                                break;
                            case >= 8 and <= 14:
                                AddOreToHand(RareOres, hand);
                                break;
                            case >= 1 and <= 7:
                                AddOreToHand(UncommonOres, hand);
                                break;
                        }
                    }
                    else
                    {
                        switch (oreRarityResult.Key)
                        {
                            case >= 36 and <= 42:
                                AddOreToHand(DivineOres, hand);
                                break;
                            case >= 29 and <= 35:
                                AddOreToHand(ExoticOres, hand);
                                break;
                            case >= 22 and <= 28:
                                AddOreToHand(MythicOres, hand);
                                break;
                            case >= 15 and <= 21:
                                AddOreToHand(LegendaryOres, hand);
                                break;
                            case >= 8 and <= 14:
                                AddOreToHand(EpicOres, hand);
                                break;
                            case >= 1 and <= 7:
                                AddOreToHand(RareOres, hand);
                                break;
                        }
                    }
                    oreAdded = true;
                    break; // Stop after adding one ore

                }
            }

            // If no ore was added, guarantee a common ore for rebirth 0
            if (!oreAdded && XPScript.Rebirth == 0)
            {
                AddOreToHand(CommonOres, hand);
            }
            // If no ore was added, guarantee an uncommon ore for rebirth 1
            else if (!oreAdded && XPScript.Rebirth == 1)
            {
                AddOreToHand(UncommonOres, hand);
            }
            else if (!oreAdded && XPScript.Rebirth >= 2)
            {
                AddOreToHand(RareOres, hand);
            }
        }
        foreach (int ID in hand)
        {
            foreach (OreClass c in allOres)
            {
                if (c.OreID == ID)
                {
                    playerHand.Add(c);
                    break;
                }
            }
        }

        DisplayCard();
    }


    // displays all the ores
    public void DisplayCard()
    {
        for (int i = 0; i < playerHand.Count; i++)
        {
            titles[i].text = playerHand[i].name;
            OreSpriteRenderer[i].color = playerHand[i].rarityEffectColor;
            effects[i].text = "1 in " + playerHand[i].chance;
            rarity[i].text = playerHand[i].rarityTitle;
            OrePicture[i].sprite = playerHand[i].OrePicture;
        }
    }

    public void Start()
    {
        AssignRarity();
    }
    // Simply adds commas to numbers
    private string NumberFormatter(float number)
    {
        if (number < 1000)
        {
            return string.Format("{0:n}", number);
        }
        else
        {
            return string.Format("{0:n0}", number);
        }
    }
    public void Update()
    {
        CurrentMoney.text = NumberFormatter(StaticVariables.cash) + "$";

        // timers for auto rolling, animation
        if (StartTimer == AutoTimer)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            timer = 0;
        }

        if (AutoRollTimer == true)
        {
            timer2 = timer2 + Time.deltaTime;
        }
        // delay after getting a ore
        if (RollStatus >= RollSkips)
        {
            if (timer2 > 1) // bigger
            {
                if (MoneyLogic.AutoRoll == true)
                {
                    RollButton.SetActive(false);
                }
                else
                {
                    RollButton.SetActive(true);
                }
                RollStatus = 0;
                AutoRollTimer = false;
                timer2 = 0;
            }
            else if (timer2 < 1) // smaller
            {
                StartTimer = 0;
                timer = 0;
                AutoRollTimer = true;
            }

        }
        // showing the ore and giving the price
        if (RollStatus == RollSkips)
        {
            RollingText.text = "You found ";
            RarityEffectSprite.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0.5f);
            RarityEffectSprite2.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0.3f);
            RarityEffectSprite3.color = new Color(RarityEffectSprite3.color.r, RarityEffectSprite3.color.g, RarityEffectSprite3.color.b, 0.5f);
            RarityEffectSprite4.color = new Color(RarityEffectSprite3.color.r, RarityEffectSprite3.color.g, RarityEffectSprite3.color.b, 0.3f);
            RarityEffectSprite5.color = new Color(RarityEffectSprite5.color.r, RarityEffectSprite5.color.g, RarityEffectSprite5.color.b, 0.5f);
            RarityEffectSprite6.color = new Color(RarityEffectSprite5.color.r, RarityEffectSprite5.color.g, RarityEffectSprite5.color.b, 0.3f);
            RarityEffectSprite7.color = new Color(RarityEffectSprite7.color.r, RarityEffectSprite7.color.g, RarityEffectSprite7.color.b, 0.5f);
            RarityEffectSprite8.color = new Color(RarityEffectSprite7.color.r, RarityEffectSprite7.color.g, RarityEffectSprite7.color.b, 0.3f);
            RarityEffectSprite9.color = new Color(RarityEffectSprite9.color.r, RarityEffectSprite9.color.g, RarityEffectSprite9.color.b, 0.5f);
            RarityEffectSprite10.color = new Color(RarityEffectSprite9.color.r, RarityEffectSprite9.color.g, RarityEffectSprite9.color.b, 0.3f);
            RarityEffectSprite11.color = new Color(RarityEffectSprite11.color.r, RarityEffectSprite11.color.g, RarityEffectSprite11.color.b, 0.5f);
            RarityEffectSprite12.color = new Color(RarityEffectSprite11.color.r, RarityEffectSprite11.color.g, RarityEffectSprite11.color.b, 0.3f);

            for (int i = 0; i < playerHand.Count; i++)
            {
                // auto sells the ores
                if (AutoSell == true)
                {
                    MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                }
                else
                {
                    playerHand[i].StorageAmount++;
                }
                // changes the color of the ore text if the texture is black
                if (new int[] { 2, 11, 23, 36, 37, 42 }.Contains(playerHand[i].OreID))
                {
                    titles[i].color = Color.white;
                    effects[i].color = Color.white;
                    rarity[i].color = Color.white;
                }
                // auto sells if ores are more then the common ore storage
                if (new int[] { 1, 2, 3, 4, 5, 6, 7 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxCommonOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxCommonOres;
                    }
                }
                // autosell for uncommon ores
                if (new int[] { 8, 9, 10, 11, 12, 13, 14 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxUncommonOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxUncommonOres;
                    }
                }
                // autosell for rare ores
                if (new int[] { 15, 16, 17, 18, 19, 20, 21 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxRareOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxRareOres;
                    }
                }
                // autosell for epic ores
                if (new int[] { 22, 23, 24, 25, 26, 27, 28 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxEpicOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxEpicOres;
                    }
                }
                // autosell for legendary ores
                if (new int[] { 29, 30, 31, 32, 33, 34, 35 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxLegendaryOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxLegendaryOres;
                    }
                }
                // autosell for mythic ores
                if (new int[] { 36, 37, 38, 39, 40, 41, 42 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxMythicOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxMythicOres;
                    }
                }
                // autosell for exotic ores
                if (new int[] { 43, 44, 45, 46, 47, 48, 49 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxExoticOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxExoticOres;
                    }
                }
                // autosell for divine ores
                if (new int[] { 50, 51, 52, 53, 54, 55, 56 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxDivineOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxDivineOres;
                    }
                }
            }
            // updates the inventory and xp
            OreStorage.UpdateInventory();
            XPScript.UpdateXP();
            RollStatus++;
        }
        // rolling the dice (roll animation)
        if (timer > RollSpeed)
        {
            timer = 0;
            RollForHand();
            RollStatus += 1;
            RarityEffectSprite.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0);
            RarityEffectSprite2.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0);
            RarityEffectSprite3.color = new Color(RarityEffectSprite3.color.r, RarityEffectSprite3.color.g, RarityEffectSprite3.color.b, 0);
            RarityEffectSprite4.color = new Color(RarityEffectSprite3.color.r, RarityEffectSprite3.color.g, RarityEffectSprite3.color.b, 0);
            RarityEffectSprite5.color = new Color(RarityEffectSprite5.color.r, RarityEffectSprite5.color.g, RarityEffectSprite5.color.b, 0);
            RarityEffectSprite6.color = new Color(RarityEffectSprite5.color.r, RarityEffectSprite5.color.g, RarityEffectSprite5.color.b, 0);
            RarityEffectSprite7.color = new Color(RarityEffectSprite7.color.r, RarityEffectSprite7.color.g, RarityEffectSprite7.color.b, 0);
            RarityEffectSprite8.color = new Color(RarityEffectSprite7.color.r, RarityEffectSprite7.color.g, RarityEffectSprite7.color.b, 0);
            RarityEffectSprite9.color = new Color(RarityEffectSprite9.color.r, RarityEffectSprite9.color.g, RarityEffectSprite9.color.b, 0);
            RarityEffectSprite10.color = new Color(RarityEffectSprite9.color.r, RarityEffectSprite9.color.g, RarityEffectSprite9.color.b, 0);
            RarityEffectSprite11.color = new Color(RarityEffectSprite11.color.r, RarityEffectSprite11.color.g, RarityEffectSprite11.color.b, 0);
            RarityEffectSprite12.color = new Color(RarityEffectSprite11.color.r, RarityEffectSprite11.color.g, RarityEffectSprite11.color.b, 0);

            RollingText.text = "Mining...";

            for (int i = 0; i < playerHand.Count; i++)
            {
                titles[i].color = Color.black;
                effects[i].color = Color.black;
                rarity[i].color = Color.black;
            }
        }

        // aligns the ores to the corerct spot after buying more roll amount
        if (cardLimit == 6)
        {
            RollButton6.SetActive(true);
            RollButton5.SetActive(true);
            RollButton4.SetActive(true);
            RollButton3.SetActive(true);
            RollButton2.SetActive(true);
            RarityEffectSprite3.enabled = true;
            RarityEffectSprite4.enabled = true;
            RarityEffectSprite5.enabled = true;
            RarityEffectSprite6.enabled = true;
            RarityEffectSprite7.enabled = true;
            RarityEffectSprite8.enabled = true;
            RarityEffectSprite9.enabled = true;
            RarityEffectSprite10.enabled = true;
            RarityEffectSprite11.enabled = true;
            RarityEffectSprite12.enabled = true;
            RollButton6.transform.position = new Vector3(0, -1.5f , 100);
            RollButton1.transform.position = new Vector3(0, 1.5f, 100);
            RollButton2.transform.position = new Vector3(5, 1.5f, 100);
            RollButton3.transform.position = new Vector3(-5, 1.5f, 100);
            RollButton4.transform.position = new Vector3(-5, -1.5f, 100);
            RollButton5.transform.position = new Vector3(5, -1.5f, 100);
        }
        else if (cardLimit == 5)
        {
            RollButton5.SetActive(true);
            RollButton4.SetActive(true);
            RollButton3.SetActive(true);
            RollButton2.SetActive(true);
            RarityEffectSprite3.enabled = true;
            RarityEffectSprite4.enabled = true;
            RarityEffectSprite5.enabled = true;
            RarityEffectSprite6.enabled = true;
            RarityEffectSprite7.enabled = true;
            RarityEffectSprite8.enabled = true;
            RarityEffectSprite9.enabled = true;
            RarityEffectSprite10.enabled = true;
            RollButton1.transform.position = new Vector3(0, 0, 100);
            RollButton2.transform.position = new Vector3(5, 1.5f, 100);
            RollButton3.transform.position = new Vector3(-5, 1.5f, 100);
            RollButton4.transform.position = new Vector3(-5, -1.5f, 100);
            RollButton5.transform.position = new Vector3(5, -1.5f, 100);
        }
        else if (cardLimit == 4)
        {
            RollButton4.SetActive(true);
            RollButton3.SetActive(true);
            RollButton2.SetActive(true);
            RarityEffectSprite3.enabled = true;
            RarityEffectSprite4.enabled = true;
            RarityEffectSprite5.enabled = true;
            RarityEffectSprite6.enabled = true;
            RarityEffectSprite7.enabled = true;
            RarityEffectSprite8.enabled = true;
            RollButton2.transform.position = new Vector3(3, 1.5f, 100);
            RollButton1.transform.position = new Vector3(3, -1.5f, 100);
            RollButton3.transform.position = new Vector3(-3, 1.5f, 100);
            RollButton4.transform.position = new Vector3(-3, -1.5f, 100);
        }
        else if (cardLimit == 3)
        {
            RollButton3.SetActive(true);
            RollButton2.SetActive(true);
            RarityEffectSprite3.enabled = true;
            RarityEffectSprite4.enabled = true;
            RarityEffectSprite5.enabled = true;
            RarityEffectSprite6.enabled = true;
            RollButton2.transform.position = new Vector3(5, 0, 100);
            RollButton1.transform.position = new Vector3(0, 0, 100);
            RollButton3.transform.position = new Vector3(-5, 0, 100);
        }
        else if (cardLimit == 2)
        {
            RollButton2.SetActive(true);
            RarityEffectSprite3.enabled = true;
            RarityEffectSprite4.enabled = true;
            RollButton1.transform.position = new Vector3(-3, 0, 100);
            RollButton2.transform.position = new Vector3(3, 0, 100);
        }
    }
    // button connected first roll
    public void RollDice()
    {
        timer = 0;
        StartTimer = 1;
        //RollForHand();
        RollButton.SetActive(false);
        RollingText.enabled = true;
        RarityEffectSprite.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0);
        RarityEffectSprite2.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0);
        RarityEffectSprite3.color = new Color(RarityEffectSprite3.color.r, RarityEffectSprite3.color.g, RarityEffectSprite3.color.b, 0);
        RarityEffectSprite4.color = new Color(RarityEffectSprite3.color.r, RarityEffectSprite3.color.g, RarityEffectSprite3.color.b, 0);
        RarityEffectSprite5.color = new Color(RarityEffectSprite5.color.r, RarityEffectSprite5.color.g, RarityEffectSprite5.color.b, 0);
        RarityEffectSprite6.color = new Color(RarityEffectSprite5.color.r, RarityEffectSprite5.color.g, RarityEffectSprite5.color.b, 0);
        RarityEffectSprite7.color = new Color(RarityEffectSprite7.color.r, RarityEffectSprite7.color.g, RarityEffectSprite7.color.b, 0);
        RarityEffectSprite8.color = new Color(RarityEffectSprite7.color.r, RarityEffectSprite7.color.g, RarityEffectSprite7.color.b, 0);
        RarityEffectSprite9.color = new Color(RarityEffectSprite9.color.r, RarityEffectSprite9.color.g, RarityEffectSprite9.color.b, 0);
        RarityEffectSprite10.color = new Color(RarityEffectSprite9.color.r, RarityEffectSprite9.color.g, RarityEffectSprite9.color.b, 0);
        RarityEffectSprite11.color = new Color(RarityEffectSprite11.color.r, RarityEffectSprite11.color.g, RarityEffectSprite11.color.b, 0);
        RarityEffectSprite12.color = new Color(RarityEffectSprite11.color.r, RarityEffectSprite11.color.g, RarityEffectSprite11.color.b, 0);

        RollingText.text = "Mining...";
    }
    
}