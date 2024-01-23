
using UnityEngine;


[CreateAssetMenu(menuName = "Players/Soldat")]
public class ControlledSoldat : ControlledUnit
{
    private string Attack_Animation_Soldat = "IsAttacking";
    private int distance;
    
    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
        unit.anim.SetBool(Attack_Animation_Soldat, false);
    }

    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {
        unit.anim.SetBool(Attack_Animation_Soldat, false);
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        if (unit.gameObject)
        {
            unit.Agent.SetDestination(position);
        }
        

    }
    
    public override void Attack(SelectableUnit unit, scriptEnemy enemyUnit)
    {


        unit.transform.LookAt(enemyUnit.transform);
        unit.anim.SetBool(Attack_Animation_Soldat, true);
        enemyUnit.TakeDamage(degatAttack);
        unit.attackElapsedtime = 0;

 
    }


    public override void UseCapacity(SelectableUnit unit)
    {
       
       
    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {   
        unit.SoldatlifePoints = unit.SoldatlifePoints - degats + nbArmors;
        

    }
}
