using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour, IDataPersistence
{
    public bool hasSeenTutorial;
    public bool ClaimedReward1;
    public bool ClaimedReward2;
    public bool ClaimedReward3;

    public GameObject tutorialPopup;
    public GameObject Tutorial;
    public GameObject RequirementsPanel;

    public GameObject RollButtonHighlight;
    public GameObject InventoryHighlight;
    public GameObject UpgradeHighlight;
    public GameObject CrafterHighlight;
    public GameObject StatsHighlight;
    public GameObject RebirthHighlight;

    public int tutorialStep = 0;
    public Text TutorialText;
    public Text RequirementText;
    public Text RewardText;
    [SerializeField] private Button RollButton;

    public MoneyLogic moneyLogic;
    public RNGscript rngScript;
    public Minerscript minerScript;

    public void LoadData(GameData data)
    {
        this.hasSeenTutorial = data.HasSeenTutorial;
        this.ClaimedReward1 = data.ClaimedReward1;
        this.ClaimedReward2 = data.ClaimedReward2;
        this.ClaimedReward3 = data.ClaimedReward3;
        ShowStartPopup();
    }
    public void SaveData(ref GameData data)
    {
        data.HasSeenTutorial = this.hasSeenTutorial;
        data.ClaimedReward1 = this.ClaimedReward1;
        data.ClaimedReward2 = this.ClaimedReward2;
        data.ClaimedReward3 = this.ClaimedReward3;
    }

    private void Start()
    {
        TutorialText = Tutorial.GetComponentInChildren<Text>();
        RequirementText = RequirementsPanel.GetComponentInChildren<Text>();
    }
    private void ShowStartPopup()
    {
        if (!hasSeenTutorial)
        {
            ShowTutorialRequest();
        }
    }

    private void ShowTutorialRequest()
    {
        tutorialPopup.SetActive(true);
    }

    public void CloseTutorial()
    {
        hasSeenTutorial = true;
        ClaimedReward1 = true;
        ClaimedReward2 = true;
        ClaimedReward3 = true;
        tutorialPopup.SetActive(false);
    }

    // step 1
    public void ConfirmTutorialRequest()
    {
        tutorialPopup.SetActive(false);
        Tutorial.SetActive(true);
        TutorialText.text = "Welcome to Gem RNG! Lets get started with the basics, click 'click me to mine' to roll your first Gem";
        RollButtonHighlight.SetActive(true);
        tutorialStep = 1;
    }
    // step 2
    public void OnClickRollButton()
    {
        if (tutorialStep == 1)
        {
            RollButtonHighlight.SetActive(false);
            InventoryHighlight.SetActive(true);
            Tutorial.GetComponentInParent<RectTransform>().anchoredPosition = new Vector2(0, -140);
            Tutorial.GetComponentInParent<RectTransform>().anchorMax = new Vector2(0.5f, 1);
            Tutorial.GetComponentInParent<RectTransform>().anchorMin = new Vector2(0.5f, 1);
            TutorialText.text = "Great! Now you have a gem, click the backpack icon to the right to open the inventory. In here you can see your gems and you can sell them";
            tutorialStep = 2;
        }
    }
    // step 3
    public void OnClickInventoryButton()
    {
        if (tutorialStep == 2)
        {
            InventoryHighlight.SetActive(false);
            TutorialText.text = "Now you can see your gems, in 'Enter amount' you can select the amount you would like to sell and then simply click 'Sell' (click to continue)";
            tutorialStep = 3;
        }
    }

    public void OnClickUpgradeButton()
    {
        if (tutorialStep == 4)
        {
            UpgradeHighlight.SetActive(false);
            TutorialText.text = "As you can see you need money as well as gems to purchase upgrades. Lets try to get 'Pickaxe speed' first";
            RequirementsPanel.SetActive(true);
            CheckRecuirementsCompletion();
        }
    }

    private void CheckRecuirementsCompletion()
    {
        bool hasRollspeed1 = moneyLogic.BoughtRollSpeed > 0 && tutorialStep == 4;
        bool hasBolts = tutorialStep == 7 && minerScript.Materials[14].StorageAmount >= 5;
        bool hasAutoRollUpgrade = tutorialStep == 9 && moneyLogic.BoughtAutoRollUpgrade >= 1;
        if (hasRollspeed1 || hasBolts || hasAutoRollUpgrade)
        {
            RequirementsPanel.SetActive(true);
            RequirementText.text = "Seems like you already have this upgrade";
            switch (tutorialStep)
            {
                case 4:
                    CrafterHighlight.SetActive(true);
                    TutorialText.text = "You need money as well as gems to buy them but already know this. Click on the button with the 2 hammers to continue";
                    tutorialStep = 5;
                    break;
                case 7:
                    TutorialText.text = "Oh, you already have bolts. Well then lets get the coal generator next (click to continue)";
                    tutorialStep = 8;
                    break;
                case 9:
                    TutorialText.text = "You have the 'auto mine' upgrade already? Then i will explain to how to use it (click to continue)";
                    tutorialStep = 10;
                    break;
            }
        }
        else
        {
            UpdateRequirementsText();
        }
    }
    public void UpdateRequirementsText()
    {
        int money = 25;
        // fancy if statement. ? stands for if true, : stands for if false
        string moneyColor = moneyLogic.Money >= money ? "green" : "white";
        string stoneGemColor = rngScript.allOres[5].StorageAmount >= 5 ? "green" : "white";
        string rustyGemColor = rngScript.allOres[6].StorageAmount >= 1 ? "green" : "white";
        string boltsColor = minerScript.Materials[14].StorageAmount >= 5 ? "green" : "white";
        string smallDrillColor = minerScript.Materials[12].StorageAmount >= 1 ? "green" : "white";
        string coalGenererator = minerScript.Materials[13].StorageAmount >= 1 ? "green" : "white";
        string drillColor = moneyLogic.BoughtAutoRollUpgrade >= 1 ? "green" : "white";
        if (tutorialStep == 4)
        {

            RequirementText.text = "Requirements \n" +
                               $"<color={moneyColor}>Money: {moneyLogic.Money}/25</color>\n" +
                               $"<color={stoneGemColor}>Stone Gem: {rngScript.allOres[5].StorageAmount}/5</color>\n" +
                               $"<color={rustyGemColor}>Rusty Gem: {rngScript.allOres[6].StorageAmount}/1</color>";
            if (moneyLogic.Money >= 25 && rngScript.allOres[5].StorageAmount >= 5 && rngScript.allOres[6].StorageAmount >= 1)
            {
                RequirementsPanel.SetActive(true);
                TutorialText.text = "Looks like You have all the requirements, great! Now click the Purchase button to buy the 'Pickaxe speed' upgrade";
                tutorialStep = 5;
                CrafterHighlight.SetActive(true);
            }

        }
        else if (tutorialStep == 7)
        {
            RequirementText.text = $"Requirements \n<color={boltsColor}>Bolts: {minerScript.Materials[14].StorageAmount}/5</color>";
            RewardText.text = "Reward \n $250";
            if (minerScript.Materials[14].StorageAmount >= 5)
            {
                RequirementsPanel.SetActive(true);
                TutorialText.text = "Great!, now the last part missing is the coal generator (click to continue)";
                tutorialStep = 8;
            }
        }
        else if (tutorialStep == 9)
        {
            money = 250;
            RequirementText.text = "Requirements \n" +
                $"<color={moneyColor}>Money: {moneyLogic.Money}/250</color>\n" +
                $"<color={boltsColor}>Bolts: {minerScript.Materials[14].StorageAmount}/5</color>\n" +
                $"<color={smallDrillColor}>Rusty Drill {minerScript.Materials[12].StorageAmount}/1</color>\n" +
                $"<color={coalGenererator}>Coal Generator: {minerScript.Materials[13].StorageAmount}/1</color>\n" +
                $"<color={drillColor}>Buy 'Auto mine' upgrade {moneyLogic.BoughtAutoRollUpgrade}/1</color>";

            RewardText.text = "Reward \n 10 Steel";
            if (moneyLogic.BoughtAutoRollUpgrade >= 1)
            {
                RequirementsPanel.SetActive(true);
                TutorialText.text = "Fantastic! You got yourself 'Auto mine'. Next i explain how to use it (click to continue)";
                tutorialStep = 10;
            }
        }
    }
    public void onClickForCraftingMenu()
    {
        if (tutorialStep == 5 && ClaimedReward1)
        {
            RequirementsPanel.SetActive(false);
            TutorialText.text = "If you click through it, you see that you have the Rusty Drill. That is needed for the 'Auto mine' upgrade (click to continue)";
            tutorialStep = 6;
            CrafterHighlight.SetActive(false);
        }
    }
    public void onClickForClaimRewards()
    {
        if (tutorialStep == 5)
        {
            StartCoroutine(ClaimReward1);
        }
        else if (tutorialStep == 8)
        {
            StartCoroutine(ClaimReward2);
        }
        else if (tutorialStep == 10)
        {
            StartCoroutine(ClaimReward3);
        }
    }
    public IEnumerator ClaimReward2
    {
        get
        {
            if (minerScript.Materials[14].StorageAmount >= 5 && !ClaimedReward2)
            {
                TutorialText.text = "Great!, now the last part missing is the coal generator (click to continue)";
                RequirementText.text = "Reward claimed";
                moneyLogic.Money += 250;
                ClaimedReward2 = true;
                yield return new WaitForSeconds(1f);
                RequirementsPanel.SetActive(false);
                tutorialStep = 8;
            }
            else if (minerScript.Materials[14].StorageAmount >= 5 && ClaimedReward2)
            {
                TutorialText.text = "You have already claimed this reward";
                yield return new WaitForSeconds(1f);
                TutorialText.text = "Great!, now the last part missing is the coal generator (click to continue)";
                tutorialStep = 8;
            }
            else
            {
                TutorialText.text = "Looks like you dont have all the requirements yet, try to get them first";
                yield return new WaitForSeconds(1f);
                TutorialText.text = "Craft some bolts";
            }
        }
    }
    public IEnumerator ClaimReward1
    {
        get
        {
            if (moneyLogic.Money >= 25 && rngScript.allOres[5].StorageAmount >= 5 && rngScript.allOres[6].StorageAmount >= 1 && !ClaimedReward1)
            {
                TutorialText.text = "Next click on the button with 2 hammers. This is the crafting menu. In here you can craft items for more expensive upgrades";
                RequirementText.text = "Reward claimed";
                minerScript.Materials[12].StorageAmount++;
                ClaimedReward1 = true;
                yield return new WaitForSeconds(1f);
                RequirementsPanel.SetActive(false);
            }
            else if (moneyLogic.Money >= 25 && rngScript.allOres[5].StorageAmount >= 5 && rngScript.allOres[6].StorageAmount >= 1 && ClaimedReward1)
            {
                TutorialText.text = "You have already claimed this reward";
                yield return new WaitForSeconds(1f);
                TutorialText.text = "Looks like You have all the requirements, great! Now click the Purchase button to buy the 'Pickaxe speed' upgrade";
            }
            else
            {
                TutorialText.text = "Looks like you dont have all the requirements yet, try to get them first";
                yield return new WaitForSeconds(1f);
                TutorialText.text = "As you can see you need money as well as gems to purchase upgrades. Lets try to get 'Pickaxe speed' first";
            }
        }
    }
    public IEnumerator ClaimReward3
    {
        get
        {
            bool boughtupgrade1 = moneyLogic.BoughtAutoRollUpgrade >= 1;
            if (boughtupgrade1 && !ClaimedReward3)
            {
                TutorialText.text = "Fantastic! You got yourself 'Auto mine'. Next i explain how to use it (click to continue)";
                RequirementText.text = "Reward claimed";
                rngScript.allOres[14].StorageAmount += 10;
                ClaimedReward3 = true;
                yield return new WaitForSeconds(1f);
                RequirementsPanel.SetActive(false);
            }
            else if (boughtupgrade1 && ClaimedReward3)
            {
                TutorialText.text = "You have already claimed this reward";
                yield return new WaitForSeconds(1f);
                TutorialText.text = "";
            }
            else
            {
                TutorialText.text = "Looks like you dont have all the requirements yet, try to get them first";
                yield return new WaitForSeconds(1f);
                TutorialText.text = "";
            }
        }
    }

    private void Update()
    {
        switch (tutorialStep)
        {
            // step 4
            case 3:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    UpgradeHighlight.SetActive(true);
                    TutorialText.text = "Now click the shopping cart icon, here you can see upgrades to your pickaxe. Your pickaxe determines the speed, luck and money of your gems";
                    tutorialStep = 4;
                }
                break;
            case 6:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    RequirementsPanel.SetActive(true);
                    TutorialText.text = "I am helping you get it faster but you need to do some work yourself too. Craft some bolts. you need them for the upgrade. You may already have enough for it";
                    tutorialStep = 7;
                    CheckRecuirementsCompletion();
                }
                break;
            case 8:
                if (Input.GetKeyDown(KeyCode.Mouse0) && !ClaimedReward2)
                {
                    TutorialText.text = "Claim your reward before continuing";
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && ClaimedReward2)
                {
                    TutorialText.text = "I think you can do the rest yourself. \n Objective: get 'Auto mine' upgrade 1";
                    tutorialStep = 9;
                    RequirementsPanel.SetActive(true);
                    CheckRecuirementsCompletion();
                }
                break;
            case 10:
                if (Input.GetKeyDown(KeyCode.Mouse0) && !ClaimedReward3)
                {
                    TutorialText.text = "Claim your reward before continuing";
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && ClaimedReward3)
                {
                    TutorialText.text = "As you can see the drill mines automatically for you, but not forever. In the bottom left corner you can see it has a duration. (click to continue)";
                    tutorialStep = 11;
                }
                break;
            case 11:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "When the timer reaches 0 it will be on cooldown and start up again when that finishes. you can turn it on and off with the button displayed (click to continue)";
                    tutorialStep = 12;
                }
                break;
            case 12:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "Under the 'Auto mine' you see auto sell. when enabled all gems you mine will be automatically sold. They also autosell when your gem storage is full (click to continue)";
                    tutorialStep = 13;
                }
                break;
            case 13:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "You have a exp bar as well. When you reach level 100 you can rebirth (click to continue)";
                    tutorialStep = 14;
                }
                break;
            case 14:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "On the left side of the exp bar is the rebirth button (click to continue)";
                    tutorialStep = 15;
                    RebirthHighlight.SetActive(true);
                }
                break;
            case 15:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "When you can eventually rebirth you will unlock a new world with better gems. This does mean common ores are replaced (click to continue)";
                    tutorialStep = 16;
                    RebirthHighlight.SetActive(false);
                }
                break;
            case 16:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "But don't worry about that. You can always switch back with the portal icon or generate gems with drills in the crafter menu (click to continue)";
                    tutorialStep = 17;
                }
                break;
            case 17:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "One last thing before i leave you alone. On the other side of the xp bar is the stats menu. In here you can see your current stats (click to continue)";
                    tutorialStep = 18;
                    StatsHighlight.SetActive(true);
                }
                break;
            case 18:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "Well thats about it. Enjoy your time here and goodluck! (click to end tutorial)";
                    tutorialStep = 19;
                    StatsHighlight.SetActive(false);
                }
                break;
            case 19:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Tutorial.SetActive(false);
                    RequirementsPanel.SetActive(false);
                    hasSeenTutorial = true;
                    tutorialStep = 20;
                }
                break;
        }
    }

    public void DebugTutorialStepUp()
    {
        tutorialStep++;
    }
}
