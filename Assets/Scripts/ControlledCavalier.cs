using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(menuName = "Players/Cavalier")]
public class ControlledCavalier : ControlledUnit
{

    //public gameManagerA3 gameManager;

    public int attackRadius = 3;
    public int degats;
    //private Vector3 decalage = new Vector3(0,0,-1);
    //private Vector3 decalage;
    
    private pushRadiusCavalier pushRadius;
    private float attackPossibleRadius = 5f;
    private int distance;
    
    //private float pushForce = 30f;

    
    void Start ()
    {
        //gameManager = FindObjectOfType(typeof(gameManagerA3)) as gameManagerA3;
        //Debug.Log("game manager"+ gameManager);
        
        
    }
    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
        //pushCollider.gameObject.SetActive(false);
    }

    
    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {
  
        distance = (int)Vector3.Distance(position, unit.transform.position);
        //Debug.Log("distance " + distance);

        if (distance <= attackPossibleRadius)
        {
            MoveTo(unit, unit.transform.position + unit.transform.forward * 15); //why not enemyPosition + transform.forward, this obliges it to pass through the enemy and attack it
            Debug.Log("move forward");
            //dont wait 
        }
        else
        {
            MoveTo(unit, position );
            Debug.Log("move to enemy");
            unit.moveToAttackElapsedTime = 0;
            //wait until the enemy is reached
        }

    }

    
    public override void Attack(SelectableUnit unit, scriptEnemy enemyUnit)
    {
        
        //when attack is possible (distance< range), we should move to enemy + forward then elapsedTime = 0 
        //Also, if we are too close to enemy, we should move forward then  re do first point

        unit.Agent.speed = speed;
        degats = 4 + (int)unit.Agent.speed;
        //Debug.Log("cavalier attack");
        pushRadius = unit.pushRadius;
        pushRadius.gameObject.SetActive(true);
        pushRadius.speed = (int)unit.Agent.speed;

        unit.attackElapsedtime = 0;
        //the attack happens when we enter the attackPossibleRadius of selectableUnit. the attack should happen, the cavalier should not run away from the enemy

        if (!enemyUnit)
        {
            pushRadius.gameObject.SetActive(false);
        }
        //go to enemy position + transform.forward (but this is already done in MoveToAttack)
    }

    public override void UseCapacity(SelectableUnit unit)
    {
        //faire du MoveToAttack
        unit.Agent.speed = 10;
        degats = 4 + (int)unit.Agent.speed;
        pushRadius.speed = (int)unit.Agent.speed;
        Debug.Log("cavalier capacity");

        //s'arrete que lorsqu'on attaque
        if (unit.Agent.speed == speed)
        {
            unit.capacityElapsedtime = 0;
        }
        
    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {
        if (unit)
        {
            unit.CavalierlifePoints = unit.CavalierlifePoints - degats + nbArmors;

            /*
            if (unit.CavalierlifePoints <= 0)
            {
                if (unit.KingModeActive)
                {
                    //GameManager.GameOver
                    Debug.Log("gameOver");
                    gameManager.GameOver();
                    Destroy(unit.gameObject);
                    //gameManager.GameOver();
                }
                else
                {

                    Destroy(unit.gameObject);
                }
            }*/
        }
        
    }
}
