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


    public abstract void Attack();

    public abstract void UseCapacity();
}