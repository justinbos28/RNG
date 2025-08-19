using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour, IDataPersistence
{
    public GameObject mainMenu;
    public XPScript XPScript;
    public DataPersistence DataPersistence;

    [SerializeField] public string MainMenuFile = "SavedGameFiles.Data";
    public static string CurrentFile = "None";
    public static int TotalPLaytime;
    public float TotalTimePlayed = 0;
    public int Index;
    
    public Text CurrentFileName;
    public Text NameSetText;
    public List<Button> SaveFileButtons = new List<Button>();
    public List<Text> SaveFileTexts = new List<Text>();
    public List<Text> SaveFilePlaytime = new List<Text>();
    public List<SaveFile> SaveFiles = new List<SaveFile>();
    public List<Image> SaveFileImages = new List<Image>();

    public GameObject SaveFilePanel;
    public GameObject DeletionPanel;
    public InputField SaveFileInputField;
    public void LoadData(GameData data)
    {
        this.TotalTimePlayed = data.TotalTimePlayed;
        this.Index = data.Index;

        while (data.SaveFiles.Count < this.SaveFiles.Count)
        {
            data.SaveFiles.Add(new SaveFile());
        }
        while (data.SaveFiles.Count > this.SaveFiles.Count)
        {
            data.SaveFiles.RemoveAt(data.SaveFiles.Count - 1);
        }
        this.SaveFiles = new List<SaveFile>(data.SaveFiles);
        LoadAllData();
    }
    public void SaveData(ref GameData data)
    {
        data.TotalTimePlayed = this.TotalTimePlayed;
        data.Index = this.Index;

        data.SaveFiles = this.SaveFiles;
    }

    private void LoadAllData()
    {
        for (int i = 0; i < SaveFiles.Count; i++)
        {
            int index = i;
            if (SaveFiles[index].FileName == "")
            {
                SaveFileTexts[index].text = "SaveFile " + (index + 1);
            }
            else
            {
                SaveFileTexts[index].text = SaveFiles[index].FileName;
            }
            if (SaveFiles[index].isSelected == true)
            {
                CurrentFile = SaveFiles[index].FileName;
                CurrentFileName.text = "Current Savefile: " + CurrentFile;
            }

            if (SaveFiles.Count > 0)
            {
                if (TotalPLaytime > 0)
                {
                    SaveFiles[Index].PlayTimeFile = TotalPLaytime;
                }
                else
                {
                    Debug.Log("playtime is 0");
                }
            }
            else
            {
                Debug.Log("No savefiles found or not in the main menu");
            }
            
            int hours = SaveFiles[i].PlayTimeFile / 3600;
            int minutes = (SaveFiles[i].PlayTimeFile % 3600) / 60;
            int seconds = SaveFiles[i].PlayTimeFile % 60;
            SaveFilePlaytime[i].text = $"Playtime: {hours:D2}:{minutes:D2}:{seconds:D2}";
        }
    }
    public void ExitGame()
    {
        Application.Quit();
        SaveFiles[Index].PlayTimeFile = TotalPLaytime;
    }
    public void PlayGame()
    {
        if (CurrentFile == "" || CurrentFile == "None" || CurrentFile == null)
        {
            NameSetText.text = "No file selected";
        }
        else
        {
            DataPersistence.SaveGame();
            StartCoroutine(LoadingSavefile());
            CurrentFileName.text = "Loading Savefile: " + CurrentFile;
            SceneManager.LoadScene("Game");
        }
    }

    IEnumerator LoadingSavefile()
    {
        yield return new WaitForSeconds(5);
    }
    public void GoToSaveFiles()
    {
        if (mainMenu.activeSelf == false)
        {
            if (CurrentFile == "" || CurrentFile == "None" || CurrentFile == null)
            {
                NameSetText.text = "No file selected";
            }
            else
            {
                mainMenu.SetActive(true);
            }
        }
        else
        {
            mainMenu.SetActive(false);
        }
    }

    public void ExitSavefile()
    {
        mainMenu.SetActive(true);
    }
    public void BackToMainMenu()
    {
        DataPersistence.SaveGame();
        SceneManager.LoadScene("MainMenu");
    }

    public void Update()
    {
        TotalTimePlayed += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackToMainMenu();
        }
        TotalPLaytime = Mathf.FloorToInt(TotalTimePlayed);
        if (CurrentFileName != null)
        {
            if (CurrentFile == "" || CurrentFile == "None" || CurrentFile == null)
            {
                CurrentFileName.text = "Current Savefile: None";
            }
            else
            {
                CurrentFileName.text = "Current Savefile: " + CurrentFile;
            }
        }
    }
    private void Start()
    {
        for (int i = 0; i < SaveFiles.Count; i++)
        {
            SaveFiles[i].ID = i;
        }
        SetFile();
        DataPersistence.UpdateFileName();
        Debug.Log("currentfile = "+CurrentFile);
    }

    public void SetNameToFile()
    {
        string inputName = SaveFileInputField.text.Replace(" ", "");
        if (inputName.Length > 0)
        {
            if (inputName != "None")
            {
                SaveFiles[Index].FileName = SaveFileInputField.text;
                SaveFilePanel.SetActive(false);
                NameSetText.text = "Savefile created";
                SaveFiles[Index].isSelected = true;
                SaveFileTexts[Index].text = SaveFiles[Index].FileName;
                SaveFileInputField.text = "";
                CurrentFile = SaveFiles[Index].FileName;
                DataPersistence.UpdateFileName();
                GoToSaveFiles();
            }
            else
            {
                NameSetText.text = "Name cannot be 'None'";
            }
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
        for (int i = 0; i < SaveFiles.Count; i++)
        {
            if (SaveFiles[i].isSelected == true)
            {
                int index = i;
                SaveFileTexts[index].text = "Savefile " + (index + 1);
            }
        }
        SaveFiles[Index].FileName = "";
        SaveFiles[Index].PlayTimeFile = 0;
        SaveFiles[Index].RebirthFile = 0;
        SaveFiles[Index].isSelected = false;
        SaveFileImages[Index].color = new Vector4(0.8f, 0.8f, 0.8f, 1);
        NameSetText.text = "Savefile deleted";
        CurrentFile = "None";
        CurrentFileName.text = "Current Savefile: " + CurrentFile;
        SaveFilePlaytime[Index].text = "Playtime: " + SaveFiles[Index].PlayTimeFile;
    }

    public void SetFile()
    {
        for (int i = 0; i < SaveFiles.Count; i++)
        {
            SaveFileButtons[i].onClick.RemoveAllListeners();
            SaveFileImages[i].color = new Vector4(0.8f ,0.8f ,0.8f ,1);

            int index = i;
            SaveFileButtons[index].onClick.AddListener(() => 
            {
                SaveFiles[index].isSelected = true;
                SaveFileImages[index].color = Color.green;
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
    public void ResetSelected()
    {
        for (int i = 0; i < SaveFiles.Count; i++)
        {
            SaveFiles[i].isSelected = false;
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
