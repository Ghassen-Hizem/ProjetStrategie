using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ControlledUnit : ScriptableObject
{
    public string Unitname;
    public int speed;
    public int lifePoints;
    public int nbArmors;
    public int attackPeriod;
    public int capacityPeriod;
    public int degatAttack;


    public abstract void MoveTo(SelectableUnit unit, Vector3 position);
    public abstract void Attack(SelectableUnit unit);

    public abstract void UseCapacity();

    public abstract void TakeDamage(int degats);
}
