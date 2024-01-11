using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class scriptTestEnemy : MonoBehaviour
{
    
    private int lifePoints = 10;
    public void TakeDamage(int degats)
    {
        lifePoints = lifePoints - degats ;

        if (lifePoints <= 0)
        {
            
            Destroy(gameObject);
            //or just deactivate the object

            Debug.Log("enemy damaged");

        }
    }
    
    
    
}
