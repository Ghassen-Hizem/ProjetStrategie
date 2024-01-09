using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRadius : MonoBehaviour
{
    [HideInInspector]
    public bool haveTarget = false;
    [HideInInspector]
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) {
        Debug.Log("target on sight");
        if(haveTarget == false) {
            Debug.Log("target on sight");
            haveTarget = true;
            target = other.gameObject;
        }
        if(other.gameObject.GetComponent<HealthManager>().healthAmount<=0) {
            haveTarget = false;
            Destroy(other.gameObject,5);
        }
        
    }
}
