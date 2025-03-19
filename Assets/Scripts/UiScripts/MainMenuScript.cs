using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour,IDataPersistence
{
    public GameObject mainMenu;
    public XPScript XPScript;
    public DataPersistence DataPersistence;

    [SerializeField] public string MainMenuFile = "SavedGameFiles.Data";
    public static string CurrentFile = "None";
    public int Index;
    
    public Text CurrentFileName;
    public Text NameSetText;
    public List<Button> SaveFileButtons = new List<Button>();
    public List<Text> SaveFileTexts = new List<Text>();
    public List<SaveFile> SaveFiles = new List<SaveFile>();

    public GameObject SaveFilePanel;
    public GameObject DeletionPanel;
    public InputField SaveFileInputField;
    public void LoadData(GameData data)
    {
        if (data.SaveFiles.Count != this.SaveFiles.Count)
        {
            int difference = this.SaveFiles.Count - data.SaveFiles.Count;
            for (int i = 0; i < difference; i++)
            {
                data.SaveFiles.Add(new SaveFile());
                SaveFileTexts[i].text = SaveFiles[i].FileName;
            }
        }
        this.SaveFiles = new List<SaveFile>(data.SaveFiles);
    }

    public void SaveData(ref GameData data)
    {
        data.SaveFiles = this.SaveFiles;
    }


    public void ExitGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        if (CurrentFile == "" || CurrentFile == "None" || CurrentFile == null)
        {
            NameSetText.text = "No file selected";
        }
        else
        {
            StartCoroutine(LoadingSavefile());
            CurrentFileName.text = "Loading Savefile: " + CurrentFile;
            SceneManager.LoadScene("Game");
        }
    }

    IEnumerator LoadingSavefile()
    {
        DataPersistence.SaveGame();
        yield return new WaitForSeconds(5);
    }
    public void GoToSaveFiles()
    {
        if (mainMenu.activeSelf == false)
        {
            mainMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(false);
        }
    }
    public void BackToMainMenu()
    {
        DataPersistence.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
    }
    private void Start()
    {
        for (int i = 0; i < SaveFiles.Count; i++)
        {
            SaveFiles[i].ID = i;
        }
        SetFile();
        if (CurrentFileName != null)
        {
            CurrentFileName.text = "Current Savefile: " + CurrentFile;
        }
        DataPersistence.UpdateFileName();
        Debug.Log(CurrentFile);
    }

    public void SetNameToFile()
    {
        string inputName = SaveFileInputField.text.Replace(" ", "");
        if (inputName.Length > 0)
        {
            SaveFiles[Index].FileName = inputName;
            SaveFilePanel.SetActive(false);
            NameSetText.text = "Savefile created";
            SaveFiles[Index].isSelected = true;
            SaveFileTexts[Index].text = SaveFiles[Index].FileName;
            SaveFileInputField.text = "";
        }
        else
        {
            NameSetText.text = "No name entered";
        }
    }
    public void CloseDeletionPanel()
    {
        DeletionPanel.SetActive(false);
    }
    public void OpenDeletionPanel()
    {
        DeletionPanel.SetActive(true);
    }

    public void CloseSavefilePanel()
    {
        SaveFilePanel.SetActive(false);
    }

    public void DeletedSavefile()
    {
        SaveFiles[Index].FileName = "";
        SaveFiles[Index].PlayTimeFile = 0;
        SaveFiles[Index].RebirthFile = 0;
        SaveFiles[Index].isSelected = false;
        NameSetText.text = "Savefile deleted";
    }

    public void SetFile()
    {
        for (int i = 0; i < SaveFiles.Count; i++)
        {
            SaveFiles[i].isSelected = false;
            SaveFileButtons[i].onClick.RemoveAllListeners();

            int index = i;
            SaveFileButtons[index].onClick.AddListener(() => 
            {
                SaveFiles[index].isSelected = true;
                Index = SaveFiles[index].ID;
                if (SaveFiles[index].FileName == "" || SaveFiles[index].FileName == null)
                {
                    SaveFilePanel.SetActive(true);
                }
                else
                {
                    CurrentFile = SaveFiles[index].FileName;
                    CurrentFileName.text = "Current Savefile: " + CurrentFile;
                    DataPersistence.UpdateFileName();
                    Debug.Log(CurrentFile);
                }
            });
        }
    }
}


[System.Serializable]
public class SaveFile
{
    public int ID;
    public string FileName;
    public int PlayTimeFile;
    public int RebirthFile;
    public bool isSelected;
}
