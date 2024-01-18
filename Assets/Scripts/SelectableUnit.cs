using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using UnityEngine.UI;


[RequireComponent(typeof(NavMeshAgent))]
public class SelectableUnit : MonoBehaviour
{

    public gameManagerA3 gameManager;

    [HideInInspector] public NavMeshAgent Agent;
    [SerializeField] private SpriteRenderer SelectionSprite;
    [SerializeField] public Image healthBar;
    [SerializeField] public Canvas canvaHealthBar;

    public ControlledUnit controlledMagicien;
    public ControlledUnit controlledCavalier;
    public ControlledUnit controlledSoldat;
    public ControlledUnit controlledBouclier;
    public ControlledUnit controlledTirailleur;
    public ControlledUnit controlledSoignant;

    [HideInInspector] public float attackElapsedtime = 0;
    [HideInInspector] public float capacityElapsedtime = 0;
    

    //jai defini des lifepoints dans ce script car la valeur des lifepoints depend de chaque instance d'unité
    [HideInInspector] public int MagicienlifePoints;
    [HideInInspector] public int CavalierlifePoints;
    [HideInInspector] public int SoldatlifePoints;
    [HideInInspector] public int TirailleurlifePoints;
    [HideInInspector] public int BouclierlifePoints;
    [HideInInspector] public int SoignantlifePoints;

    [HideInInspector] public bool KingModeActive = false;

    public GameObject Flag;
    public GameObject KingParticles;

    [HideInInspector] public Vector3 unitPosition;
    [HideInInspector] public Vector3 enemyPosition;
    

    [HideInInspector] public int unitDistance;
    [HideInInspector] public bool attack;
    private int attackPossibleRadius = 10;

    private Camera mainCam;
    public pushRadiusCavalierPlayer pushRadiusCavalier;
    public pushRadiusBouclier pushRadiusBouclier;



    //au debut du jeu, le chargement d'attaque et de capacité sont vides
    //si on veut qu'ils soient "rechargés" dès le debut, il faut initialiser le attackElaspsedTime avec la attackPeriod, mais chaque unité a une valeur differente, il faut donc 6 variables
    //attackElapsedtime = controlledMagicien.attackPeriod;

    private void Awake()
    {
        gameManager = FindObjectOfType(typeof(gameManagerA3)) as gameManagerA3;

        SelectionManager.Instance.AvailableUnits.Add(this);
        Agent = GetComponent<NavMeshAgent>();
        MagicienlifePoints = controlledMagicien.lifePoints;
        CavalierlifePoints = controlledCavalier.lifePoints;
        SoldatlifePoints = controlledSoldat.lifePoints;
        BouclierlifePoints = controlledBouclier.lifePoints;
        TirailleurlifePoints = controlledTirailleur.lifePoints;
        SoignantlifePoints = controlledSoignant.lifePoints;
        mainCam = Camera.main;
    }

    private void Update()
    {
        canvaHealthBar.transform.rotation = Quaternion.LookRotation(healthBar.transform.position - mainCam.transform.position);
        attackElapsedtime += Time.deltaTime;
        capacityElapsedtime += Time.deltaTime;
        
        unitPosition = Agent.transform.position;


        if (!gameObject)
        {
            StopAllCoroutines();
        }
        /*
        if (KingModeActive && !this)
        {
            gameManager.GameOver();
        }*/
    }
    

