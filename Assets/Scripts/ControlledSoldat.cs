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

    public override void Attack(SelectableUnit unit, scriptTestEnemy enemyUnit)
    {
       
        // ghassen work //

        
        enemyUnit.TakeDamage(degatAttack);
        Debug.Log("soldat attack");

        //the player should face the enemy: (rotation.lookAt)
        //attack animation

        //end ghassen work//



        unit.attackElapsedtime = 0;
    }


    public override void UseCapacity(SelectableUnit unit)
    {
        // ghassen work //
        //end ghassen work//
    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {   
        unit.SoldatlifePoints = unit.SoldatlifePoints - degats + nbArmors;

        if (unit.SoldatlifePoints <= 0)
        {
            if (unit.KingModeActive)
            {
                //GameManager.GameOver
                Debug.Log("gameOver");
            }
            else
            {
                Destroy(unit.gameObject);
                //or just deactivate the object
            }
        }
    }
}
