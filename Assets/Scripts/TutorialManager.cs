using System.Collections;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour, IDataPersistence
{
    public bool hasSeenTutorial;
    public bool ClaimedReward1;
    public bool ClaimedReward2;

    public GameObject tutorialPopup;
    public GameObject Tutorial;
    public GameObject RequirementsPanel;
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
        ShowStartPopup();
    }
    public void SaveData(ref GameData data)
    {
        data.HasSeenTutorial = this.hasSeenTutorial;
        data.ClaimedReward1 = this.ClaimedReward1;
        data.ClaimedReward2 = this.ClaimedReward2;
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
        tutorialPopup.SetActive(false);
    }

    // step 1
    public void ConfirmTutorialRequest()
    {
        //hasSeenTutorial = true;
        tutorialPopup.SetActive(false);
        Tutorial.SetActive(true);
        TutorialText.text = "Welcome to Gem RNG! Lets get started with the basics, click 'click me to mine' to roll your first Gem";
        tutorialStep = 1;
    }
    // step 2
    public void OnClickRollButton()
    {
        if (tutorialStep == 1)
        {
            Tutorial.transform.position = new Vector3(0, 3.5f, 0);
            TutorialText.text = "Great! Now you have a gem, click the backpack icon to the right to open the inventory. In here you can see your gems and you can sell them";
            tutorialStep = 2;
        }
    }
    // step 3
    public void OnClickInventoryButton()
    {
        if (tutorialStep == 2)
        {
            TutorialText.text = "Now you can see your gems, in 'Enter amount' you can select the amount you would like to sell and then simply click 'Sell' (click to continue)";
            tutorialStep = 3;
        }
    }

    public void OnClickUpgradeButton()
    {
        if (tutorialStep == 4)
        {
            TutorialText.text = "As you can see you need money as well as gems to purchase upgrades. Lets try to get 'Pickaxe speed' first";
            RequirementsPanel.SetActive(true);
        }
    }

    public void UpdateRequirementsText()
    {
        if (moneyLogic.BoughtRollSpeed > 0 && tutorialStep == 4)
        {
            RequirementsPanel.SetActive(true);
            RequirementText.text = "Seems like you already have this upgrade";
            TutorialText.text = "You need money as well as gems to buy them but already know this. Click on the button with the 2 hammers to continue";
            tutorialStep = 5;
        }
        else
        {
            // fancy if statement. ? stands for if true, : stands for if false
            string moneyColor = moneyLogic.Money >= 25 ? "green" : "white";
            string stoneGemColor = rngScript.allOres[5].StorageAmount >= 5 ? "green" : "white";
            string rustyGemColor = rngScript.allOres[6].StorageAmount >= 1 ? "green" : "white";
            string BoltsColor = minerScript.Materials[14].StorageAmount >= 5 ? "green" : "white";
            if (tutorialStep == 4)
            {
                RequirementText.text = "Requirements \n" +
                                   $"<color={moneyColor}>Money: 25/{moneyLogic.Money}</color>\n" +
                                   $"<color={stoneGemColor}>Stone Gem: 5/{rngScript.allOres[5].StorageAmount}</color>\n" +
                                   $"<color={rustyGemColor}>Rusty Gem: 1/{rngScript.allOres[6].StorageAmount}</color>";
                if (moneyLogic.Money >= 25 && rngScript.allOres[5].StorageAmount >= 5 && rngScript.allOres[6].StorageAmount >= 1)
                {
                    RequirementsPanel.SetActive(true);
                    TutorialText.text = "Looks like You have all the requirements, great! Now click the Purchase button to buy the 'Pickaxe speed' upgrade";
                    tutorialStep = 5;
                }

            }
            else if (tutorialStep == 7)
            {
                RequirementText.text = $"Requirements \n<color={BoltsColor}>Bolts: 5/{minerScript.Materials[14].StorageAmount}</color>";
                RewardText.text = "Reward \n $250";
            }
        }     
    }
    public void onClickForCrafterButton()
    {

    }
    public void onClickForClaimRewards()
    {
        if (tutorialStep == 5)
        {
            StartCoroutine(ClaimReward1);
        }
        else if (tutorialStep == 7)
        {
            StartCoroutine(ClaimReward2);
        }
    }
    public IEnumerator ClaimReward2 
    {
        get
        {
            if (minerScript.Materials[14].StorageAmount >= 5 && ClaimedReward2 == false)
            {
                TutorialText.text = "Great!, now the last part missing is the coal generator (click to continue)";
                RequirementText.text = "Reward claimed";
                moneyLogic.Money += 250;
                ClaimedReward2 = true;
                yield return new WaitForSeconds(5f);
                RequirementsPanel.SetActive(false);
                tutorialStep = 8;
            }
            else if (minerScript.Materials[14].StorageAmount >= 5 && ClaimedReward2 == true)
            {
                TutorialText.text = "You have already claimed this reward";
                yield return new WaitForSeconds(5f);
                TutorialText.text = "Great!, now the last part missing is the coal generator (click to continue)";
                tutorialStep = 8;
            }
            else
            {
                TutorialText.text = "Looks like you dont have all the requirements yet, try to get them first";
                yield return new WaitForSeconds(5f);
                TutorialText.text = "Craft some bolts";
            }
        }
    }
    public IEnumerator ClaimReward1
    {
        get
        {
            if (moneyLogic.Money >= 25 && rngScript.allOres[5].StorageAmount >= 5 && rngScript.allOres[6].StorageAmount >= 1 && ClaimedReward1 == false)
            {
                TutorialText.text = "Next click on the button with 2 hammers. This is the crafting menu. In here you can craft items for more expensive upgrades (click to continue)";
                RequirementText.text = "Reward claimed";
                minerScript.Materials[12].StorageAmount++;
                ClaimedReward1 = true;
                yield return new WaitForSeconds(5f);
                RequirementsPanel.SetActive(false);
            }
            else if (moneyLogic.Money >= 25 && rngScript.allOres[5].StorageAmount >= 5 && rngScript.allOres[6].StorageAmount >= 1 && ClaimedReward1 == true)
            {
                TutorialText.text = "You have already claimed this reward";
                yield return new WaitForSeconds(5f);
                TutorialText.text = "Looks like You have all the requirements, great! Now click the Purchase button to buy the 'Pickaxe speed' upgrade";
            }
            else
            {
                TutorialText.text = "Looks like you dont have all the requirements yet, try to get them first";
                yield return new WaitForSeconds(5f);
                TutorialText.text = "As you can see you need money as well as gems to purchase upgrades. Lets try to get 'Pickaxe speed' first";
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
                    TutorialText.text = "Now click the shopping cart icon, here you can see upgrades to your pickaxe. Your pickaxe determines the speed, luck and money of your gems";
                    tutorialStep = 4;
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.Mouse0) && ClaimedReward1)
                {
                    RequirementsPanel.SetActive(false);
                    TutorialText.text = "If you click through it you see that you have the small drill. That is needed for the 'auto mine' upgrade (click to continue)";
                    tutorialStep = 6;
                }
                break;
            case 6:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    RequirementsPanel.SetActive(true);
                    TutorialText.text = "I am helping you get it faster but you need to do some work yourself too. Craft some bolts. you need them for the upgrade. You may already have enough for it";
                    tutorialStep = 7;
                    UpdateRequirementsText();
                }
                break;
            case 8:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "I think you can do the rest yourself";
                    tutorialStep = 9;
                    RollButton.interactable = true;
                    RequirementsPanel.SetActive(false);
                }
                break;
        }
    }
}
