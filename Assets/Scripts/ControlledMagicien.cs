using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Players/Magicien")]
public class ControlledMagicien : ControlledUnit
{
    
    
    public override void Attack()
    {
        // the fct depends on the unit, le rayon de l'attaque, on peut s'arreter au rayon des q'on a l'ennemi visé dans notre sphere(collider)
        Debug.Log("magicien attack");
    }

    public override void UseCapacity()
    {
        Debug.Log("magicien capacity");
    }
}
