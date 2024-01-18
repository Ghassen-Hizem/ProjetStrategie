using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Players/Soldat")]
public class ControlledSoldat : ControlledUnit
{
   
    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }

    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {
        MoveTo(unit, position);
    }
    
    public override void Attack(SelectableUnit unit, scriptEnemy enemyUnit)
    {
        
        enemyUnit.TakeDamage(degatAttack);
        Debug.Log("soldat attack");

        unit.transform.LookAt(enemyUnit.transform);

        //the player should face the enemy: (rotation.lookAt)  ?
        //add attack animation
        //IMPORTANT
  
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
