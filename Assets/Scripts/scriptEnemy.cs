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
    
    public Image healthBar;
    public float healthAmount;

    private Camera mainCam;
    [SerializeField] public Canvas canvaHealthBar;

    private SelectableUnit Unit;
    private Transform UnitPosition;
    private float distance;
    private NavMeshAgent Agent;
    public Animator anim;
    private string Attack_Animation_Soldat = "IsAttacking";

    private bool enemyReached = false;

    public A1EnemyRadius EnemyRadius;

    private bool IsFighting = false;

    private int attackPeriod = 2;
    private float attackElapsedtime = 0;

    private int nbArmors;
    private int speed = 3;

    void Start()
    {
        mainCam = Camera.main;
        
        healthAmount = 50;
        anim = GetComponent<Animator>();
        Agent = gameObject.GetComponent<NavMeshAgent>();
        Agent.speed = speed;
        //EnemyRadius = transform.GetChild(0).gameObject.GetComponent<EnemyRadius>();
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
                    Agent.stoppingDistance = 1;
                    Agent.speed = speed;
                    Agent.SetDestination(UnitPosition.position);
                }
                else
                {
                    Agent.speed = speed;
                    Agent.SetDestination(UnitPosition.position);
                }
                
            }

            if (distance < 1)
            {
                Agent.ResetPath();
                enemyReached = true;
                Agent.speed = 0f;
                //Debug.Log("enemy attacking");

                if (gameObject.CompareTag("Magicien"))
                {
                    enemyMagicien.Attack(this, Unit);
                }
                else if (gameObject.CompareTag("Soldat"))
                {
                    anim.SetBool(Attack_Animation_Soldat, true);
                }

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
                Agent.speed = speed;
                if (gameObject.CompareTag("Soldat"))
                {
                    anim.SetBool(Attack_Animation_Soldat, false);
                }
                
                StopCoroutine("giveDamage");
            }
        }


    }

    IEnumerator giveDamage()
    {
        if (Unit != null)
        {
            yield return new WaitForSeconds(2f);
            
            Unit.TakeDamage(2);
        }

    }

    public void TakeDamage(int damage)
    {
       
        healthAmount -= damage + nbArmors;
        healthBar.fillAmount = healthAmount / 50f;
       
        if (healthAmount <= 0)
        {
            if (gameObject.CompareTag("Soldat"))
            {
                anim.SetBool("IsDead", true);
            }
            
            Destroy(gameObject, 5);

        }
    }


}
