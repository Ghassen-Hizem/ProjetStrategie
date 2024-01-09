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
    private string Run_Animation = "IsRunning";
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

    // Update is called once per frame
    void Update()
    {
        
        IsFighting = EnemyRadius.GetComponent<EnemyRadius>().haveTarget;
        if(IsFighting) {
            Unit = EnemyRadius.GetComponent<EnemyRadius>().target;
            UnitPosition = Unit.transform;
            distance = Vector3.Distance(gameObject.transform.position,UnitPosition.position);
            Attack();
        }
        if(EnemyRadius.GetComponent<EnemyRadius>().haveTarget == false) {
            
            Agent.ResetPath();
             anim.SetBool(Attack_Animation,false);
        }
            
        
    }

    void Attack() {
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
    else {
        enemyReached = false;
        Agent.speed = 5f;
        anim.SetBool(Attack_Animation,false);
        StopCoroutine("giveDamage");
    }
    
}

IEnumerator giveDamage() {
    yield return new WaitForSeconds(2f);
    Unit.GetComponent<HealthManager>().TakeDamage(10);
}
    
}   
