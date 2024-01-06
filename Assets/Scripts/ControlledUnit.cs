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

    //pour le soldat, il attaque l'enemi selectionné tant que l'ennemi a encore de la vie
    //par contre le magicien, le cavalier et le soignant attaquent seulement qd on leur dit

    public abstract void MoveTo(SelectableUnit unit, Vector3 position);
    public abstract void Attack(SelectableUnit unit);

    public abstract void UseCapacity(SelectableUnit unit);

    public abstract void TakeDamage(SelectableUnit unit, int degats);


    
 
 }
