using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class pushRadiusCavalier : MonoBehaviour
{
    public int speed;
    private float pushForce;

    private int degats;
    private SelectableUnit unit;
    [SerializeField] private GameObject attackParticules;
    //private Vector3 smoothedPosition;
    //private float tLerp = 0.8f;
    
    private void Start()
    {
        pushForce = speed / 2 + 200f;
        unit = GetComponentInParent<SelectableUnit>();

    }

    private void Update()
    {
        if (unit.Agent.velocity.magnitude < 5)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<scriptEnemy>(out scriptEnemy enemy))
        {
            if (enemy)
            {

                var particules = Instantiate(attackParticules, unit.transform.position, unit.transform.rotation, unit.transform);
                particules.SetActive(true);

                degats = 4 + speed;
                enemy.TakeDamage(degats);

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
