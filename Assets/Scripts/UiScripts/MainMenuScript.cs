using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour,IDataPersistence
{
    public GameObject mainMenu;
    public Image Image;
    public List<Sprite> Images;
    public XPScript XPScript;
    public DataPersistence DataPersistence;
    public static string CurrentFile = "SavedGameFiles.Data";

    public List<SaveFile> SaveFiles = new List<SaveFile>();

    public void LoadData(GameData data)
    {
        this.SaveFiles = data.SaveFiles;
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
        if (CurrentFile == "")
        {
            Debug.Log("No file selected");
        }
        else
        {
            SceneManager.LoadScene("Game");
        }
    }
    public void GoToSaveFiles()
    {
        mainMenu.SetActive(false);
    }

    public void Reload()
    {
        DataPersistence.SaveGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DataPersistence.LoadGame();
    }
    public void BackToMainMenu()
    {
        mainMenu.SetActive(true);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(true);
        }
        if (XPScript == null)
        {
            
        }
        else 
        { 
            if (XPScript.Rebirth > 1)
            {
                Image.sprite = Images[2];
            }
            else
            {
                Image.sprite = Images[XPScript.Rebirth];
            }
        }
    }

    public void SetFile()
    {
        for (int i = 0; i < SaveFiles.Count; i++)
        {
            SaveFiles[i].isSelected = false;
            SaveFiles[i].button.onClick.RemoveAllListeners();

            int index = i;
            SaveFiles[index].button.onClick.AddListener(() => 
            { 
                SaveFiles[index].isSelected = true;
                CurrentFile = SaveFiles[index].FileName;
                Debug.Log(CurrentFile);
            });
        }
    }
}


[System.Serializable]
public class SaveFile
{
    public string FileName;
    public int PlayTimeFile;
    public int RebirthFile;
    public bool isSelected;
    public Button button;

    public Text TimePLayedText;
}
