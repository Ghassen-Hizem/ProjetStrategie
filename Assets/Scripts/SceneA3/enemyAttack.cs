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
    void Start()
    {
        Unit = GameObject.Find("SwordWarrior");
        UnitPosition = Unit.transform;
        Agent = gameObject.GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(gameObject.transform.position,UnitPosition.position);
        if(distance < 10) {
            Attack();
        }
    }

    void Attack() {
         Agent.SetDestination(UnitPosition.position);
         anim.SetBool(Run_Animation,true);
           if(distance < 1) {
           
            Debug.Log("enemy attacking");
            anim.SetBool(Run_Animation,false);
            anim.SetBool(Attack_Animation,true);
            Unit.GetComponent<HealthManager>().TakeDamage(10);

        
    }
    else {
        anim.SetBool(Attack_Animation,false);
        anim.SetBool(Run_Animation,true);
    }
    
}
    
}   
