using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class A1EnemyRadius : MonoBehaviour
{
    [HideInInspector]
    public bool haveTarget = false;
    [HideInInspector]
    //public GameObject target;
    public SelectableUnit target;

    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) {


        if (other.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
        {
            haveTarget = true;
            target = unit;
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
                if (unit.MagicienlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }
            else if (unit.CompareTag("Soldat"))
            {
                if (unit.MagicienlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }
            else if (unit.CompareTag("Tirailleur"))
            {
                if (unit.MagicienlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }
            else if (unit.CompareTag("Bouclier"))
            {
                if (unit.MagicienlifePoints <= 0)
                {
                    haveTarget = false;
                }
            }
            else if (unit.CompareTag("Soignant"))
            {
                if (unit.MagicienlifePoints <= 0)
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
