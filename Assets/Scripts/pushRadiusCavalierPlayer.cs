using System.Collections;

using UnityEngine;


public class pushRadiusCavalierPlayer : MonoBehaviour
{
    public int speed;
    private float pushForce;

    private int degats;
    private SelectableUnit playerUnit;

    private int degatPlayer = 1;
    private int count = 0;


    [SerializeField] private GameObject attackParticules;
    //private Vector3 smoothedPosition;
    //private float tLerp = 0.8f;
    
    private void Start()
    {
        pushForce = speed / 2;
        playerUnit = GetComponentInParent<SelectableUnit>();

    }


    private void OnTriggerEnter(Collider collider)
    {
        if (count == 0)
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


        if (collider.TryGetComponent<scriptEnemy>(out scriptEnemy enemy))
        {
            if (enemy)
            {
                var particules = Instantiate(attackParticules, playerUnit.transform.position, playerUnit.transform.rotation, playerUnit.transform);
                particules.SetActive(true);

                degats = 3 + speed;

                enemy.TakeDamage(degats);

                pushForce = speed / 2;
               
                collider.transform.position += -collider.transform.right * pushForce;

                //var initialPosition = collider.transform.position;
                //var newPosition = collider.transform.position - collider.transform.right * pushForce;
                //smoothedPosition = Vector3.Lerp(initialPosition, newPosition, tLerp);
                //collider.transform.position = smoothedPosition;
                //enemyUnit.TakeDamage(degatEnemy);
                playerUnit.TakeDamage(degatPlayer);
                gameObject.SetActive(false);
            }
        }
        count = 0;

        yield break;
    }


}
