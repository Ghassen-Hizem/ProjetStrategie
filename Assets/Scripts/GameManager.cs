using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    
    private int idArena;
    [SerializeField] private GameObject canvaArenaChoice;
    [SerializeField] private GameObject canvaMainMenu;
    [SerializeField] private GameObject canvaOptions;
    public void ArenaChoice(Button button)
    {
        if (button.name == "Arena1")
        {
            idArena = 1;
        }
        
        if (button.name == "Arena2")
        {
            idArena = 2;
        }
        
    }
    
    public void playButton()
    {
        canvaArenaChoice.SetActive(true);
        canvaMainMenu.SetActive(false);
    }
    public void startGame()
    {
        SceneManager.LoadSceneAsync(idArena);
    }

    public void GameOptions()
    {
        canvaOptions.SetActive(true);
        canvaMainMenu.SetActive(false);
    }

    public void ReturnToMenu()
    {
        canvaMainMenu.SetActive(true);
        canvaOptions.SetActive(false);
        canvaArenaChoice.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
