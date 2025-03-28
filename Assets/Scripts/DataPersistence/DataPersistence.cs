using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;

public class DataPersistence : MonoBehaviour
{
    [Header("File Storage Config")]
    private string fileName;
    private string currentFile;
    private GameData gameData;
    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;
    public MainMenuScript MainMenuScript;
    public static DataPersistence Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("more then 1 data persistence in the scene");
        }
        Instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            Debug.Log("main menu");
            currentFile = MainMenuScript.MainMenuFile;
        }
        else
        {
            Debug.Log("game");
            currentFile = MainMenuScript.CurrentFile;
        }
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, currentFile);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void UpdateFileName()
    {
       fileName = MainMenuScript.CurrentFile;
    }
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();

        if (this.gameData == null)
        {
            Debug.Log("no data was found");
            NewGame();
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    private void DeleteSavefile(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("File deleted successfully.");
            MainMenuScript.DeletedSavefile();
        }
        else
        {
            Debug.LogWarning("File not found.");
        }
    }

    // New method to call DeleteSavefile with a predefined file path
    public void DeleteCurrentSavefile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        DeleteSavefile(filePath);
    }
}
