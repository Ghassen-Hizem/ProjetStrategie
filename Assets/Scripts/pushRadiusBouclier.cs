using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class pushRadiusBouclier : MonoBehaviour
{
    //private float timeElapsed = 0;
    private float pushForce = 2;
    //private SelectableUnit unit;
    //private int attackRadius = 5;
    private void Start()
    {
        //timeElapsed = 0;
        //unit = GetComponentInParent<SelectableUnit>();
    }
    private void Update()
    {
        

        /*
        if (unit.Agent.velocity.magnitude ==0)
        {
            gameObject.SetActive(false);
        }*/
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<scriptEnemy>(out scriptEnemy enemy))
        {
            if (enemy)
            {
                
                collider.transform.position += -collider.transform.forward * pushForce;

                //var initialPosition = collider.transform.position;
                //var newPosition = collider.transform.position - collider.transform.right * pushForce;
                //smoothedPosition = Vector3.Lerp(initialPosition, newPosition, tLerp);
                //collider.transform.position = smoothedPosition;
            }
        }
        

    }

    


}