    public void MoveTo(Vector3 Position)
    {
        
        StopAllCoroutines();
        
        if (Agent.CompareTag("Magicien"))
        {
            controlledMagicien.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Cavalier"))
        {
            controlledCavalier.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Soldat"))
        {
            controlledSoldat.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Tirailleur"))
        {
            controlledTirailleur.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Bouclier"))
        {
            controlledBouclier.MoveTo(this, Position);
        }
        else if (Agent.CompareTag("Soignant"))
        {
            controlledSoignant.MoveTo(this, Position);
        }
    }

    
    public IEnumerator HandleAttack(scriptEnemy enemyUnit)
    {
  
        while (enemyUnit != null)
        {
            enemyPosition = enemyUnit.transform.position;
            
            unitDistance = (int)Vector3.Distance(enemyPosition, unitPosition);
            attack = unitDistance <= attackPossibleRadius;

            if (!attack)
            {

                StartCoroutine(MoveToAttack(enemyUnit));
                
                yield return new WaitUntil(() => attack == true);

                if (this)
                {
                    Attack(enemyUnit);
                    yield return null;

                    List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(transform.position, attackPossibleRadius));
                    List<scriptEnemy> enemies = new List<scriptEnemy>();
                    foreach (Collider collider in colliders)
                    {
                        if (collider.TryGetComponent<scriptEnemy>(out scriptEnemy otherEnemy))
                        {
                            enemies.Add(otherEnemy);
                        }
                    }
                    var sortedEnemies = enemies.OrderBy(otherEnemy => Vector3.Distance(otherEnemy.transform.position, unitPosition));
                    if (sortedEnemies.Count() != 0)
                    {
                        enemyUnit = sortedEnemies.FirstOrDefault();
                    }
                    else
                    {
                        StopCoroutine(MoveToAttack(enemyUnit));
                        /*
                        if (pushRadiusBouclier)
                        {
                            pushRadiusBouclier.gameObject.SetActive(false);
                        }*/
                    }
                }
                
            }
            else
            {
                if (this)
                {
                    if (CompareTag("Cavalier"))
                    {
                        StartCoroutine(MoveToAttack(enemyUnit));

                        yield return new WaitUntil(() => attack == true);
                    }

                    Attack(enemyUnit);


                    yield return null;

                    List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(transform.position, attackPossibleRadius));
                    List<scriptEnemy> enemies = new List<scriptEnemy>();
                    foreach (Collider collider in colliders)
                    {
                        if (collider.TryGetComponent<scriptEnemy>(out scriptEnemy otherEnemy))
                        {
                            enemies.Add(otherEnemy);
                        }
                    }
                    var sortedEnemies = enemies.OrderBy(otherEnemy => Vector3.Distance(otherEnemy.transform.position, unitPosition));
                    if (sortedEnemies.Count() != 0)
                    {
                        enemyUnit = sortedEnemies.FirstOrDefault();
                    }
                    else
                    {
                        StopCoroutine(MoveToAttack(enemyUnit));
                    }

                }
                
            }

        }
        yield break;

    }

    

    public IEnumerator MoveToAttack(scriptEnemy enemyToAttack)
    {
        //Vector3 fixedEnemyPosition = enemyToAttack.transform.position;

        while (true)
        {
            if (enemyToAttack && this)
            {
                enemyPosition = enemyToAttack.transform.position;

                unitDistance = (int)Vector3.Distance(enemyPosition, unitPosition);
                attack = unitDistance <= attackPossibleRadius;

                if (Agent.CompareTag("Magicien"))
                {
                    controlledMagicien.MoveToAttack(this, enemyPosition);
                }
                else if (Agent.CompareTag("Cavalier"))
                {
                    //if we wait too long, the enemies will reach us and we need to do move forward again and again
                    //if we dont wait enough, it will execute the move to enemy as soon as they are not in the range
                    //it should be low because the cavalier should move forward as soon as he moves to enemy and attacks
                    if (attackElapsedtime >= controlledCavalier.attackPeriod)
                    {
                        controlledCavalier.MoveToAttack(this, enemyPosition);
                    }
                }
                else if (Agent.CompareTag("Soldat"))
                {
                    controlledSoldat.MoveToAttack(this, enemyPosition);
                }
                else if (Agent.CompareTag("Tirailleur"))
                {
                    controlledTirailleur.MoveToAttack(this, enemyPosition);
                }
                else if (Agent.CompareTag("Bouclier"))
                {
                    controlledBouclier.MoveToAttack(this, enemyPosition);
                }
                else if (Agent.CompareTag("Soignant"))
                {
                    controlledSoignant.MoveToAttack(this, enemyPosition);
                }
                    yield return null;
            }
            else
            {
                yield break;
            }
        } 
    }


    public void Attack(scriptEnemy enemyUnit)
    {

        if (KingModeActive == false)
        {
            if (Agent.CompareTag("Magicien"))
            {
                if (attackElapsedtime >= controlledMagicien.attackPeriod)
                {
                    controlledMagicien.Attack(this, enemyUnit);
                }
            }
            else if (Agent.CompareTag("Cavalier"))
            {
                controlledCavalier.Attack(this, enemyUnit);
            }
            else if (Agent.CompareTag("Soldat"))
            {
                if (attackElapsedtime >= controlledSoldat.attackPeriod)
                {
                    controlledSoldat.Attack(this, enemyUnit);
                }
            }
            else if (Agent.CompareTag("Tirailleur"))
            {
                if (attackElapsedtime >= controlledTirailleur.attackPeriod)
                {
                    controlledTirailleur.Attack(this, enemyUnit);
                }
            }
            else if (Agent.CompareTag("Bouclier"))
            {
                if (attackElapsedtime >= controlledBouclier.attackPeriod)
                {
                    controlledBouclier.Attack(this, enemyUnit);
                }
            }
            else if (Agent.CompareTag("Soignant"))
            {
                if (attackElapsedtime >= controlledSoignant.attackPeriod)
                {
                    controlledSoignant.Attack(this, enemyUnit);
                }
            }
        }
    }

    public void OnSelected()
    {
        SelectionSprite.gameObject.SetActive(true);
    }

    public void OnDeselected()
    {
        SelectionSprite.gameObject.SetActive(false);
    }   

    public void UseCapacity()
    {

        if (KingModeActive == false)
        {
            if (Agent.CompareTag("Magicien"))
            {
                if (capacityElapsedtime >= controlledMagicien.capacityPeriod)
                {
                    controlledMagicien.UseCapacity(this);
                }
            }
            else if (Agent.CompareTag("Cavalier"))
            {
                if (capacityElapsedtime >= controlledCavalier.capacityPeriod)
                {
                    controlledCavalier.UseCapacity(this);
                }
            }
            else if (Agent.CompareTag("Soldat"))
            {
                if (capacityElapsedtime >= controlledSoldat.capacityPeriod)
                {
                    controlledSoldat.UseCapacity(this);
                }
            }
            else if (Agent.CompareTag("Tirailleur"))
            {
                if (capacityElapsedtime >= controlledTirailleur.capacityPeriod)
                {
                    controlledTirailleur.UseCapacity(this);
                }
            }
            else if (Agent.CompareTag("Bouclier"))
            {
                if (capacityElapsedtime >= controlledBouclier.capacityPeriod)
                {
                    controlledBouclier.UseCapacity(this);
                }
            }
            else if (Agent.CompareTag("Soignant"))
            {
                if (capacityElapsedtime >= controlledSoignant.capacityPeriod)
                {
                    controlledSoignant.UseCapacity(this);
                }
            }
        }
    }

    public void TakeDamage(int degats)
    {
        if(this)
        {
            if (Agent.CompareTag("Magicien"))
            {
                controlledMagicien.TakeDamage(this, degats);
                healthBar.fillAmount = (float)MagicienlifePoints / controlledMagicien.lifePoints;
                if (MagicienlifePoints <= 0)
                {
                    if (KingModeActive)
                    {  
                        gameManager.GameOver();
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else if (Agent.CompareTag("Cavalier"))
            {
                controlledCavalier.TakeDamage(this, degats);
                healthBar.fillAmount = (float)CavalierlifePoints / controlledCavalier.lifePoints;
                if (CavalierlifePoints <= 0)
                {
                    if (KingModeActive)
                    {
                        gameManager.GameOver();
                        Destroy(gameObject);   
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else if (Agent.CompareTag("Soldat"))
            {
                controlledSoldat.TakeDamage(this, degats);
                healthBar.fillAmount = (float)SoldatlifePoints / controlledSoldat.lifePoints;
                if (SoldatlifePoints <= 0)
                {
                    if (KingModeActive)
                    {
                        gameManager.GameOver();
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else if (Agent.CompareTag("Tirailleur"))
            {
                controlledTirailleur.TakeDamage(this, degats);
                healthBar.fillAmount = (float)TirailleurlifePoints / controlledTirailleur.lifePoints;
                if (TirailleurlifePoints <= 0)
                {
                    if (KingModeActive)
                    {
                        gameManager.GameOver();
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else if (Agent.CompareTag("Bouclier"))
            {
                controlledBouclier.TakeDamage(this, degats);
                healthBar.fillAmount = (float)BouclierlifePoints / controlledBouclier.lifePoints;
                if (BouclierlifePoints <= 0)
                {
                    if (KingModeActive)
                    {
                        gameManager.GameOver();
                        Destroy(gameObject);
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                }
            }
            else if (Agent.CompareTag("Soignant"))
            {
                controlledSoignant.TakeDamage(this, degats);
                healthBar.fillAmount = (float)SoignantlifePoints / controlledSoignant.lifePoints;
            }
        }
        
    }

    public void KingMode()
    {
        gameManager.KingMode = true;
        KingModeActive = true;
        print("KingMode");

        var positionFlag = transform.position;
        positionFlag.y = 4;
        var flag = Instantiate(Flag, positionFlag, Flag.transform.rotation, gameObject.transform);
        flag.SetActive(true);

        var kingParticles = Instantiate(KingParticles, transform.position, KingParticles.transform.rotation, gameObject.transform);
        kingParticles.SetActive(true);

        //le flag il apparait bizarrement avec le soldatPlayer. il est tres haut dans l'axe y. il ne faut pas montrer ça dans la demo.
    }
}
