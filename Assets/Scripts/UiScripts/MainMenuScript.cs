using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public Image Image;
    public List<Sprite> Images;
    public XPScript XPScript;
    public DataPersistence DataPersistence;

    public void ExitGame()
    {
        Application.Quit();
    }
    public void PlayGame()
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
