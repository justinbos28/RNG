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
    public List<OreClass> playerHand = new List<OreClass>();
    public int cardLimit = 1;

    public float timer = 0;
    public int StartTimer;
    public int RollStatus;
    public int RollSkips = 4;
    public float RollSpeed = 0.5f;
    public int RollAmount = 1;

    public Button RollButton;
    public Text RollingText;
    public Text CurrentMoney;
    public SpriteRenderer RarityEffectSprite;
    public SpriteRenderer RarityEffectSprite2;

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
        temp = Random.Range(0, 100);
        if (temp > 0 && temp < percent)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Assign the cards to list by level
    public void AssignRarity()
    {
        foreach (OreClass card in allOres)
        {
            if (card.rarity == 1)
            {
                card.rarityTitle = "Common";
                card.chance = 1;
                card.rarityEffectColor = Color.white;
                level1Ore.Add(card);
            }
            else if (card.rarity == 2)
            {
                card.rarityTitle = "Uncommon";
                card.chance = 2;
                card.rarityEffectColor = Color.green;
                level2Ore.Add(card);
            }
            else if (card.rarity == 3)
            {
                card.rarityTitle = "Rare";
                card.chance = 4;
                card.rarityEffectColor = Color.cyan;
                level3Ore.Add(card);
            }
            else if (card.rarity == 4)
            {
                card.rarityTitle = "Epic";
                card.rarityEffectColor = Color.magenta;
                card.chance = 6;
                level4Ore.Add(card);
            }
            else if (card.rarity == 5)
            {
                card.rarityTitle = "Legendary";
                card.chance = 10;
                card.rarityEffectColor = Color.yellow;
                level5Ore.Add(card);
            }
        }
    }

    // Roll for a new hand
    public void RollForHand()
    {
        playerHand.Clear();
        List<int> hand = new List<int>();
        for (int i = 0; i < cardLimit; i++)
        {
            bool cardRarityChance5 = CalulateRNGPercent(10);
            bool cardRarityChance4 = CalulateRNGPercent(17);
            bool cardRarityChance3 = CalulateRNGPercent(25);
            bool cardRarityChance2 = CalulateRNGPercent(50);
            if (cardRarityChance5)
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

    // Displays all the cards
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
        if (StartTimer == 1)

        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
        if (RollStatus == RollSkips)
        {
            StartTimer = 0;
            timer = 0;
            RollStatus = 0;
            RollButton.enabled = true;
            RollingText.text = "You rolled ";
            RarityEffectSprite.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0.5f);
            RarityEffectSprite2.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0.3f);

            for (int i = 0; i < playerHand.Count; i++)
            {
                StaticVariables.cash = StaticVariables.cash + playerHand[i].OrePrice;
            }
            CurrentMoney.text = StaticVariables.cash + "$";
        }
        if (timer > RollSpeed)
        {
            timer = 0;
            RollForHand();
            RollStatus += 1;
            RarityEffectSprite.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0);
            RarityEffectSprite2.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0);
        }
    }

    public void RollDice()
    {
        timer = 0;
        StartTimer = 1;
        RollForHand();
        RollButton.enabled = false;
        RollingText.enabled = true;
        RarityEffectSprite.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0);
        RarityEffectSprite2.color = new Color(RarityEffectSprite.color.r, RarityEffectSprite.color.g, RarityEffectSprite.color.b, 0);
        RollingText.text = "Rolling...";
    }
}