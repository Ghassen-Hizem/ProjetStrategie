using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptFlag : MonoBehaviour
{
    SelectableUnit playerUnit;

    private void OnTriggerEnter(Collider collider)
    {
        playerUnit = collider.GetComponent<SelectableUnit>();
        if (playerUnit)
        {
            playerUnit.KingMode();
            Destroy(gameObject);
        }
        
    }
}
