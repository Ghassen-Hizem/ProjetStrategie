using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolcanoScript : MonoBehaviour
{
    public GameObject health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
       
        if(other.CompareTag("Player")) {
             health = GameObject.Find("HealthManager");
             health.GetComponent<HealthManager>().TakeDamage(4);
            
        }
    }
}
