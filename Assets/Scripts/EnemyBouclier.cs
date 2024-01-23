
using UnityEngine;


[CreateAssetMenu(menuName = "Enemy/Bouclier")]
public class EnemyBouclier : EnemyUnit
{

    private pushRadiusBouclierEnemy pushRadius;
    public void MoveToAttack(scriptEnemy enemyUnit, Vector3 position)
    {
        enemyUnit.Agent.stoppingDistance = 1;
        enemyUnit.Agent.speed = speed;
        enemyUnit.Agent.SetDestination(position);
    }


    public override void Attack(scriptEnemy enemyUnit, SelectableUnit unit)
    {
        //MoveToAttack(enemyUnit, unit.transform.position - unit.transform.forward * 1.3f);
        MoveToAttack(enemyUnit, unit.transform.position);
        pushRadius = enemyUnit.pushRadiusBouclier;
        pushRadius.gameObject.SetActive(true);
        

    }


    

    public override void TakeDamage(scriptEnemy enemyUnit, int degats)
    {   
        
        
    }
}
