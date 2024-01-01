using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Players/Cavalier")]
public class ControlledCavalier : ControlledUnit
{
    public override void Attack(SelectableUnit unit)
    {
        // the fct depends on the unit, le rayon de l'attaque, on peut s'arreter au rayon des q'on a l'ennemi visé dans notre sphere(collider)
        Debug.Log("cavalier attack");
    }

    public override void UseCapacity()
    {
        Debug.Log("cavalier capacity");
    }

    public override void TakeDamage(int degats)
    {
        lifePoints -= degats - nbArmors;
    }
}
