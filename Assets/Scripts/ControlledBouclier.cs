
using UnityEngine;


[CreateAssetMenu(menuName = "Players/Bouclier")]
public class ControlledBouclier : ControlledUnit
{
    private pushRadiusBouclierPlayer pushRadius;
    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
        if (pushRadius)
        {
            pushRadius.gameObject.SetActive(false);
        }
    }

    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }

    public override void Attack(SelectableUnit unit, scriptEnemy enemyUnit)
    {

        MoveToAttack(unit, enemyUnit.transform.position - enemyUnit.transform.forward * 2);
        pushRadius = unit.pushRadiusBouclier;
        pushRadius.gameObject.SetActive(true);

        


        unit.attackElapsedtime = 0;
    }

   

    public override void UseCapacity(SelectableUnit unit)
    {
        

    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {   
        unit.BouclierlifePoints = unit.BouclierlifePoints - degats + nbArmors;

        
    }
}
