using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName = "Players/Magicien")]
public class ControlledMagicien : ControlledUnit
{

    private int stopRadius = 10;
    private int attackRadius = 3;
    private int unitDistance;
    //private GameObject particules;
    //private ParticleSystem laserParticles;
    private ParticleSystem particules;
    public int degatCapacity;


    /*
    private void Awake()
    {
        particules = GameObject.FindWithTag("Laser");
        laserParticles = particules.GetComponent<ParticleSystem>();

    }*/

    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }

    public override void Attack(SelectableUnit unit)
    {
            
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit) )
        {

            //Debug.Log("magicien attack");
            unitDistance = (int)Vector3.Distance(Hit.point, unit.transform.position);
            unit.Agent.stoppingDistance = stopRadius;
            if (unitDistance > stopRadius )
            {
                unit.Agent.speed = speed;
                unit.Agent.SetDestination(Hit.point);
                
            }
            else
            {
                unit.attack = true;

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
                    
                    particules = unit.GetComponentInChildren<ParticleSystem>();
                    //particules.gameObject.WorldPositionStays(true);
                    //si on definit un particuleSystem dans la scene et qu'on fait le getComponent dans le awake, il n'y a plus de probleme de laser qui suit le magicien
                    //mais il faut un particulesystem pour chaque magicien (c'est compliqué). sinon on peut fixer la position du particulesystem au real world ou le detacher du local 
                    
                    if (particules.name == "LaserParticles")
                    {
                        particules.transform.position = Hit.point;
                        //particules.transform.localPosition = particules.transform.position;
                        particules.Play();
                    }
                    
                    //animation d'attaque
                }
            }
            //le navmesh s'effectue avec les agents "player". si les ennemis sont des agents de type "player" comme les players, ce raycast ne marche pas toujours
        }
        
    }

    public override void UseCapacity(SelectableUnit unit)
    {
        //instantiate a piege from a prefab in the raycast point 
        //if an enemy triggers the piege's collider, enemy.take damage then destroy the piege
        
        Debug.Log("magicien capacity");
        unit.capacity = true;

        //GetComponent le script AIController ou autre  :  AIController = collider.GetComponent
        //AIController.TakeDamage(degatCapacity)
    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {
        unit.MagicienlifePoints = unit.MagicienlifePoints - degats + nbArmors;

        if (unit.MagicienlifePoints <= 0)
        {
            Destroy(unit.gameObject);
        }
    }

    
    
}
