using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Enemy/Magicien")]
public class EnemyMagicien : EnemyUnit
{
    //lui assigner l'animator du prefab EnemySoldat ou le prefab EnemySoldat entier 
    //public Animator anim;
    //private string Attack_Animation = "IsAttacking";
    public GameObject LaserParticules;
    private int attackRadius = 3;

    public override void Attack(scriptEnemy enemyUnit, SelectableUnit unit)
    {

        if (unit & enemyUnit)
        {
            Collider[] colliders = Physics.OverlapSphere(unit.transform.position, attackRadius);
            foreach (Collider collider in colliders)
            {
                //ou alors voir si il a un script TestEnemy
                if (collider.TryGetComponent<SelectableUnit>(out SelectableUnit otherPlayer))
                {
                    if (collider)
                    {
                        otherPlayer.TakeDamage(degatAttack);
                    }

                }
            }

            var particules = Instantiate(LaserParticules, unit.transform.position, LaserParticules.transform.rotation);
            particules.SetActive(true);

        }

    }


    

    public override void TakeDamage(scriptEnemy enemyUnit, int degats)
    {   
        
        
    }
}
