
using UnityEngine;


[CreateAssetMenu(menuName = "Players/Magicien")]
public class ControlledMagicien : ControlledUnit
{

    private int attackPossibleRadius = 10;
    private int attackRadius = 3;
    private int unitDistance;
    public GameObject LaserParticules;
    public GameObject Piege;

    

    public override void MoveTo(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 1;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }

    public override void MoveToAttack(SelectableUnit unit, Vector3 position)
    {
        unit.Agent.stoppingDistance = 10;
        unit.Agent.speed = speed;
        unit.Agent.SetDestination(position);
    }
    
    public override void Attack(SelectableUnit unit, scriptEnemy enemyUnit)
    {
        
            Collider[] colliders = Physics.OverlapSphere(enemyUnit.transform.position, attackRadius);
            foreach (Collider collider in colliders)
            {
                
                if (collider.TryGetComponent<scriptEnemy>(out scriptEnemy otherEnemy))
                {
                    otherEnemy.TakeDamage(degatAttack);
                }
            }

            var particules = Instantiate(LaserParticules, enemyUnit.transform.position, LaserParticules.transform.rotation);
            particules.SetActive(true);

            //animation d'attaque

            unit.attackElapsedtime = 0;
    }


    public override void UseCapacity(SelectableUnit unit)
    {

        //si on appuie loin, on peut pas utiliser la capacité
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit))
        {
            unitDistance = (int)Vector3.Distance(Hit.point, unit.transform.position);
            if (unitDistance <= attackPossibleRadius)
            {
                Vector3 positionPiege = Hit.transform.position;
                positionPiege.y = 2.5f;
                var piege = Instantiate(Piege, positionPiege, Hit.transform.rotation);
                piege.SetActive(true);
                unit.capacityElapsedtime = 0;
            }
        }
    }

    public override void TakeDamage(SelectableUnit unit, int degats)
    {   
        
        if (unit)
        {
            unit.MagicienlifePoints = unit.MagicienlifePoints - degats + nbArmors;
        }
   
    }
}
