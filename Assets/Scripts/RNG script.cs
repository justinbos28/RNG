using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RNGscript : MonoBehaviour, IDataPersistence
{
    [Header("Ores")]
    public List<OreClass> allOres = new List<OreClass>();
    public List<OreClass> CommonOres = new List<OreClass>();
    public List<OreClass> UncommonOres = new List<OreClass>();
    public List<OreClass> RareOres = new List<OreClass>();
    public List<OreClass> EpicOres = new List<OreClass>();
    public List<OreClass> LegendaryOres = new List<OreClass>();
    public List<OreClass> MythicOres = new List<OreClass>();
    public List<OreClass> ExoticOres = new List<OreClass>();
    public List<OreClass> DivineOres = new List<OreClass>();
    public List<OreClass> SecretOres = new List<OreClass>();

    [Header("Ore Ui")]
    public List<GameObject> Dices = new List<GameObject>();
    public List<SpriteRenderer> RarityEffectSprite = new List<SpriteRenderer>();
    public List<SpriteRenderer> RarityEffectSprite2 = new List<SpriteRenderer>();
    public List<Sprite> Stones = new List<Sprite>();
    [Header("Particle Effects")]
    public List<Particles> RarityParticles = new List<Particles>();


    public List<OreClass> playerHand = new List<OreClass>();
    [Header("Limit")] public int cardLimit = 1;

    [Header("Others")]
    public float LocalScale = 1;
    public float timer = 0;
    public float timer2 = 0;
    public float DelayTimeBetweenRolls = 2;
    public float DelayTimeBetweenRollsReference = 2f; // used for saving the delay time since when you get a rare ore the delay time will be longer so you have time to see what you got
    public float TestSecretRarity = 0.0001f; // used for testing the secret ore chance

    [Header("Ore upgrades")]
    public float RollSpeed = 0.70f;
    public float LuckPercentage = 1;
    public float LuckMultiplier = 1;
    public float MoneyMultiplier = 1;

    public int RollStatus;
    public int RollSkips = 5;
    public int StoneStatus;

    [Header("Bools")]
    public bool active = false;
    public bool StartTimer;
    public bool AutoTimer = true;
    public bool AutoSell = false;
    public bool Timer2Active = false;
    public bool AutoRollTimer = false;
    public bool UnlockedSecret = false;

    public Text RollingText;
    public Text CurrentMoney;

    public MoneyLogic MoneyLogic;
    public XPScript XPScript;
    public OreStorage OreStorage;
    [Header("buttons and effects")]
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
        this.UnlockedSecret = data.Test;
        
        StoneStatus = RollSkips;
    }
    public void SaveData(ref GameData data)
    {
        data.CardLimit = this.cardLimit;
        data.LuckMultiplier = this.LuckMultiplier;
        data.LuckPercentage = this.LuckPercentage;
        data.MoneyMultiplier = this.MoneyMultiplier;
        data.RollSkips = this.RollSkips;
        data.RollSpeed = this.RollSpeed;
        data.Test = this.UnlockedSecret;
    }
    // end saving and getting saved data


    public static bool CalulateRNGPercent(float percent)
    {
        float temp;
        temp = Random.Range(0f, 100f);
        if (temp > 0 && temp < percent)
        {
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
                case 64:
                    CommonOres.Add(card);
                    break;
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                case 65:
                    UncommonOres.Add(card);
                    break;
                case 15:
                case 16:
                case 17:
                case 18:
                case 19:
                case 20:
                case 21:
                case 66:
                    RareOres.Add(card);
                    break;
                case 22:
                case 23:
                case 24:
                case 25:
                case 26:
                case 27:
                case 28:
                case 67:
                    EpicOres.Add(card);
                    break;
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
                case 34:
                case 35:
                case 68:
                    LegendaryOres.Add(card);
                    break;
                case 36:
                case 37:
                case 38:
                case 39:
                case 40:
                case 41:
                case 42:
                case 69:
                    MythicOres.Add(card);
                    break;
                case 43:
                case 44:
                case 45:
                case 46:
                case 47:
                case 48:
                case 49:
                case 70:
                    ExoticOres.Add(card);
                    break;
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 71:
                    DivineOres.Add(card);
                    break;
                case 57:
                case 58:
                case 59:
                case 60:
                case 61:
                case 62:
                case 63:
                case 72:
                    SecretOres.Add(card);
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
            Dictionary<int, float> oreRarityChances = new Dictionary<int, float>
            {
                { 12, 0.000001f }, { 11, 0.000099f }, // mythic ores
                { 10, 0.00001f }, { 9, 0.00099f }, // legendary ores
                { 8, 0.0001f }, { 7, 0.0099f }, // epic ores
                { 6, 0.001f }, { 5, 0.099f }, // rare ores
                { 4, 0.01f },{ 3, 0.99f }, // uncommon ores
                { 2, 1f }, { 1, 100f } // common ores
            };

            
            Dictionary<int, bool> oreRarityResults = oreRarityChances.ToDictionary(
                kvp => kvp.Key,
                kvp => CalulateRNGPercent(kvp.Value * LuckPercentage * LuckMultiplier * XPScript.XPLuckMultiplier)
            );

            float temp;
            temp = Random.Range(0f, 100f);
            if (temp > 0 && temp < TestSecretRarity)
            {
                //print(temp);
                AddOreToHand(SecretOres, hand);
            }
            else
            {
                //print(temp);
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
                                case >= 11 and <= 12:
                                    AddOreToHand(MythicOres, hand);
                                    break;
                                case >= 9 and <= 10:
                                    AddOreToHand(LegendaryOres, hand);
                                    break;
                                case >= 7 and <= 8:
                                    AddOreToHand(EpicOres, hand);
                                    break;
                                case >= 5 and <= 6:
                                    AddOreToHand(RareOres, hand);
                                    break;
                                case >= 3 and <= 4:
                                    AddOreToHand(UncommonOres, hand);
                                    break;
                                case >= 1 and <= 2:
                                    AddOreToHand(CommonOres, hand);
                                    break;
                            }
                        }
                        // assign new ores if the player has rebirthed
                        else if (XPScript.Rebirth == 1)
                        {
                            switch (oreRarityResult.Key)
                            {
                                case >= 11 and <= 12:
                                    AddOreToHand(ExoticOres, hand);
                                    break;
                                case >= 9 and <= 10:
                                    AddOreToHand(MythicOres, hand);
                                    break;
                                case >= 7 and <= 8:
                                    AddOreToHand(LegendaryOres, hand);
                                    break;
                                case >= 5 and <= 6:
                                    AddOreToHand(EpicOres, hand);
                                    break;
                                case >= 3 and <= 4:
                                    AddOreToHand(RareOres, hand);
                                    break;
                                case >= 1 and <= 2:
                                    AddOreToHand(UncommonOres, hand);
                                    break;
                            }
                        }
                        else
                        {
                            switch (oreRarityResult.Key)
                            {
                                case >= 11 and <= 12:
                                    AddOreToHand(DivineOres, hand);
                                    break;
                                case >= 9 and <= 10:
                                    AddOreToHand(ExoticOres, hand);
                                    break;
                                case >= 7 and <= 8:
                                    AddOreToHand(MythicOres, hand);
                                    break;
                                case >= 5 and <= 6:
                                    AddOreToHand(LegendaryOres, hand);
                                    break;
                                case >= 3 and <= 4:
                                    AddOreToHand(EpicOres, hand);
                                    break;
                                case >= 1 and <= 2:
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
            switch (playerHand[i].rarityTitle)
            {
                case "Rare":
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1].material.mainTexture = RarityParticles[0].ParticleImage;
                    break;
                case "Epic":
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1].material.mainTexture = RarityParticles[1].ParticleImage;
                    break;
                case "Legendary":
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[0].material.mainTexture = RarityParticles[2].ParticleImage;
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1].material.mainTexture = RarityParticles[2].ParticleImage;
                    break;
                case "Mythic":
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[0].material.mainTexture = RarityParticles[3].ParticleImage;
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1].material.mainTexture = RarityParticles[3].ParticleImage;
                    break;
                case "Exotic":
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[0].material.mainTexture = RarityParticles[4].ParticleImage;
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1].material.mainTexture = RarityParticles[4].ParticleImage;
                    break;
                case "Divine":
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[0].material.mainTexture = RarityParticles[5].ParticleImage;
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1].material.mainTexture = RarityParticles[5].ParticleImage;
                    break;
                case "Secret":
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[0].material.mainTexture = RarityParticles[6].ParticleImage;
                    RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1].material.mainTexture = RarityParticles[6].ParticleImage;
                    UnlockedSecret = true;
                    break;
            }

            titles[i].text = playerHand[i].Name;
            OreSpriteRenderer[i].color = playerHand[i].rarityEffectColor;
            effects[i].text = "1 in " + playerHand[i].chance;
            rarity[i].text = playerHand[i].rarityTitle;
            OrePicture[i].sprite = playerHand[i].OrePicture;
            if (new string[] { "Rare", "Epic", "Legendary", "Mythic", "Exotic", "Divine", "Secret" }.Contains(playerHand[i].rarityTitle) && RarityParticles[i].ParticlePrefab.activeSelf == true)
            {
                RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystem>()[1].Clear();
                RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystem>()[1].Play();
                if (new string[] { "Legendary", "Mythic", "Exotic", "Divine", "Secret" }.Contains(playerHand[i].rarityTitle))
                {
                    if (RarityParticles[i].ParticlePrefab.activeSelf == true)
                    {
                        RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystem>()[0].Clear();
                        RarityParticles[i].ParticlePrefab.GetComponentsInChildren<ParticleSystem>()[0].Play();
                    }
                    else
                    {
                        RarityParticles[i].ParticlePrefab.SetActive(true);
                    }
                }
            }
            else
            {
                RarityParticles[i].ParticlePrefab.SetActive(true);
            }
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

        CurrentMoney.text = "$" + NumberFormatter(MoneyLogic.Money);

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
            if (timer2 > DelayTimeBetweenRolls) // bigger
            {
                MoneyLogic.CheckDrillStatus();
                if (MoneyLogic.AutoRoll)
                {
                    RollButton.SetActive(false);
                }
                else
                {
                    RollButton.SetActive(true);
                }
                if (AutoTimer == StartTimer)
                {
                    for (int i = 0; i < RarityEffectSprite.Count; i++)
                    {
                        RarityEffectSprite[i].color = new Color(RarityEffectSprite[i].color.r, RarityEffectSprite[i].color.g, RarityEffectSprite[i].color.b, 0);
                        RarityEffectSprite2[i].color = new Color(RarityEffectSprite[i].color.r, RarityEffectSprite[i].color.g, RarityEffectSprite[i].color.b, 0);
                    }                  
                    RollingText.enabled = true;
                    for (int i = 0; i < playerHand.Count; i++)
                    {
                        titles[i].color = Color.black;
                        effects[i].color = Color.black;
                        rarity[i].color = Color.black;

                        titles[i].text = "";
                        effects[i].text = "";
                        rarity[i].text = "";
                        OrePicture[i].sprite = Stones[0];
                    }

                    RollingText.text = "Mining...";
                }
                RollStatus = 0;
                AutoRollTimer = false;
                timer2 = 0;
            }
            else if (timer2 < DelayTimeBetweenRolls) // smaller
            {
                StartTimer = !AutoTimer;
                timer = 0;
                AutoRollTimer = true;
            }
        }

        if (active)
        {
            if (LocalScale > 1)
            {
                LocalScale = 1;
                active = false;
            }
            else
            {
                LocalScale += Time.deltaTime * 3;
            }
        }

        for (int i = 0; i < Dices.Count; i++)
        {
            Dices[i].transform.localScale = new Vector3(LocalScale, LocalScale, LocalScale);
        }

        // showing the ore and giving the price
        if (RollStatus == RollSkips)
        {
            StoneStatus = RollSkips;
            RollForHand();
            RollingText.text = "You found ";
            LocalScale = 0;
            active = true;

            for (int i = 0; i < Dices.Count; i++)
            {
                Dices[i].transform.localScale = new Vector3(LocalScale, LocalScale, LocalScale);
            }

            for (int i = 0; i < RarityEffectSprite.Count; i++)
            {
                    RarityEffectSprite[i].color = new Color(RarityEffectSprite[i].color.r, RarityEffectSprite[i].color.g, RarityEffectSprite[i].color.b, 0.5f);
                    RarityEffectSprite2[i].color = new Color(RarityEffectSprite[i].color.r, RarityEffectSprite[i].color.g, RarityEffectSprite[i].color.b, 0.3f);
            }
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
                if (new int[] { 2, 11, 23, 36, 37, 42, 57 }.Contains(playerHand[i].OreID))
                {
                    titles[i].color = Color.white;
                    effects[i].color = Color.white;
                    rarity[i].color = Color.white;
                }
                /// optimize this code in the future
                // auto sells if ores are more then the common ore storage
                if (playerHand[i].rarityTitle == "Common")
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxCommonOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxCommonOres;
                    }
                }
                // autosell for uncommon ores
                if (playerHand[i].rarityTitle == "Uncommon")
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxUncommonOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxUncommonOres;
                    }
                }
                // autosell for rare ores
                if (playerHand[i].rarityTitle == "Rare")
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxRareOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxRareOres;
                    }
                }
                // autosell for epic ores
                if (playerHand[i].rarityTitle == "Epic")
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxEpicOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxEpicOres;
                    }
                }
                // autosell for legendary ores
                if (playerHand[i].rarityTitle == "Legendary")
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxLegendaryOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxLegendaryOres;
                    }
                }
                // autosell for mythic ores
                if (playerHand[i].rarityTitle == "Mythic")
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxMythicOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxMythicOres;
                    }
                }
                // autosell for exotic ores
                if (playerHand[i].rarityTitle == "Exotic")
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxExoticOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxExoticOres;
                    }
                }
                // autosell for divine ores
                if (playerHand[i].rarityTitle == "Divine")
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxDivineOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxDivineOres;
                    }
                }
                // autosell for secret ores
                if (playerHand[i].rarityTitle == "Secret")
                {
                    if (playerHand[i].StorageAmount >= OreStorage.MaxSecretOres)
                    {
                        MoneyLogic.Money += playerHand[i].OrePrice * MoneyMultiplier;
                        playerHand[i].StorageAmount = OreStorage.MaxSecretOres;
                    }
                }
                /// end optimize this code in the future
            }
            // updates the inventory and xp
            OreStorage.UpdateInventory();
            XPScript.UpdateXP();
            RollStatus++;
        }
        // rolling the dice (roll animation)
        if (timer > RollSpeed)
        {
            StoneStatus--;
            timer = 0;
            RollStatus += 1;
            for (int i = 0; i < RarityEffectSprite.Count; i++)
            {
                RarityEffectSprite[i].color = new Color(RarityEffectSprite[i].color.r, RarityEffectSprite[i].color.g, RarityEffectSprite[i].color.b, 0f);
                RarityEffectSprite2[i].color = new Color(RarityEffectSprite[i].color.r, RarityEffectSprite[i].color.g, RarityEffectSprite[i].color.b, 0f);
            }

            RollingText.text = "Mining...";

            for (int i = 0; i < playerHand.Count; i++)
            {
                titles[i].color = Color.black;
                effects[i].color = Color.black;
                rarity[i].color = Color.black;

                titles[i].text = "";
                effects[i].text = "";
                rarity[i].text = "";
                OrePicture[i].sprite = Stones[StoneStatus];
            }
        }
        // aligns the ores to the corerct spot after buying more roll amount
        switch (cardLimit - 1)
        {
            case 0:
            default:
                SetRollButtonActive(RollButton1, new Vector3(0, 0, 100));
                break;
            case 1:
                SetRollButtonActive(RollButton2, new Vector3(3, 0, 100));
                SetRollButtonActive(RollButton1, new Vector3(-3, 0, 100));
                EnableRarityEffectSprites(1);
                EnableRarityEffectSprites2(1);
                break;
            case 2:
                SetRollButtonActive(RollButton3, new Vector3(-5, 0, 100));
                SetRollButtonActive(RollButton2, new Vector3(5, 0, 100));
                SetRollButtonActive(RollButton1, new Vector3(0, 0, 100));
                EnableRarityEffectSprites(1, 2);
                EnableRarityEffectSprites2(1,2);
                break;
            case 3:
                SetRollButtonActive(RollButton4, new Vector3(-3, -1.5f, 100));
                SetRollButtonActive(RollButton3, new Vector3(-3, 1.5f, 100));
                SetRollButtonActive(RollButton2, new Vector3(3, 1.5f, 100));
                SetRollButtonActive(RollButton1, new Vector3(3, -1.5f, 100));
                EnableRarityEffectSprites(1, 2, 3);
                EnableRarityEffectSprites2(1, 2, 3);
                break;
            case 4:
                SetRollButtonActive(RollButton5, new Vector3(5, -1.5f, 100));
                SetRollButtonActive(RollButton4, new Vector3(-5, -1.5f, 100));
                SetRollButtonActive(RollButton3, new Vector3(-5, 1.5f, 100));
                SetRollButtonActive(RollButton2, new Vector3(5, 1.5f, 100));
                SetRollButtonActive(RollButton1, new Vector3(0, 0, 100));
                EnableRarityEffectSprites(1, 2, 3, 4);
                EnableRarityEffectSprites2(1, 2, 3, 4);
                break;
            case 5:
                SetRollButtonActive(RollButton6, new Vector3(0, -1.5f, 100));
                SetRollButtonActive(RollButton5, new Vector3(5, -1.5f, 100));
                SetRollButtonActive(RollButton4, new Vector3(-5, -1.5f, 100));
                SetRollButtonActive(RollButton3, new Vector3(-5, 1.5f, 100));
                SetRollButtonActive(RollButton2, new Vector3(5, 1.5f, 100));
                SetRollButtonActive(RollButton1, new Vector3(0, 1.5f, 100));
                EnableRarityEffectSprites(1, 2, 3, 4, 5);
                EnableRarityEffectSprites2(1, 2, 3, 4, 5);
                break;
        }

    }
    // button connected first roll
    public void RollDice()
    {
        for (int i = 0; i < RarityEffectSprite.Count; i++)
        {
            RarityEffectSprite[i].color = new Color(RarityEffectSprite[i].color.r, RarityEffectSprite[i].color.g, RarityEffectSprite[i].color.b, 0);
            RarityEffectSprite2[i].color = new Color(RarityEffectSprite[i].color.r, RarityEffectSprite[i].color.g, RarityEffectSprite[i].color.b, 0);
        }
        timer = 0;
        StartTimer = AutoTimer;
        //RollForHand();
        RollButton.SetActive(false);
        RollingText.enabled = true;

        for (int i = 0; i < playerHand.Count; i++)
        {
            titles[i].color = Color.black;
            effects[i].color = Color.black;
            rarity[i].color = Color.black;

            titles[i].text = "";
            effects[i].text = "";
            rarity[i].text = "";
            OrePicture[i].sprite = Stones[0];
        }

        RollingText.text = "Mining...";
    }
    void SetRollButtonActive(GameObject button, Vector3 position)
    {
        button.SetActive(true);
        button.transform.position = position;
    }

    void EnableRarityEffectSprites(params int[] indices)
    {
        foreach (int index in indices)
        {
            RarityEffectSprite[index].enabled = true;
        }
    }

    void EnableRarityEffectSprites2(params int[] indices)
    {
        foreach (int index in indices)
        {
            RarityEffectSprite2[index].enabled = true;
        }
    }

}