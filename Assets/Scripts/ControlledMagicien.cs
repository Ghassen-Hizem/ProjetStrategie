using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Players/Magicien")]
public class ControlledMagicien : ControlledUnit
{

    public int stopRadius = 10;
    private int unitDistance;
    [SerializeField] private LayerMask floorLayers;

    
    public override void Attack(SelectableUnit unit)
    {
        // the fct depends on the unit, le rayon de l'attaque, on peut s'arreter au rayon des q'on a l'ennemi visé dans notre sphere(collider)
        

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, floorLayers ) )
        {
            unitDistance = (int)Vector3.Distance(Hit.point, unit.transform.position);
            unit.Agent.stoppingDistance = stopRadius;
            if (unitDistance > stopRadius )
            {
                unit.MoveTo(Hit.point);
                
            }

            //voir les ennemis dans le rayon et leurs faire des degats
            //le navmesh s'effectue avec les agents "player". si les ennemis sont des agents de type "player" comme les players, ce raycast ne marche pas toujours
            //il doit s'arreter lorsque il arrive a un rayon précis (rayon d'arret) different du rayon de l'attaque
            //le bouton d'attaque et de mvt est le meme ???
            //unit.Agent.stoppingDistance
            Debug.Log("magicien attack");
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
