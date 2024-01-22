
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

    [SerializeField]
    private GameObject GamePanel;

    public GameObject GameDescriptionText;
    private string CurrentTimer;
    public TMP_Text Timertext;

    //(sarra) utiliser cette variable dans le scriptEnemy pour qu'ils changent de comportement lorsque KingMode == true 
    [HideInInspector] public bool KingMode = false;


    private SelectableUnit[] Units;
    private scriptEnemy[] Enemies;
   

    public InstantiatePlayer scriptInstantiate;
    public GameObject VictoryZoneDepart;   //assigner ca dans l'inspecteur avec l'objet de la zone de depart


    void Update()
    {
        
        Units = FindObjectsOfType(typeof(SelectableUnit)) as SelectableUnit[];
        Enemies = FindObjectsOfType(typeof(scriptEnemy)) as scriptEnemy[];

        if (Units.Length == 0) {
            GameOver();
            
        }


     if(Enemies.Length == 0) {
            Victory();
            
        }
     /*
        if (scriptInstantiate.enabled == false)
        {
            VictoryZoneDepart.SetActive(true);
        }*/
        





        //ghassen work //

        //implementation de victoire en cas de capture et de deposition du drapeau en zone de depart:
        //ajouter gameObject vide avec un box collider de la taille de la zone de depart
        //ajouter un script à cet objet pour appeler la fonction victory de ce script:  
        //OnTriggerEnter()
        // collider = tryGetComponent<SelectableUnit>(out SelectableUnit)
        //if (SelectableUnit.kingModeActive == true) {gameManagerA3.Victory()}

        //l'object doit etre desactivé dans la scene.



    }

    public void GameOver()
    {
        if (scriptInstantiate.enabled == false)
        {
            CurrentTimer = Timertext.text;
            
            
            gameOverPanel.SetActive(true);
            GameDescriptionText.GetComponent<TextMeshProUGUI>().text = "Game Time: " + CurrentTimer + "\n" + "Units Lost: " + Convert.ToString(scriptInstantiate.UnitsNbr - Units.Length) + "\n" + "Remaining enemies: " + Convert.ToString(Enemies.Length) + "\n";
            GamePanel.SetActive(false);
            Time.timeScale = 0;
        }
        
    }

    public void Victory()
    {
        youWinPanel.SetActive(true);
        GamePanel.SetActive(false);
        Time.timeScale = 0;
    }

    public void RestartGame() {
        
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame() {
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}
