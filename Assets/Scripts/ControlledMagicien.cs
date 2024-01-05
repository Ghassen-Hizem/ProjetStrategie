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
    //float elapsedtime = 0;
    //private int attackDecount;

    /*
    private void Awake()
    {
        attackDecount = attackPeriod;
    }*/
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

    public override bool Attack(SelectableUnit unit)
    {
        // the fct depends on the unit, le rayon de l'attaque, on peut s'arreter au rayon des q'on a l'ennemi visé dans notre sphere(collider)
        
        
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
                    //cette fct initialise instantanément attackperiod à 0 alors qu'elle doit etre egale à 3 pour fonctionner ??
                    //attackPeriod = 0;
                    //AttackPeriodTimer();

                    //animation d'attaque
                }
            }
            //le navmesh s'effectue avec les agents "player". si les ennemis sont des agents de type "player" comme les players, ce raycast ne marche pas toujours
        }

        return true;
        
    }

    public override void UseCapacity()

    {
        Debug.Log("magicien capacity");
    }

    public override void TakeDamage(int degats)
    {
        lifePoints -= degats - nbArmors;
    }

    
    public override void AttackPeriodTimer()
    {
        /*
        while (attackPeriod != 3)
        {
            elapsedtime += Time.deltaTime;
            int seconds = ((int)MathF.Floor(elapsedtime % 60));
            attackPeriod += seconds;
            Debug.Log("attackPeriod " + attackPeriod);
            
        }


        //cette partie bloque le jeu
        if (attackPeriod == 0)
        {
            Debug.Log("value " + attackPeriod);
            attackPeriod = 3;
            elapsedtime = 0;
        }
    */

    }
}
