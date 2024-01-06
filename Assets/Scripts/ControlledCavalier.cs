using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Players/Cavalier")]
public class ControlledCavalier : ControlledUnit
{

    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }
    public override void Attack(SelectableUnit unit)
    {
        
        Debug.Log("cavalier attack");

    }

    public override void UseCapacity(SelectableUnit unit)
    {
        Debug.Log("cavalier capacity");
    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {
        unit.CavalierlifePoints = unit.CavalierlifePoints - degats + nbArmors;

        if (unit.CavalierlifePoints <= 0)
        {
            Destroy(unit.gameObject);
        }
    }

    
}
