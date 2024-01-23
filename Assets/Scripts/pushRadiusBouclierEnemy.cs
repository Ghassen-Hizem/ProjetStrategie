using System.Collections;

using UnityEngine;


public class pushRadiusBouclierEnemy : MonoBehaviour
{
    //private float timeElapsed = 0;
    private float pushForce = 2;
    private scriptEnemy enemyUnit;
    //private int attackRadius = 5;
    private void Start()
    {
        //timeElapsed = 0;
        enemyUnit = GetComponentInParent<scriptEnemy>();
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
        if (collider.TryGetComponent<SelectableUnit>(out SelectableUnit player))
        {
            if (player)
            {
                
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
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        enemyUnit.attackElapsedtime = 0;
        yield break;
    }


}
