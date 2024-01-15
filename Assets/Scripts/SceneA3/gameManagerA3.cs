using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class gameManagerA3 : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField]
    private GameObject youWinPanel;

    public GameObject GameDescriptionText;


    

    private GameObject[] Units;
    private GameObject[] Enemies;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Units = GameObject.FindGameObjectsWithTag("Player");
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(Units.Length == 0) {
            gameOverPanel.SetActive(true);
            GameDescriptionText.GetComponent<TextMeshProUGUI>().text  = "Temps du jeu : Timer_variable\n" + "Nombre d'unités perdu : " + Convert.ToString(20-Units.Length) +"\n" + "Nombre d'enemies restants: "+ Convert.ToString(Enemies.Length) +"\n";
        }

        //implemntation de defaite en cas de mort du roi 


     if(Enemies.Length == 0) {
            youWinPanel.SetActive(true);
        }

        //implementation de victoire en cas de capture et de deposition du drapeau en zone de depart

        

    }

    public void RestartGame() {
        SceneManager.LoadScene("A3Scene");
    }

    public void ExitGame() {
        SceneManager.LoadScene("MenuScene");
    }
}
