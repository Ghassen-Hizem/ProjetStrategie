
using UnityEngine;



[CreateAssetMenu(menuName = "Players/Magicien")]
public class ControlledMagicien : ControlledUnit
{

    private int attackPossibleRadius = 10;
    private int attackRadius = 3;
    private int unitDistance;
    public GameObject LaserParticules;
    public GameObject Piege;
    public LayerMask layerMaskFloor;
    

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

            unit.transform.LookAt(enemyUnit.transform);
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

            

            unit.attackElapsedtime = 0;
    }


    public override void UseCapacity(SelectableUnit unit)
    {

        //si on appuie loin, on peut pas utiliser la capacité
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit Hit, layerMaskFloor))
        {
            unitDistance = (int)Vector3.Distance(Hit.point, unit.transform.position);
            if (unitDistance <= attackPossibleRadius)
            {
                float mZcoord = Camera.main.WorldToScreenPoint(Hit.transform.position).z;
                Vector3 mousePoint = Input.mousePosition;
                mousePoint.z = mZcoord;

                Vector3 positionPiege = Camera.main.ScreenToWorldPoint(mousePoint);
                
               
                positionPiege.y = 2.5f;
                var piege = Instantiate(Piege, positionPiege, Piege.transform.rotation);
                Debug.Log("piege");
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
