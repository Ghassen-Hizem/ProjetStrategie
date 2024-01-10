using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Players/Magicien")]
public class ControlledMagicien : ControlledUnit
{

    private int attackPossibleRadius = 15;
    private int attackRadius = 3;
    private int unitDistance;
    public GameObject LaserParticules;
    public GameObject Piege;

    

    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }


    public override void Attack(SelectableUnit unit, scriptTestEnemy enemyUnit)
    {

        Debug.Log("magicien attack");

              
        Collider[] colliders = Physics.OverlapSphere(enemyUnit.transform.position, attackRadius);
        foreach (Collider collider in colliders)
        {
            //ou alors voir si il a un script TestEnemy
            if (collider.CompareTag("Enemy"))
                {
                    //GetComponent le script AIController ou autre  :  AIController = collider.GetComponent
                    //AIController.TakeDamage(degatAttack)
                    Debug.Log("enemy damaged");
                }
        }

        var particules = Instantiate(LaserParticules, enemyUnit.transform.position, LaserParticules.transform.rotation);
        particules.SetActive(true);

        //animation d'attaque

        unit.attackElapsedtime = 0;

    }



    /*
    public override void Attack(SelectableUnit unit)
    {

        Debug.Log("magicien attack");
        unit.attackElapsedtime = 0;
        
        //mon navmesh correspond aux agents "player". si les ennemis sont des agents de type "player" comme les players, ce raycast ne marche pas toujours

        //si on attaque loin, le magicien va marcher et s'arreter a une stopping distance de 10 mais n'attaque pas. par contre, cela consomme une periode d'attaque
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit) )
        {
            //faire sortir le moveTo de l'attaque pour ne pas qu'elle consomme une periode d'attaque. (c'est compliqué) 
            unitDistance = (int)Vector3.Distance(Hit.point, unit.transform.position);
            unit.Agent.stoppingDistance = attackPossibleRadius;
            if (unitDistance > attackPossibleRadius )
            {
                unit.Agent.speed = speed;
                unit.Agent.SetDestination(Hit.point);
                
            }
            else
            {
                Collider[] colliders = Physics.OverlapSphere(Hit.point, attackRadius);
                foreach (Collider collider in colliders)
                {
                    //ou alors voir si il a un script AIController
                    if (collider.CompareTag("Enemy"))
                    {
                        //GetComponent le script AIController ou autre  :  AIController = collider.GetComponent
                        //AIController.TakeDamage(degatAttack)
                        Debug.Log("enemy damaged");
                    }
                }

                var particules = Instantiate(LaserParticules, Hit.transform.position, LaserParticules.transform.rotation);
                particules.SetActive(true);

                //animation d'attaque

                unit.attackElapsedtime = 0;
            }
            
        }
        
    }*/

    public override void UseCapacity(SelectableUnit unit)
    {

        //si on appuie loin, on peut pas utiliser la capacité
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit))
        {
            unitDistance = (int)Vector3.Distance(Hit.point, unit.transform.position);
            if (unitDistance <= attackPossibleRadius)
            {
                Debug.Log("magicien capacity");

                Vector3 positionPiege = Hit.transform.position;
                positionPiege.y = 2.5f;
                var piege = Instantiate(Piege, positionPiege, Hit.transform.rotation);
                piege.SetActive(true);
                unit.capacityElapsedtime = 0;
            }
        }
    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {
        
        unit.MagicienlifePoints = unit.MagicienlifePoints - degats + nbArmors;

        if (unit.MagicienlifePoints <= 0)
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
