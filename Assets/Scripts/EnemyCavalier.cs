using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Enemy/Cavalier")]
public class EnemyCavalier : EnemyUnit
{
    public int attackRadius = 3;
    private pushRadiusCavalierEnemy pushRadius;
    private float attackPossibleRadius = 10f;
    private int distance;


    public void MoveTo(scriptEnemy enemyUnit, Vector3 position)
    {
        enemyUnit.Agent.stoppingDistance = 1;
        enemyUnit.Agent.speed = speed;
        enemyUnit.Agent.SetDestination(position);
        if (pushRadius)
        {
            pushRadius.gameObject.SetActive(false);
        }


    }

    public override void Attack(scriptEnemy enemyUnit, SelectableUnit unit)
    {

        
        if (unit & enemyUnit)
        {
            pushRadius = enemyUnit.pushRadiusCavalier;
            pushRadius.gameObject.SetActive(true);
            pushRadius.speed = (int)enemyUnit.Agent.speed;

            distance = (int)Vector3.Distance(enemyUnit.transform.position, unit.transform.position);

            if (distance <= attackPossibleRadius)
            {
                MoveTo(enemyUnit, enemyUnit.transform.position + enemyUnit.transform.forward * 10);
                //Debug.Log("move forward");
            }
            else
            {
                //je peux ajouter du forward + position mais pas obligé
                MoveTo(enemyUnit, unit.transform.position);
                //Debug.Log("move to enemy");
                enemyUnit.attackElapsedtime = 0;
            }

        }
        

        if (!enemyUnit)
        {
            pushRadius.gameObject.SetActive(false);
        }

    }


    

    public override void TakeDamage(scriptEnemy enemyUnit, int degats)
    {   
        
        
    }
}
