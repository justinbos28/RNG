using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RNGscript : MonoBehaviour
{
    public List<OreClass> allOres = new List<OreClass>();
    public List<OreClass> CommonOres = new List<OreClass>();
    public List<OreClass> UncommonOres = new List<OreClass>();
    public List<OreClass> RareOres = new List<OreClass>();
    public List<OreClass> EpicOres = new List<OreClass>();
    public List<OreClass> LegendaryOres = new List<OreClass>();
    public List<OreClass> MythicOres = new List<OreClass>();

    public List<OreClass> playerHand = new List<OreClass>();
    public int cardLimit = 1;

    public float timer = 0;
    public float timer2 = 0;
    public float RollSpeed = 0.5f;
    public float LuckPercentage = 1;
    public float LuckMultiplier = 1;
    public int StartTimer;
    public int RollStatus;
    public int RollSkips = 5;
    public int AutoTimer = 1;
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
                    CommonOres.Add(card);
                    break;
                case 2:
                    CommonOres.Add(card);
                    break;
                case 3:
                    CommonOres.Add(card);
                    break;
                case 4:
                    CommonOres.Add(card);
                    break;
                case 5:
                    CommonOres.Add(card);
                    break;
                case 6:
                    CommonOres.Add(card);
                    break;
                case 7:
                    CommonOres.Add(card);
                    break;
                case 8:
                    UncommonOres.Add(card);
                    break;
                case 9:
                    UncommonOres.Add(card);
                    break;
                case 10:
                    UncommonOres.Add(card);
                    break;
                case 11:
                    UncommonOres.Add(card);
                    break;
                case 12:
                    UncommonOres.Add(card);
                    break;
                case 13:
                    RareOres.Add(card);
                    break;
                case 14:
                    RareOres.Add(card);
                    break;
                case 15:
                    RareOres.Add(card);
                    break;
                case 16:
                    RareOres.Add(card);
                    break;
                case 17:
                    RareOres.Add(card);
                    break;
                case 18:
                    EpicOres.Add(card);
                    break;
                case 19:
                    EpicOres.Add(card);
                    break;
                case 20:
                    EpicOres.Add(card);
                    break;
                case 21:
                    EpicOres.Add(card);
                    break;
                case 22:
                    EpicOres.Add(card);
                    break;
                case 23:
                    LegendaryOres.Add(card);
                    break;
                case 24:
                    LegendaryOres.Add(card);
                    break;
                case 25:
                    LegendaryOres.Add(card);
                    break;
                case 26:
                    LegendaryOres.Add(card);
                    break;
                case 27:
                    LegendaryOres.Add(card);
                    break;
                case 28:
                    MythicOres.Add(card);
                    break;
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
            bool cardRarityChance1 = CalulateRNGPercent(50 * LuckPercentage * LuckMultiplier);

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
            rarity[i].text = playerHand[i].rarityTitle + "\n" + playerHand[i].decription;
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
            RollingText.text = "You rolled ";
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
                //foreach (var ore in playerHand)
                //{
                //    switch (ore.OreID)
                //    {
                //        case 1:
                //            StaticVariables.ore1++;
                //            break;
                //        case 2:
                //            StaticVariables.ore2++;
                //            break;
                //        case 3:
                //            StaticVariables.ore3++;
                //            break;
                //        case 4:
                //            StaticVariables.ore4++;
                //            break;
                //        case 5:
                //            StaticVariables.ore5++;
                //            break;
                //        case 6:
                //            StaticVariables.ore6++;
                //            break;
                //        case 7:
                //            StaticVariables.ore7++;
                //            break;
                //        case 8:
                //            StaticVariables.ore8++;
                //            break;
                //        case 9:
                //            StaticVariables.ore9++;
                //            break;
                //        case 10:
                //            StaticVariables.ore10++;
                //            break;
                //        case 11:
                //            StaticVariables.ore11++;
                //            break;
                //        case 12:
                //            StaticVariables.ore12++;
                //            break;
                //        case 13:
                //            StaticVariables.ore13++;
                //            break;
                //        case 14:
                //            StaticVariables.ore14++;
                //            break;
                //        case 15:
                //            StaticVariables.ore15++;
                //            break;
                //        case 16:
                //            StaticVariables.ore16++;
                //            break;
                //        case 17:
                //            StaticVariables.ore17++;
                //            break;
                //        case 18:
                //            StaticVariables.ore18++;
                //            break;
                //        case 19:
                //            StaticVariables.ore19++;
                //            break;
                //        case 20:
                //            StaticVariables.ore20++;
                //            break;
                //        case 21:
                //            StaticVariables.ore21++;
                //            break;
                //        case 22:
                //            StaticVariables.ore22++;
                //            break;
                //        case 23:
                //            StaticVariables.ore23++;
                //            break;
                //        case 24:
                //            StaticVariables.ore24++;
                //            break;
                //        case 25:
                //            StaticVariables.ore25++;
                //            break;
                //        case 26:
                //            StaticVariables.ore26++;
                //            break;
                //        case 27:
                //            StaticVariables.ore27++;
                //            break;
                //        case 28:
                //            StaticVariables.ore28++;
                //            break;
                //    }
                //}
            }
            if (StaticVariables.cash < 1000)
            {
                CurrentMoney.text = StaticVariables.cash.ToString("F2") + "$";
            }
            else
            {
                CurrentMoney.text = StaticVariables.cash.ToString("F0") + "$";
            }
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

            RollingText.text = "Rolling...";
        }

        // aligns the ores to the corerct spot after buying more roll amount
        if (cardLimit == 6)
        {
            RollButton6.SetActive(true);
            RarityEffectSprite11.enabled = true;
            RarityEffectSprite12.enabled = true;
            RollButton6.transform.position = new Vector3(0, -1.5f , 100);
            RollButton1.transform.position = new Vector3(0, 1.5f, 100);
            RarityEffectObject.transform.position = RollButton1.transform.position;
            RarityEffectObject6.transform.position = RollButton6.transform.position;
        }
        else if (cardLimit == 5)
        {
            RollButton5.SetActive(true);
            RarityEffectSprite9.enabled = true;
            RarityEffectSprite10.enabled = true;
            RollButton1.transform.position = new Vector3(0, 0, 100);
            RollButton2.transform.position = new Vector3(5, 1.5f, 100);
            RollButton3.transform.position = new Vector3(-5, 1.5f, 100);
            RollButton4.transform.position = new Vector3(-5, -1.5f, 100);
            RollButton5.transform.position = new Vector3(5, -1.5f, 100);
            RarityEffectObject.transform.position = RollButton1.transform.position;
            RarityEffectObject2.transform.position = RollButton2.transform.position;
            RarityEffectObject3.transform.position = RollButton3.transform.position;
            RarityEffectObject4.transform.position = RollButton4.transform.position;
            RarityEffectObject5.transform.position = RollButton5.transform.position;
        }
        else if (cardLimit == 4)
        {
            RollButton4.SetActive(true);
            RarityEffectSprite7.enabled = true;
            RarityEffectSprite8.enabled = true;
            RollButton2.transform.position = new Vector3(3, 1.5f, 100);
            RollButton1.transform.position = new Vector3(3, -1.5f, 100);
            RollButton3.transform.position = new Vector3(-3, 1.5f, 100);
            RollButton4.transform.position = new Vector3(-3, -1.5f, 100);
            RarityEffectObject.transform.position = RollButton1.transform.position;
            RarityEffectObject2.transform.position = RollButton2.transform.position;
            RarityEffectObject3.transform.position = RollButton3.transform.position;
            RarityEffectObject4.transform.position = RollButton4.transform.position;
        }
        else if (cardLimit == 3)
        {
            RollButton3.SetActive(true);
            RarityEffectSprite5.enabled = true;
            RarityEffectSprite6.enabled = true;
            RollButton2.transform.position = new Vector3(5, 0, 100);
            RollButton1.transform.position = new Vector3(0, 0, 100);
            RollButton3.transform.position = new Vector3(-5, 0, 100);
            RarityEffectObject.transform.position = RollButton1.transform.position;
            RarityEffectObject2.transform.position = RollButton2.transform.position;
        }
        else if (cardLimit == 2)
        {
            RollButton2.SetActive(true);
            RarityEffectSprite3.enabled = true;
            RarityEffectSprite4.enabled = true;
            RollButton1.transform.position = new Vector3(-3, 0, 100);
            RollButton2.transform.position = new Vector3(3, 0, 100);
            RarityEffectObject.transform.position = RollButton1.transform.position;
            RarityEffectObject2.transform.position = RollButton2.transform.position;
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

        RollingText.text = "Rolling...";
    }
    
}