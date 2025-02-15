using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public void ExitGame()
    {
        Application.Quit();
    }
    public void PlayGame()
    {
        mainMenu.SetActive(false);
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
    }
}
