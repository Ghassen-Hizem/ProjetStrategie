
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;



public class InstantiatePlayer : MonoBehaviour
{
    private SelectableUnit[] Units;
    private scriptEnemy[] Enemies;
    public PlayerInput playerInput;

    public GameObject playerSoldat;
    public GameObject playerBouclier;
    public GameObject playerMagicien;
    public GameObject playerCavalier;

    private int NumPlayer;

    private Vector3 playerPosition;
    public Camera mainCam;

    public GameObject StartPanel;
    public dragHandlerUI slot1;
    public dragHandlerUI slot2;
    public dragHandlerUI slot3;
    public dragHandlerUI slot4;

    public TMP_Text textNbUnits;
    public GameObject GamePanel;

    private Transform highlight;

    public GameObject textMaxPlayers;
    
    private RaycastHit raycastHit;
 
    public bool dragging = false;
    public GameTimerAndInfo GameTimer;

    private void Start()
    {
        playerInput.enabled = false;
        Enemies = FindObjectsOfType(typeof(scriptEnemy)) as scriptEnemy[];
        foreach (scriptEnemy enemy in Enemies)
        {
            enemy.enabled = false;
        }
    }
    
    private void Update()
    {
        Units = FindObjectsOfType(typeof(SelectableUnit)) as SelectableUnit[];
        
        textNbUnits.text = Units.Length + "/20 players";

        if ((IsMouseOverUI() != 0) && Input.GetMouseButtonDown(0))
        {
            NumPlayer = IsMouseOverUI();
        }
        

        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit))
        {

            highlight = raycastHit.transform;
            if (highlight.gameObject.CompareTag("StartZone"))
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;

                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }


        }

        if (!slot1.onDragActive && !slot2.onDragActive && !slot3.onDragActive && !slot4.onDragActive)
        {
            dragging = true;
        }
        else
        {
            dragging = false;
        }


        }



    //we can remove a player (destroy it and remove it from the number of selected) (maybe put a trash can if the player is dragged)


    public void InstantiateAPlayer ()
    {
        if (highlight)
        {
            if (Units.Length < 20)
            {
                playerPosition = raycastHit.point;
                playerPosition.y = 2;

                if (NumPlayer == 1)
                {
                    var soldat = Instantiate(playerSoldat, playerPosition, playerSoldat.transform.rotation);
                    soldat.SetActive(true);
                }
                else if (NumPlayer == 2)
                {
                    var soldat = Instantiate(playerBouclier, playerPosition, playerBouclier.transform.rotation);
                    soldat.SetActive(true);
                }
                else if (NumPlayer == 3)
                {
                    var soldat = Instantiate(playerMagicien, playerPosition, playerMagicien.transform.rotation);
                    soldat.SetActive(true);
                }
                else if (NumPlayer == 4)
                {
                    var soldat = Instantiate(playerCavalier, playerPosition, playerCavalier.transform.rotation);
                    soldat.SetActive(true);
                }
                //textMaxPlayers.SetActive(true);
            }
            else
            {
                textMaxPlayers.SetActive(true);
            }
        }

        
    }
    private int IsMouseOverUI()
    {
        RectTransform slot1Transform = slot1.gameObject.transform as RectTransform;
        RectTransform slot2Transform = slot2.gameObject.transform as RectTransform;
        RectTransform slot3Transform = slot3.gameObject.transform as RectTransform;
        RectTransform slot4Transform = slot4.gameObject.transform as RectTransform;

        if (RectTransformUtility.RectangleContainsScreenPoint(slot1Transform, Input.mousePosition))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return 1;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(slot2Transform, Input.mousePosition))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return 2;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(slot3Transform, Input.mousePosition))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return 3;
        }
        else if (RectTransformUtility.RectangleContainsScreenPoint(slot4Transform, Input.mousePosition))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return 4;
        }
        return 0;
    }


    public void StartGame()
    {
        //initializate timer
        GameTimer.Seconds = 0;
        GameTimer.Minutes = 0;

        if (Units.Length != 0)
        {
            StartPanel.SetActive(false);
            GamePanel.SetActive(true);
            playerInput.enabled = true;

            foreach (scriptEnemy enemy in Enemies)
            {
                enemy.enabled = true;
            }

            enabled = false;
        }
        
        
    }

   
}
