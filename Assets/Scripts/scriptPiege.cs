using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPiege : MonoBehaviour
{

    //private int degatCapacity = 5;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            //AIController = collider.GetComponent<AIController>();
            //AIController.TakeDamage(degatCapacity)

            print("Enemy damaged by piege");

            Destroy(gameObject);
        }
    }
}
