using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class scriptTestEnemy : MonoBehaviour
{

    public Image healthBar;
    public float healthAmount;

    private Animator anim;

    private Camera mainCam;
    [SerializeField] public Canvas canvaHealthBar;

    private SelectableUnit Unit;
    private Transform UnitPosition;
    private float distance;
    private NavMeshAgent Agent;
    private string Attack_Animation = "IsAttacking";
    
    private bool enemyReached = false;

    public A1EnemyRadius EnemyRadius;

    private bool IsFighting = false;

    private int attackPeriod = 2;
    private float attackElapsedtime = 0;

    private int nbArmors;

    void Start()
    {
        mainCam = Camera.main;
        anim = gameObject.GetComponent<Animator>();
        healthAmount = 50;

        Agent = gameObject.GetComponent<NavMeshAgent>();
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
                //enemy is attacking every frame. there should be an attack period
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
            anim.SetBool(Attack_Animation, false);
        }


    }

    void Attack()
    {
        if (Unit != null)
        {
            if (enemyReached == false)
            {
                Agent.SetDestination(UnitPosition.position);
            }

            if (distance < 1)
            {
                Agent.ResetPath();
                enemyReached = true;
                Agent.speed = 0f;
                Debug.Log("enemy attacking");
                attackElapsedtime = 0;
                anim.SetBool(Attack_Animation, true);
                StartCoroutine("giveDamage");



            }
            else if (Unit == null)
            {
                StopCoroutine("giveDamage");
            }
            else
            {
                enemyReached = false;
                Agent.speed = 5f;
                anim.SetBool(Attack_Animation, false);
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
            anim.SetBool("IsDead", true);
            Destroy(gameObject, 5);

        }
    }


}
