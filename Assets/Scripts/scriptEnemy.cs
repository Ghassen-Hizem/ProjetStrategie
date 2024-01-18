using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class scriptEnemy : MonoBehaviour
{

    public EnemyUnit enemyMagicien;
    public EnemyUnit enemyCavalier;
    public EnemyUnit enemyBouclier;

    public Image healthBar;
    public float healthAmountSoldat = 50;
    
    public float healthAmount;
    public float maxHealth;

    private Camera mainCam;
    [SerializeField] public Canvas canvaHealthBar;

    private SelectableUnit Unit;
    private Transform UnitPosition;
    private float distance;
    public NavMeshAgent Agent;
    public Animator anim;
    private string Attack_Animation_Soldat = "IsAttacking";

    private bool enemyReached = false;

    public A1EnemyRadius EnemyRadius;

    private bool IsFighting = false;

    //attackPeriod should change for each unit
    private int attackPeriod = 2;
    public float attackElapsedtime = 0;

    private int nbArmors;
    private int nbArmorsSoldat = 1;
    private int speedSoldat = 3;
    private int degatAttack;
    private int degatAttackSoldat = 2;

    public pushRadiusCavalierEnemy pushRadiusCavalier;

    void Start()
    {
        mainCam = Camera.main;
        
        //healthAmount = 50;
        
        //anim = GetComponent<Animator>();
        //Agent = gameObject.GetComponent<NavMeshAgent>();
        //Agent.speed = speed;
        //EnemyRadius = transform.GetChild(0).gameObject.GetComponent<EnemyRadius>();

        if (gameObject.CompareTag("Magicien"))
        {
            healthAmount = enemyMagicien.lifePoints;
            nbArmors = enemyMagicien.nbArmors;
            maxHealth = healthAmount;
            degatAttack = enemyMagicien.degatAttack;
        }
        if (gameObject.CompareTag("Soldat"))
        {
            healthAmount = healthAmountSoldat;
            nbArmors = nbArmorsSoldat;
            maxHealth = healthAmount;
            degatAttack = degatAttackSoldat;
        }
        if (gameObject.CompareTag("Bouclier"))
        {
            healthAmount = enemyBouclier.lifePoints;
            nbArmors = enemyBouclier.nbArmors;
            maxHealth = healthAmount;
            degatAttack = enemyBouclier.degatAttack;
        }
        if (gameObject.CompareTag("Cavalier"))
        {
            healthAmount = enemyCavalier.lifePoints;
            nbArmors = enemyCavalier.nbArmors;
            maxHealth = healthAmount;
            degatAttack = enemyCavalier.degatAttack;
        }
    }

    void Update()
    {

        canvaHealthBar.transform.rotation = Quaternion.LookRotation(healthBar.transform.position - mainCam.transform.position);
        attackElapsedtime += Time.deltaTime;

        IsFighting = EnemyRadius.haveTarget;
        if (IsFighting)
        {
            if (EnemyRadius.target)
            {
                Unit = EnemyRadius.target;
                UnitPosition = Unit.transform;
                distance = Vector3.Distance(gameObject.transform.position, UnitPosition.position);
                
                if (attackElapsedtime >= attackPeriod)
                {
                    Attack();
                    print("enemy attacking");
                    
                }
            }
            else
            {
                EnemyRadius.haveTarget = false;
            }

        }
        if (EnemyRadius.haveTarget == false)
        {
            Agent.ResetPath();
            if (gameObject.CompareTag("Soldat"))
            {
                anim.SetBool(Attack_Animation_Soldat, false);
            }
            
        }


    }

    void Attack()
    {
        if (Unit != null)
        {
            if (enemyReached == false)
            {
                if (gameObject.CompareTag("Magicien"))
                {
                    //should be 5 at least
                    //Agent.stoppingDistance = 1;
                    Agent.speed = enemyMagicien.speed;
                    Agent.SetDestination(UnitPosition.position);
                }
                else if (gameObject.CompareTag("Cavalier"))
                {
                    Agent.speed = enemyCavalier.speed;
                    Agent.SetDestination(UnitPosition.position);
                }
                else if (gameObject.CompareTag("Soldat"))
                {
                    Agent.speed = speedSoldat;
                    Agent.SetDestination(UnitPosition.position);
                }

            }

            if (gameObject.CompareTag("Soldat"))
            {
                if (distance < 1)
                {
                    Agent.ResetPath();
                    enemyReached = true;
                    Agent.speed = 0f;
                    //Debug.Log("enemy attacking");

                    anim.SetBool(Attack_Animation_Soldat, true);
                    
                    attackElapsedtime = 0;
                    StartCoroutine("giveDamage");

                }
                else if (Unit == null)
                {
                    StopCoroutine("giveDamage");
                }
                else
                {
                    enemyReached = false;
                    //Agent.speed = speed;
                   
                    anim.SetBool(Attack_Animation_Soldat, false);
                    
                    StopCoroutine("giveDamage");
                }

            }
            else
            {
                if (distance < 10)
                {
                    Agent.ResetPath();
                    enemyReached = true;
                    Agent.speed = 0f;
                    //Debug.Log("enemy attacking");

                    if (gameObject.CompareTag("Magicien"))
                    {
                        //il doit attaque depuis une longue distance
                        enemyMagicien.Attack(this, Unit);
                    }
                    else if (gameObject.CompareTag("Cavalier"))
                    {
                        enemyCavalier.Attack(this, Unit);
                    }
                    //attackElapsedtime = 0;
                    StartCoroutine("giveDamage");

                }
                else if (Unit == null)
                {
                    StopCoroutine("giveDamage");
                }
                else
                {
                    enemyReached = false;
                    //Agent.speed = speed;
                    StopCoroutine("giveDamage");
                }
            }
            
        }


    }

    IEnumerator giveDamage()
    {
        if (Unit != null)
        {
            yield return new WaitForSeconds(2f);
            
            if (Unit)
            {
                //the damage changes for each unit
                Unit.TakeDamage(degatAttack);
            }
            
        }

    }

    public void TakeDamage(int damage)
    {

        
        healthAmount -= damage + nbArmors;
        healthBar.fillAmount = healthAmount / maxHealth;
       
        if (healthAmount <= 0)
        {
            Agent.speed = 0;
            //stop the moveToAttack
            if (gameObject.CompareTag("Soldat"))
            {
                anim.SetBool("IsDead", true);
            }
            
            Destroy(gameObject, 5);

        }
    }


}
