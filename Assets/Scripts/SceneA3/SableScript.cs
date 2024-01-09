using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SableScript : MonoBehaviour
{

    void Start()
    {
         
    }


    void Update()
    {
        
    }

    
    private void OnTriggerEnter(Collider other) {
       
        if(other.CompareTag("Player")) {
            
             other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 2f;
            
        }
    }
    private void OnTriggerExit(Collider other) {
       
        if(other.CompareTag("Player")) {
            
             other.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().speed = 10f;
            
        }
    }
}
