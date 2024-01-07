using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flag : MonoBehaviour
{
   selectUnit playerUnit;

    private void OnTriggerEnter(Collider collider)
    {
        playerUnit = collider.GetComponent<selectUnit>();
        if (playerUnit)
        {
            playerUnit.KingMode();
            Destroy(gameObject);
        }
        
    }
}
