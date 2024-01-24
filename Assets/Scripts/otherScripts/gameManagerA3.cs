
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections;


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
    public TMP_Text VictoryTimer;
    
    [HideInInspector] public bool KingMode = false;


    private SelectableUnit[] Units;
    private scriptEnemy[] Enemies;
   

    public InstantiatePlayer scriptInstantiate;
    public GameObject VictoryZoneDepart;
    private int count = 0;

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
     
        if (scriptInstantiate.enabled == false)
        {
            VictoryZoneDepart.SetActive(true);
        }
        
        //codeForDemo
        //kill all Soldat enemies
        if (Input.GetKey(KeyCode.K))
        {
            foreach (scriptEnemy enemy in Enemies)
            {
                if(enemy.gameObject.CompareTag("Soldat") || enemy.gameObject.CompareTag("Bouclier"))
                {
                    Destroy(enemy.gameObject);
                }
                
            }
        }

    }

    public void GameOver()
    {
        if (scriptInstantiate.enabled == false)
        {
            //print("gameOver");
            //CurrentTimer = Timertext.text;
            if (count == 0)
            {
                StartCoroutine(TimerCoroutine());
            }
            count += 1;

            gameOverPanel.SetActive(true);
            GameDescriptionText.GetComponent<TextMeshProUGUI>().text = "Game Time: " + CurrentTimer + "\n" + "Units Lost: " + Convert.ToString(scriptInstantiate.UnitsNbr - Units.Length) + "\n" + "Remaining enemies: " + Convert.ToString(Enemies.Length) + "\n";
            GamePanel.SetActive(false);
            //Time.timeScale = 0;
        }
        
    }

    private IEnumerator TimerCoroutine()
    {

        CurrentTimer = Timertext.text;

        yield break;
    }


    public void Victory()
    {
        if (count == 0)
        {
            StartCoroutine(TimerCoroutine());
        }
        count += 1;

        VictoryTimer.text = "Game Time: " + CurrentTimer;
        

        youWinPanel.SetActive(true);
        GamePanel.SetActive(false);
        //Time.timeScale = 0;
    }

    public void RestartGame() {
        
        gameOverPanel.SetActive(false);
        //Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }

    public void ExitGame() {
        gameOverPanel.SetActive(false);
        //Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}
