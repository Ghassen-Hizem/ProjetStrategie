using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class pushRadiusCavalierEnemy : MonoBehaviour
{
    public int speed;
    private float pushForce;

    private int degats;
    private scriptEnemy enemyUnit;
    
    
    [SerializeField] private GameObject attackParticules;
    //private Vector3 smoothedPosition;
    //private float tLerp = 0.8f;
    
    private void Start()
    {
        pushForce = speed / 2;
        enemyUnit = GetComponentInParent<scriptEnemy>();

    }

    private void Update()
    {
        /*
        if (enemyUnit.Agent.velocity.magnitude < 5)
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

                var particules = Instantiate(attackParticules, enemyUnit.transform.position, enemyUnit.transform.rotation, enemyUnit.transform);
                particules.SetActive(true);

                degats = 3 + speed;
                player.TakeDamage(degats);

                pushForce = speed / 2;
                //collider.transform.position += -collider.transform.forward * pushForce;

                collider.transform.position += -collider.transform.right * pushForce;

                //var initialPosition = collider.transform.position;
                //var newPosition = collider.transform.position - collider.transform.right * pushForce;
                //smoothedPosition = Vector3.Lerp(initialPosition, newPosition, tLerp);
                //collider.transform.position = smoothedPosition;
            }
        }

    }
    
    
}
