using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour
{
    [HideInInspector]
    public bool haveTarget = false;
    [HideInInspector]
    public GameObject target;
  
    void Start()
    {
        
    }

 
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) {
        
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
            Destroy(other.gameObject,5);
        }
        
    }
}
