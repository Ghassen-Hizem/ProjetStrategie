using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class A1EnemyRadius : MonoBehaviour
{
    [HideInInspector]
    public bool haveTarget = false;
    [HideInInspector]
    //public GameObject target;
    public SelectableUnit target;

    //private List<SelectableUnit> Units;
    //private HashSet<SelectableUnit> Units = new HashSet<SelectableUnit>();
    private scriptEnemy enemyUnit;

    private void Start()
    {
        //Units = new List<SelectableUnit>();
        enemyUnit = GetComponentInParent<scriptEnemy>();
         

}

    public void OnTriggerStay(Collider other) {

       
        HashSet<SelectableUnit> Units = new HashSet<SelectableUnit>();
        if (other.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
        {
            
            if (unit)
            {
                
                Units.Add(unit);
                //le rendre en set
                //supprimer les elements du set qui sont null
                var sortedUnits = Units.OrderBy(unit => Vector3.Distance(unit.transform.position, enemyUnit.transform.position));
                if (sortedUnits.Count() != 0)
                {
                    target = sortedUnits.FirstOrDefault();

                }
            }
            

            //print(Units);
            if (target)
            {
                haveTarget = true;
            }
            else
            {
                haveTarget = false;
                Units.Remove(target);
            }
            
            //target = unit;
            
            //target = other.gameObject;

            if (unit.CompareTag("Magicien"))
            {
                if (unit.MagicienlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }
            else if (unit.CompareTag("Cavalier"))
            {
                if (unit.CavalierlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }
            else if (unit.CompareTag("Soldat"))
            {
                if (unit.SoldatlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }
            else if (unit.CompareTag("Tirailleur"))
            {
                if (unit.TirailleurlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }
            else if (unit.CompareTag("Bouclier"))
            {
                if (unit.BouclierlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }
            else if (unit.CompareTag("Soignant"))
            {
                if (unit.SoignantlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }

        }

        /*
        if(haveTarget == false) {
            
            if(other.gameObject.CompareTag("Player")) {
                    haveTarget = true;
                    target = other.gameObject;
            }

            if (other.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
            {
                haveTarget = true;
                target = other.gameObject;
            }

        }
        if(other.gameObject.GetComponent<HealthManager>().healthAmount<=0) {
            Debug.Log("enemy is dead");
            haveTarget = false;
            //Destroy(other.gameObject,5);
        }*/

    }
}
