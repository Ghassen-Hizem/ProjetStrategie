using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Players/Tirailleur")]
public class ControlledTirailleur : ControlledUnit
{
    //public GameObject javelot;
    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }

    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 10;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }
    
    public override void Attack(SelectableUnit unit, scriptEnemy enemyUnit)
    {
        
        
        
        Debug.Log("Tirailleur attack");

        enemyUnit.TakeDamage(degatAttack);



        unit.attackElapsedtime = 0;
    }


    public override void UseCapacity(SelectableUnit unit)
    {
       

    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {   
        unit.TirailleurlifePoints = unit.TirailleurlifePoints - degats + nbArmors;

    }
}
