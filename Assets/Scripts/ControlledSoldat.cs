using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Players/Soldat")]
public class ControlledSoldat : ControlledUnit
{
    private string Attack_Animation_Soldat = "IsAttacking";
    private int distance;
    
    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
        unit.anim.SetBool(Attack_Animation_Soldat, false);
    }

    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        if (unit.gameObject)
        {
            unit.Agent.SetDestination(position);
        }
        

    }
    
    public override void Attack(SelectableUnit unit, scriptEnemy enemyUnit)
    {

        distance = (int)Vector3.Distance(enemyUnit.transform.position, unit.transform.position);
        
        if(distance < 2)
        {
            unit.transform.LookAt(enemyUnit.transform);
            unit.anim.SetBool(Attack_Animation_Soldat, true);
            enemyUnit.TakeDamage(degatAttack);
            
        }
        else
        {
            unit.anim.SetBool(Attack_Animation_Soldat, false);
            MoveToAttack(unit, enemyUnit.transform.position);
        }


        if (!enemyUnit)
        {
            unit.anim.SetBool(Attack_Animation_Soldat, false);
        }

        unit.attackElapsedtime = 0;
    }


    public override void UseCapacity(SelectableUnit unit)
    {
       
       
    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {   
        unit.SoldatlifePoints = unit.SoldatlifePoints - degats + nbArmors;

        
    }
}
