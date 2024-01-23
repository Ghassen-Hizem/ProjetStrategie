using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPiege : MonoBehaviour
{

    private int degatCapacity = 5;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<scriptEnemy>(out scriptEnemy enemyUnit))
        {
            
            enemyUnit.TakeDamage(degatCapacity);

            Destroy(gameObject);
        }
    }
}
