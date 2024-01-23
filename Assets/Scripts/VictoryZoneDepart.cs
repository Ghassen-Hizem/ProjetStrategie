using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZoneDepart : MonoBehaviour
{

    public gameManagerA3 gameManager;
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.TryGetComponent<SelectableUnit>(out SelectableUnit unit))
        {
            if (unit.KingModeActive == true)
            {
                gameManager.Victory();
            }
        }

    }
}
