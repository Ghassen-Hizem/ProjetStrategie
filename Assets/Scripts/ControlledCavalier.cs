using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Players/Cavalier")]
public class ControlledCavalier : ControlledUnit
{

    public int attackRadius = 5;
    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }

    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {
        MoveTo(unit, position);
    }
    public override void Attack(SelectableUnit unit, scriptTestEnemy enemyUnit)
    {
        unit.attackElapsedtime = 0;
        //repousse les unit à 1 uu de speed/2
        degatAttack = 4 + speed;
        Debug.Log("cavalier attack");
        /*
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit))
        {
            unit.Agent.speed = speed;
            unit.Agent.SetDestination(Hit.point);

            Debug.Log("magnitude" + unit.Agent.velocity.magnitude);
            //on attaque quand on est entrain de se deplacer
            if (unit.Agent.velocity.magnitude > 0 )
            {
                Collider[] colliders = Physics.OverlapSphere(unit.transform.position, attackRadius);
                foreach (Collider collider in colliders)
                {
                    //ou alors voir si il a un script AIController
                    if (collider.CompareTag("Enemy"))
                    {
                        //GetComponent le script AIController ou autre  :  AIController = collider.GetComponent
                        //AIController.TakeDamage(degatAttack)
                        Debug.Log("enemy damaged");

                        //il ne doit pas marcher vers cette position mais etre poussé
                        //Debug.Log(-collider.transform.forward * speed / 2);
                        collider.transform.position = -collider.transform.forward * speed / 2;
                    }
                    
                }
            }
            
        }*/

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
            if (unit.KingModeActive)
            {
                //GameManager.GameOver
                Debug.Log("gameOver");
            }
            else
            {
                Destroy(unit.gameObject);
            }
        }
    }
}
