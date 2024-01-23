using System.Collections;

using UnityEngine;


public class pushRadiusBouclierPlayer : MonoBehaviour
{
    //private float timeElapsed = 0;
    private float pushForce = 2;
    private SelectableUnit unit;
    //private int attackRadius = 5;
    private void Start()
    {
        //timeElapsed = 0;
        unit = GetComponentInParent<SelectableUnit>();
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
                //unit.Agent.SetDestination(enemy.transform.position);
                collider.transform.position += -collider.transform.forward * pushForce;
                
                StartCoroutine(pushOnce());
                
                //var initialPosition = collider.transform.position;
                //var newPosition = collider.transform.position - collider.transform.right * pushForce;
                //smoothedPosition = Vector3.Lerp(initialPosition, newPosition, tLerp);
                //collider.transform.position = smoothedPosition;
            }
        }
        

    }

    private IEnumerator pushOnce()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        yield break;
    }


}
