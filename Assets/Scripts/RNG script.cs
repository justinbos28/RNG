using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RNGscript : MonoBehaviour
{
    public List<OreClass> allOres = new List<OreClass>();
    public List<OreClass> level1Ore = new List<OreClass>();
    public List<OreClass> level2Ore = new List<OreClass>();
    public List<OreClass> level3Ore = new List<OreClass>();
    public List<OreClass> level4Ore = new List<OreClass>();
    public List<OreClass> level5Ore = new List<OreClass>();
    public List<OreClass> level6Ore = new List<OreClass>();
    public List<OreClass> level7Ore = new List<OreClass>();
    public List<OreClass> level8Ore = new List<OreClass>();
    public List<OreClass> level9Ore = new List<OreClass>();
    public List<OreClass> level10Ore = new List<OreClass>();
    public List<OreClass> level11Ore = new List<OreClass>();
    public List<OreClass> level12Ore = new List<OreClass>();
    public List<OreClass> playerHand = new List<OreClass>();
    public int cardLimit = 1;

    public float timer = 0;
    public float timer2 = 0;
    public float RollSpeed = 0.5f;
    public int StartTimer;
    public int RollStatus;
    public int RollSkips = 4;
    public int AutoTimer = 1;
    public bool Timer2Active = false;
    public bool AutoRollTimer = false;

    public Text RollingText;
    public Text CurrentMoney;
    public MoneyLogic MoneyLogic;
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
            if (card.rarity == 1)
            {
                card.rarityTitle = "Common";
                card.rarityEffectColor = Color.white;
                level1Ore.Add(card);
            }
            else if (card.rarity == 2)
            {
                card.rarityTitle = "Uncommon";
                card.rarityEffectColor = Color.green;
                level2Ore.Add(card);
            }
            else if (card.rarity == 3)
            {
                card.rarityTitle = "Rare";
                card.rarityEffectColor = Color.cyan;
                level3Ore.Add(card);
            }
            else if (card.rarity == 4)
            {
                card.rarityTitle = "Epic";
                card.rarityEffectColor = Color.magenta;
                level4Ore.Add(card);
            }
            else if (card.rarity == 5)
            {
                card.rarityTitle = "Legendary";
                card.rarityEffectColor = Color.yellow;
                level5Ore.Add(card);
            }
            else if (card.rarity == 6)
            {
                card.rarityTitle = "Mythical";
                card.rarityEffectColor = new Vector4(1f, 0.5f, 0f);
                level6Ore.Add(card);
            }
            else if (card.rarity == 7)
            {
                card.rarityTitle = "Godly";
                card.rarityEffectColor = Color.red;
                level7Ore.Add(card);
            }
            else if (card.rarity == 8)
            {
                card.rarityTitle = "Divine";
                card.rarityEffectColor = Color.black;
                level8Ore.Add(card);
            }
            else if (card.rarity == 9)
            {
                card.rarityTitle = "Unreal";
                card.rarityEffectColor = new Vector4(1f, 0.5f, 0.1f ,0f);
                level9Ore.Add(card);
            }
            else if (card.rarity == 10)
            {
                card.rarityTitle = "Ancient";
                card.rarityEffectColor = new Vector4(0.6f, 0.45f, 0.08f, 0f);
                level10Ore.Add(card);
            }
            else if (card.rarity == 11)
            {
                card.rarityTitle = "IDon'tKnowAnymore";
                card.rarityEffectColor = new Vector4(0.8f, 0, 1, 0);
                level11Ore.Add(card);
            }
            else if (card.rarity == 12)
            {
                card.rarityTitle = "YouJustGotLucky";
                card.rarityEffectColor = new Vector4(0, 1, 1, 0);
                level12Ore.Add(card);
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
            bool cardRarityChance12 = CalulateRNGPercent(0.1f);
            bool cardRarityChance11 = CalulateRNGPercent(0.13f);
            bool cardRarityChance10 = CalulateRNGPercent(0.25f);
            bool cardRarityChance9 = CalulateRNGPercent(0.4f);
            bool cardRarityChance8 = CalulateRNGPercent(0.625f);
            bool cardRarityChance7 = CalulateRNGPercent(0.8f);
            bool cardRarityChance6 = CalulateRNGPercent(1);
            bool cardRarityChance5 = CalulateRNGPercent(10);
            bool cardRarityChance4 = CalulateRNGPercent(17);
            bool cardRarityChance3 = CalulateRNGPercent(25);
            bool cardRarityChance2 = CalulateRNGPercent(50);
            if (cardRarityChance12)
            {
                if (level12Ore.Count == 1)
                {
                    hand.Add(level12Ore[0].OreID);
                }
                else
                {
                    hand.Add(level12Ore[Random.Range(1, level12Ore.Count)].OreID);
                }
            }
            else if (cardRarityChance11)
            {
                if (level11Ore.Count == 1)
                {
                    hand.Add(level11Ore[0].OreID);
                }
                else
                {
                    hand.Add(level11Ore[Random.Range(1, level11Ore.Count)].OreID);
                }
            }
            else if (cardRarityChance10)
            {
                if (level10Ore.Count == 1)
                {
                    hand.Add(level10Ore[0].OreID);
                }
                else
                {
                    hand.Add(level10Ore[Random.Range(1, level10Ore.Count)].OreID);
                }
            }
            else if (cardRarityChance9)
            {
                if (level9Ore.Count == 1)
                {
                    hand.Add(level9Ore[0].OreID);
                }
                else
                {
                    hand.Add(level9Ore[Random.Range(1, level9Ore.Count)].OreID);
                }
            }
            else if (cardRarityChance8)
            {
                if (level8Ore.Count == 1)
                {
                    hand.Add(level8Ore[0].OreID);
                }
                else
                {
                    hand.Add(level8Ore[Random.Range(1, level8Ore.Count)].OreID);
                }
            }
            else if (cardRarityChance7)
            {
                if (level7Ore.Count == 1)
                {
                    hand.Add(level7Ore[0].OreID);
                }
                else
                {
                    hand.Add(level7Ore[Random.Range(1, level7Ore.Count)].OreID);
                }
            }
            else if (cardRarityChance6)
            {
                if (level6Ore.Count == 1)
                {
                    hand.Add(level6Ore[0].OreID);
                }
                else
                {
                    hand.Add(level6Ore[Random.Range(1, level6Ore.Count)].OreID);
                }
            }
            else if (cardRarityChance5)
            {
                if (level5Ore.Count == 1)
                {
                    hand.Add(level5Ore[0].OreID);
                }
                else
                {
                    hand.Add(level5Ore[Random.Range(1, level5Ore.Count)].OreID);
                }
            }
            else if (cardRarityChance4)
            {
                if (level4Ore.Count == 1)
                {
                    hand.Add(level4Ore[0].OreID);
                }
                else
                {
                    hand.Add(level4Ore[Random.Range(1, level4Ore.Count)].OreID);
                }

            }
            else if (cardRarityChance3)
            {
                if (level3Ore.Count == 1)
                {
                    hand.Add(level3Ore[0].OreID);
                }
                else
                {
                    hand.Add(level3Ore[Random.Range(1, level3Ore.Count)].OreID);
                }

            }
            else if (cardRarityChance2)
            {
                if (level2Ore.Count == 1)
                {
                    hand.Add(level2Ore[0].OreID);
                }
                else
                {
                    hand.Add(level2Ore[Random.Range(1, level2Ore.Count)].OreID);
                }
            }
            else
            {
                if (level1Ore.Count == 1)
                {
                    hand.Add(level1Ore[0].OreID);
                }
                else
                {
                    hand.Add(level1Ore[Random.Range(1, level1Ore.Count)].OreID);
                }
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
                StaticVariables.cash = StaticVariables.cash + playerHand[i].OrePrice;
                if (playerHand[i].OreID >= 6)
                {
                    AutoTimer = 1;
                    RollButton.SetActive(true);
                    MoneyLogic.AutoRoll = false;
                }
            }
            CurrentMoney.text = StaticVariables.cash + "$";
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
            RollButton6.transform.position = new Vector3(0, -1.5f , 0);
            RollButton1.transform.position = new Vector3(0, 1.5f, 0);
            RarityEffectObject.transform.position = RollButton1.transform.position;
            RarityEffectObject6.transform.position = RollButton6.transform.position;
        }
        else if (cardLimit == 5)
        {
            RollButton5.SetActive(true);
            RarityEffectSprite9.enabled = true;
            RarityEffectSprite10.enabled = true;
            RollButton1.transform.position = new Vector3(0, 0, 0);
            RollButton2.transform.position = new Vector3(5, 1.5f, 0);
            RollButton3.transform.position = new Vector3(-5, 1.5f, 0);
            RollButton4.transform.position = new Vector3(-5, -1.5f, 0);
            RollButton5.transform.position = new Vector3(5, -1.5f, 0);
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
            RollButton2.transform.position = new Vector3(3, 1.5f, 0);
            RollButton1.transform.position = new Vector3(3, -1.5f, 0);
            RollButton3.transform.position = new Vector3(-3, 1.5f, 0);
            RollButton4.transform.position = new Vector3(-3, -1.5f, 0);
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
            RollButton2.transform.position = new Vector3(5, 0, 0);
            RollButton1.transform.position = Vector3.zero;
            RollButton3.transform.position = new Vector3(-5, 0, 0);
            RarityEffectObject.transform.position = RollButton1.transform.position;
            RarityEffectObject2.transform.position = RollButton2.transform.position;
        }
        else if (cardLimit == 2)
        {
            RollButton2.SetActive(true);
            RarityEffectSprite3.enabled = true;
            RarityEffectSprite4.enabled = true;
            RollButton1.transform.position = new Vector3(-3, 0, 0);
            RollButton2.transform.position = new Vector3(3, 0, 0);
            RarityEffectObject.transform.position = RollButton1.transform.position;
            RarityEffectObject2.transform.position = RollButton2.transform.position;
        }
    }
    // button connected first roll
    public void RollDice()
    {
        timer = 0;
        StartTimer = 1;
        RollForHand();
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