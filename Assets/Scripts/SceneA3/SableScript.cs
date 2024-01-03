using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SableScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // SOLVED , REMOVED KINEMATIC BODY
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
