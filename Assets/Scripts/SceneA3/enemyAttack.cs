using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAttack : MonoBehaviour
{
    private GameObject Unit;
    private Transform UnitPosition;
    private float distance;
    private NavMeshAgent Agent;
    private string Attack_Animation = "IsAttacking";
    private Animator anim;
    private bool enemyReached = false;

    private GameObject EnemyRadius;

    private bool IsFighting = false;
    void Start()
    {
        Agent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        EnemyRadius = gameObject.transform.GetChild(0).gameObject;

    }

   
    void Update()
    {
        
        IsFighting = EnemyRadius.GetComponent<EnemyRadius>().haveTarget;
        if(IsFighting) {
            if(EnemyRadius.GetComponent<EnemyRadius>().target) {
                     Unit = EnemyRadius.GetComponent<EnemyRadius>().target;
                    UnitPosition = Unit.transform;
                                distance = Vector3.Distance(gameObject.transform.position,UnitPosition.position);
            Attack();

            }
            else {
                EnemyRadius.GetComponent<EnemyRadius>().haveTarget = false;
                
            }
           
        }
        if(EnemyRadius.GetComponent<EnemyRadius>().haveTarget == false) {
            
            Agent.ResetPath();
             anim.SetBool(Attack_Animation,false);
        }
            
        
    }

    void Attack() {
        if(Unit !=null) {
if(enemyReached == false) {
         Agent.SetDestination(UnitPosition.position);
        }

           if(distance < 1) {
            Agent.ResetPath();
            enemyReached = true;
            Agent.speed = 0f;
            Debug.Log("enemy attacking");
            anim.SetBool(Attack_Animation,true);
            StartCoroutine("giveDamage");       

        
    }
    else if (Unit == null) {
                StopCoroutine("giveDamage");
    }
    else {
        enemyReached = false;
        Agent.speed = 5f;
        anim.SetBool(Attack_Animation,false);
        StopCoroutine("giveDamage");
    }
        }
        
    
}

IEnumerator giveDamage() {
    if(Unit !=null) {
    yield return new WaitForSeconds(2f);
    Unit.GetComponent<HealthManager>().TakeDamage(10);
    }
    
}
    
}   
