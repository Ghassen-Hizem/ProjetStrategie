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
        Debug.Log("target on sight");
        if(haveTarget == false) {
            Debug.Log("target confirmed");
            if(other.gameObject.CompareTag("Player")) {
                    haveTarget = true;
                   target = other.gameObject;
            }
          
        }
        if(other.gameObject.GetComponent<HealthManager>().healthAmount<=0) {
            haveTarget = false;
            Destroy(other.gameObject,5);
        }
        
    }
}
