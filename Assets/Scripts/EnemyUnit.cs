using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyUnit : ScriptableObject
{
    
    public int speed;
    public int lifePoints;
    public int nbArmors;
    public int attackPeriod;
    public int degatAttack;
    


    public abstract void Attack(scriptEnemy enemyUnit, SelectableUnit unit);

    
    public abstract void TakeDamage(scriptEnemy enemyUnit, int degats);


    
 
 }
