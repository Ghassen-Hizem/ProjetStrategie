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
    public ParticleSystem laserParticles;

    private void Awake()
    {
        laserParticles = FindObjectOfType<ParticleSystem>();
    }

    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }

    public override void Attack(SelectableUnit unit)
    {
        // the fct depends on the unit, le rayon de l'attaque, on peut s'arreter au rayon des q'on a l'ennemi visé dans notre sphere(collider)
        

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit) )
        {

            Debug.Log("magicien attack");
            unitDistance = (int)Vector3.Distance(Hit.point, unit.transform.position);
            unit.Agent.stoppingDistance = stopRadius;
            if (unitDistance > stopRadius )
            {
                unit.Agent.speed = speed;
                unit.Agent.SetDestination(Hit.point);
            }

            if (unitDistance <= stopRadius)
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
                    //animation d'attaque
                    //rayon laser de particules
                    
                    if (laserParticles.name == "LaserParticles")
                    {
                        Debug.Log("name" + laserParticles.name);
                        laserParticles.transform.position = Hit.point;
                        laserParticles.Play();
                    }
                        
                }         

            }

            //le navmesh s'effectue avec les agents "player". si les ennemis sont des agents de type "player" comme les players, ce raycast ne marche pas toujours
        }
        
    }

    public override void UseCapacity()
    {
        Debug.Log("magicien capacity");
    }

    public override void TakeDamage(int degats)
    {
        lifePoints -= degats - nbArmors;
    }
}
