
using UnityEngine;


[CreateAssetMenu(menuName = "Players/Cavalier")]
public class ControlledCavalier : ControlledUnit
{

    public int attackRadius = 3;
    //private int capacityRadius = 10;

    private pushRadiusCavalierPlayer pushRadius;
    private float attackPossibleRadius = 5f;
    private int distance;
    //private scriptEnemy enemyUnit;

 
    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
        if(pushRadius)
        {
            pushRadius.gameObject.SetActive(false);
        }

        
    }

    
    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {

        //unit.Agent.speed = speed;
        

        distance = (int)Vector3.Distance(position, unit.transform.position);

        if (distance <= attackPossibleRadius)
        {
            MoveTo(unit, unit.transform.position + unit.transform.forward * 7); 
            //Debug.Log("move forward");
        }
        else
        {
            //je peux ajouter du forward + position mais pas obligé
            MoveTo(unit, position );
            //Debug.Log("move to enemy");
            unit.attackElapsedtime = 0;
        }
        
    }

    
    public override void Attack(SelectableUnit unit, scriptEnemy enemyUnit)
    {
        //this attack happens every frame (when enemies collide with pushRadius)

        
        unit.capacityElapsedtime = 0;

        unit.Agent.speed = speed;
        pushRadius = unit.pushRadiusCavalier;
        pushRadius.gameObject.SetActive(true);
        pushRadius.speed = (int)unit.Agent.speed;

        if (!enemyUnit)
        {
            pushRadius.gameObject.SetActive(false);
        }
        
    }

    public override void UseCapacity(SelectableUnit unit)
    {
         /*
        while (true)
        {
            Collider[] colliders = Physics.OverlapSphere(unit.transform.position, capacityRadius);
            List<scriptEnemy> enemies = new List<scriptEnemy>();

            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent<scriptEnemy>(out scriptEnemy otherEnemy))
                {
                    enemies.Add(otherEnemy);
                }
            }
            var sortedEnemies = enemies.OrderBy(otherEnemy => Vector3.Distance(otherEnemy.transform.position, unit.transform.position));
            if (sortedEnemies.Count() != 0)
            {
                enemyUnit = sortedEnemies.FirstOrDefault();
            }
            Debug.Log(enemyUnit);

            if (enemyUnit)
            {
                unit.Agent.speed = 10;
                MoveToAttack(unit, enemyUnit.transform.position);
                Debug.Log("cavalier capacity");
                if (pushRadius)
                {
                    pushRadius.speed = (int)unit.Agent.speed;
                }
                else
                {
                    pushRadius.gameObject.SetActive(true);
                    pushRadius.speed = (int)unit.Agent.speed;
                }
            }
            else
            {
                break;
            }

           if(Input.GetKeyUp(KeyCode.Mouse1))
            {
                break;
            }


        }*/        
        
    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {
        if (unit)
        {
            unit.CavalierlifePoints = unit.CavalierlifePoints - degats + nbArmors;

        }
        
    }
}
