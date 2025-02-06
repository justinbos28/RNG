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
    public List<OreClass> UnrealOres = new List<OreClass>();

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

    // Calulates the chance of something happening by percent simple use a chance from 1 to 10 else chance is 1 to 100
    // <param name="percent"></param>
    // <param name="simple"></param>

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
        foreach (OreClass card in allOres)
        {
            switch (card.rarity)
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
                    UncommonOres.Add(card);
                    break;
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                    RareOres.Add(card);
                    break;
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                    EpicOres.Add(card);
                    break;
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                    LegendaryOres.Add(card);
                    break;
                case 28:
                    MythicOres.Add(card);
                    break;
                //case 29:
                //    UnrealOres.Add(card);
                //    break;
            }
        }
    }

    // roll for a new hand
    public void RollForHand()
    {
        playerHand.Clear();
        List<int> hand = new List<int>();
        for (int i = 0; i < cardLimit; i++)
        {
            // unreal ores
            //bool cardRarityChance29 = CalulateRNGPercent(0.00001f * LuckPercentage * LuckMultiplier);
            // mythic ores
            bool cardRarityChance28 = CalulateRNGPercent(0.0001f * LuckPercentage * LuckMultiplier);
            // legendary ores
            bool cardRarityChance27 = CalulateRNGPercent(0.00011f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance26 = CalulateRNGPercent(0.00015f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance25 = CalulateRNGPercent(0.00022f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance24 = CalulateRNGPercent(0.0004f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance23 = CalulateRNGPercent(0.00067f * LuckPercentage * LuckMultiplier);
            // epic ores
            bool cardRarityChance22 = CalulateRNGPercent(0.0011f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance21 = CalulateRNGPercent(0.0015f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance20 = CalulateRNGPercent(0.0022f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance19 = CalulateRNGPercent(0.0035f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance18 = CalulateRNGPercent(0.0067f * LuckPercentage * LuckMultiplier);
            // rare ores
            bool cardRarityChance17 = CalulateRNGPercent(0.01f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance16 = CalulateRNGPercent(0.013f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance15 = CalulateRNGPercent(0.018f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance14 = CalulateRNGPercent(0.03f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance13 = CalulateRNGPercent(0.074f * LuckPercentage * LuckMultiplier);
            // uncommon ores
            bool cardRarityChance12 = CalulateRNGPercent(0.121f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance11 = CalulateRNGPercent(0.15f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance10 = CalulateRNGPercent(0.25f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance9 = CalulateRNGPercent(0.4f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance8 = CalulateRNGPercent(0.9f * LuckPercentage * LuckMultiplier);
            // common ores
            bool cardRarityChance7 = CalulateRNGPercent(1.4f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance6 = CalulateRNGPercent(1.8f * LuckPercentage * LuckMultiplier);
            bool cardRarityChance5 = CalulateRNGPercent(4.8f / LuckPercentage / LuckMultiplier);
            bool cardRarityChance4 = CalulateRNGPercent(9.1f / LuckPercentage / LuckMultiplier);
            bool cardRarityChance3 = CalulateRNGPercent(14.3f / LuckPercentage / LuckMultiplier);
            bool cardRarityChance2 = CalulateRNGPercent(20 / LuckPercentage / LuckMultiplier);
            bool cardRarityChance1 = CalulateRNGPercent(50 / LuckPercentage / LuckMultiplier);

            void AddOreToHand(List<OreClass> oreList, List<int> hand)
            {
                if (oreList.Count == 0) // checks if theres 0 ores in the list
                {
                    Debug.LogError("No ores in the list");
                }
                else if (oreList.Count == 1) // checks if theres only 1 ore in the list
                {
                    hand.Add(oreList[0].OreID);
                }
                else // if theres more than 1 ore in the list randomly selects one
                {
                    hand.Add(oreList[Random.Range(0, oreList.Count)].OreID);
                }
            }
            //if (cardRarityChance29)
            //{
            //    AddOreToHand(UnrealOres, hand);
            //}
            if (cardRarityChance28)
            {
                AddOreToHand(MythicOres, hand);
            }
            else if (cardRarityChance27)
            {
                AddOreToHand(LegendaryOres, hand);
            }
            else if (cardRarityChance26)
            {
                AddOreToHand(LegendaryOres, hand);
            }
            else if (cardRarityChance25)
            {
                AddOreToHand(LegendaryOres, hand);
            }
            else if (cardRarityChance24)
            {
                AddOreToHand(LegendaryOres, hand);
            }
            else if (cardRarityChance23)
            {
                AddOreToHand(LegendaryOres, hand);
            }
            else if (cardRarityChance22)
            {
                AddOreToHand(EpicOres, hand);
            }
            else if (cardRarityChance21)
            {
                AddOreToHand(EpicOres, hand);
            }
            else if (cardRarityChance20)
            {
                AddOreToHand(EpicOres, hand);
            }
            else if (cardRarityChance19)
            {
                AddOreToHand(EpicOres, hand);
            }
            else if (cardRarityChance18)
            {
                AddOreToHand(EpicOres, hand);
            }
            else if (cardRarityChance17)
            {
                AddOreToHand(RareOres, hand);
            }
            else if (cardRarityChance16)
            {
                AddOreToHand(RareOres, hand);
            }
            else if (cardRarityChance15)
            {
                AddOreToHand(RareOres, hand);
            }
            else if (cardRarityChance14)
            {
                AddOreToHand(RareOres, hand);
            }
            else if (cardRarityChance13)
            {
                AddOreToHand(RareOres, hand);
            }
            else if (cardRarityChance12)
            {
                AddOreToHand(UncommonOres, hand);
            }
            else if (cardRarityChance11)
            {
                AddOreToHand(UncommonOres, hand);
            }
            else if (cardRarityChance10)
            {
                AddOreToHand(UncommonOres, hand);
            }
            else if (cardRarityChance9)
            {
                AddOreToHand(UncommonOres, hand);
            }
            else if (cardRarityChance8)
            {
                AddOreToHand(UncommonOres, hand);
            }
            else if (cardRarityChance7)
            {
                AddOreToHand(CommonOres, hand);
            }
            else if (cardRarityChance6)
            {
                AddOreToHand(CommonOres, hand);
            }
            else if (cardRarityChance5)
            {
                AddOreToHand(CommonOres, hand);
            }
            else if (cardRarityChance4)
            {
                AddOreToHand(CommonOres, hand);
            }
            else if (cardRarityChance3)
            {
                AddOreToHand(CommonOres, hand);
            }
            else if (cardRarityChance2)
            {
                AddOreToHand(CommonOres, hand);
            }
            else if (cardRarityChance1)
            {
                AddOreToHand(CommonOres, hand);
            }
            else
            {
                AddOreToHand(CommonOres, hand);
            }
        }
        foreach (int ID in hand)
        {
            foreach (OreClass c in allOres)
            {
                if (c.OreID == ID)
                {
                    playerHand.Add(c);
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
        for (int i = 0; i < allOres.Count; i++)
        {
            allOres[i].OreID = i + 1;
        }
        AssignRarity();
    }

    public void Update()
    {
        if (MoneyLogic.Money < 1000)
        {
            CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
        }
        else
        {
            CurrentMoney.text = StaticVariables.cash.ToString("F0") + "$";
        }

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
            RollingText.text = "You mined ";
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
                
                if (new int[] { 2, 6, 10, 28, 29 }.Contains(playerHand[i].OreID))
                //if (playerHand[i].OreID == 9 || playerHand[i].OreID == 12 || playerHand[i].OreID == 19 || playerHand[i].OreID == 21 || playerHand[i].OreID == 26)
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
                if (new int[] { 8, 9, 10, 11, 12 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxUncommonOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxUncommonOres;
                    }
                }
                if (new int[] { 13, 14, 15, 16, 17 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxRareOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxRareOres;
                    }
                }
                if (new int[] { 18, 19, 20, 21, 22 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxEpicOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxEpicOres;
                    }
                }
                if (new int[] { 23, 24, 25, 26, 27 }.Contains(playerHand[i].OreID))
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxLegendaryOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxLegendaryOres;
                    }
                }
                if (playerHand[i].OreID == 28)
                {
                    MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                    playerHand[i].StorageAmount = OreStorage.MaxMythicOres;
                }
            }
            OreStorage.UpdateInventory();
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