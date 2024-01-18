using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoScript : MonoBehaviour
{
    public GameObject health;
   
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
       
        //i deleted healthManager script. you can use unit.TakeDamage
        //but we said we will destroy the object if it touches lava
        
        /*
        if(other.CompareTag("Player")) {
             health = GameObject.Find("HealthManager");
             health.GetComponent<HealthManager>().TakeDamage(4);
            
        }*/
    }
}
