using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Enemy/Soldat")]
public class EnemySoldat : EnemyUnit
{
    //lui assigner l'animator du prefab EnemySoldat ou le prefab EnemySoldat entier 
    public Animator anim;
    //private string Attack_Animation = "IsAttacking";

    public override void Attack(scriptEnemy enemyUnit, SelectableUnit unit)
    {
        
        
        //animation ??
    }


    

    public override void TakeDamage(scriptEnemy enemyUnit, int degats)
    {   
        
        
    }
}
