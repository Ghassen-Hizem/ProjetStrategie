using System.Collections;

using UnityEngine;


public class pushRadiusCavalierEnemy : MonoBehaviour
{
    public int speed;
    private float pushForce;

    private int degats;
    private scriptEnemy enemyUnit;

    private int count = 0;
    
    [SerializeField] private GameObject attackParticules;
    //private int degatEnemy = 1;


    private void Start()
    {
        pushForce = speed / 2;
        enemyUnit = GetComponentInParent<scriptEnemy>();

    }

    private void Update()
    {  
        if (enemyUnit.Agent.velocity.magnitude < 5)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(count == 0)
        {
            StartCoroutine(triggerOne(collider));
        }
        count += 1;
    }
    private void OnTriggerExit(Collider collider)
    {
        count = 0;
    }

    private IEnumerator triggerOne(Collider collider)
    {
        
        if (collider.TryGetComponent<SelectableUnit>(out SelectableUnit player))
        {
            if (player)
            {
                var particules = Instantiate(attackParticules, enemyUnit.transform.position, enemyUnit.transform.rotation, enemyUnit.transform);
                particules.SetActive(true);

                degats = 3 + 7;
  
                player.TakeDamage(degats);              

                pushForce = 7 / 2;
                //collider.transform.position += -collider.transform.forward * pushForce;

                collider.transform.position += -collider.transform.right * pushForce;

                //var initialPosition = collider.transform.position;
                //var newPosition = collider.transform.position - collider.transform.right * pushForce;
                //smoothedPosition = Vector3.Lerp(initialPosition, newPosition, tLerp);
                //collider.transform.position = smoothedPosition;
                //enemyUnit.TakeDamage(degatEnemy);
            }
        }
        count = 0;
        
        yield break;
    }
    
    
}
