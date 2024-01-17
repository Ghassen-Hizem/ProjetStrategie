using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(menuName = "Players/Cavalier")]
public class ControlledCavalier : ControlledUnit
{

    public int attackRadius = 3;
    public int degats;

    private pushRadiusCavalier pushRadius;
    private float attackPossibleRadius = 10f;
    private int distance;
    
    
    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
        if(pushRadius)
        {
            pushRadius.gameObject.SetActive(false);
        }
        
        
    }

    
    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {

        //unit.Agent.speed = speed;
        //i maybe will need this line if i add capacity

        distance = (int)Vector3.Distance(position, unit.transform.position);

        if (distance <= attackPossibleRadius)
        {
            MoveTo(unit, unit.transform.position + unit.transform.forward * 10); 
            //Debug.Log("move forward");
        }
        else
        {
            //je peux ajouter du forward + position mais pas obligé
            MoveTo(unit, position );
            //Debug.Log("move to enemy");
            unit.attackElapsedtime = 0;
        }

    }

    
    public override void Attack(SelectableUnit unit, scriptEnemy enemyUnit)
    {
        //this attack happens every frame (when enemies collide with pushRadius)
        
        unit.Agent.speed = speed;
        pushRadius = unit.pushRadius;
        pushRadius.gameObject.SetActive(true);
        pushRadius.speed = (int)unit.Agent.speed;

        if (!enemyUnit)
        {
            pushRadius.gameObject.SetActive(false);
        }
        
    }

    public override void UseCapacity(SelectableUnit unit)
    {
        //faire du MoveToAttack
        unit.Agent.speed = 10;
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

        }
        
    }
}
