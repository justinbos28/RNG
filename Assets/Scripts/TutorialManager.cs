using System.Collections;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour, IDataPersistence
{
    public bool hasSeenTutorial;
    public bool ClaimedReward; 

    public GameObject tutorialPopup;
    public GameObject Tutorial;
    public GameObject RequirementsPanel;
    public int tutorialStep = 0;
    public Text TutorialText;
    public Text RequirementText;
    [SerializeField] private Button RollButton;

    public MoneyLogic moneyLogic;
    public RNGscript rngScript;
    public Minerscript minerScript;

    public void LoadData(GameData data)
    {
        this.hasSeenTutorial = data.HasSeenTutorial;
        ShowStartPopup();
    }
    public void SaveData(ref GameData data)
    {
        data.HasSeenTutorial = this.hasSeenTutorial;
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
        ClaimedReward = true;
        tutorialPopup.SetActive(false);
    }

    public void ConfirmTutorialRequest()
    {
        //hasSeenTutorial = true;
        tutorialPopup.SetActive(false);
        Tutorial.SetActive(true);
        TutorialText.text = "Welcome to Gem RNG! Lets get started with the basics, click 'click me to mine' to roll your first Gem";
        tutorialStep = 1;
    }
    public void OnClickRollButton()
    {
        if (tutorialStep == 1)
        {
            Tutorial.transform.position = new Vector3(0, 3.5f, 0);
            TutorialText.text = "Great! Now you have a gem, click the backpack icon to the right to open the inventory. In here you can see your gems and you can sell them";
            tutorialStep = 2;
        }
    }

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
        if (moneyLogic.BoughtRollSpeed > 0)
        {
            RequirementText.text = "Hmm seems like you already have this upgrade. lets continue then";
            // add more dialoge here
            tutorialStep = 5;
        }
        else
        {
            //fancy if statement. ? stands for if true, : stands for if false
            string moneyColor = moneyLogic.Money >= 25 ? "green" : "white";
            string stoneGemColor = rngScript.allOres[5].StorageAmount >= 5 ? "green" : "white";
            string rustyGemColor = rngScript.allOres[6].StorageAmount >= 1 ? "green" : "white";

            RequirementText.text = "Requirements \n" +
                                   $"<color={moneyColor}>Money: 25/{moneyLogic.Money}</color>\n" +
                                   $"<color={stoneGemColor}>Stone Gem: 5/{rngScript.allOres[5].StorageAmount}</color>\n" +
                                   $"<color={rustyGemColor}>Rusty Gem: 1/{rngScript.allOres[6].StorageAmount}</color>";
            if (moneyLogic.Money >= 25 && rngScript.allOres[5].StorageAmount >= 5 && rngScript.allOres[6].StorageAmount >= 1)
            {
                TutorialText.text = "Looks like You have all the requirements, great! Now click the Purchase button to buy the 'Pickaxe speed' upgrade";
                tutorialStep = 5;
            }
        }     
    }
    public void onClickForClaimRewards()
    {
        StartCoroutine(ClaimReward());
    }
    public IEnumerator ClaimReward()
    {
        Debug.Log("Claiming reward");
        if (moneyLogic.Money >= 25 && rngScript.allOres[5].StorageAmount >= 5 && rngScript.allOres[6].StorageAmount >= 1 && ClaimedReward == false)
        {
            Debug.Log("reward claimed");
            RequirementText.text = "Reward claimed";
            minerScript.Materials[10].StorageAmount++;
            ClaimedReward = true;
            yield return new WaitForSeconds(5f);
            RequirementsPanel.SetActive(false);
        }
        else if (moneyLogic.Money >= 25 && rngScript.allOres[5].StorageAmount >= 5 && rngScript.allOres[6].StorageAmount >= 1 && ClaimedReward == true)
        {
            Debug.Log("reward already claimed");
            TutorialText.text = "You have already claimed this reward";
            yield return new WaitForSeconds(5f);
            TutorialText.text = "Looks like You have all the requirements, great! Now click the Purchase button to buy the 'Pickaxe speed' upgrade";
        }
        else
        {
            Debug.Log("not enough resources");
            TutorialText.text = "Looks like you dont have all the requirements yet, try to get them first";
            yield return new WaitForSeconds(5f);
            TutorialText.text = "As you can see you need money as well as gems to purchase upgrades. Lets try to get 'Pickaxe speed' first";
        }
    }

    private void Update()
    {
        switch (tutorialStep)
        {
            case 3:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    TutorialText.text = "Now click the shopping cart icon, here you can see upgrades to your pickaxe. Your pickaxe determines how fast you can mine, if you can do it automatically or how much luck you have";
                    tutorialStep = 4;
                }
                break;
        }
    }
}
