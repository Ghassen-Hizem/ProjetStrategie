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
    private void Start()
    {
        pushForce = speed / 2 + 200f;
        unit = GetComponentInParent<SelectableUnit>();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<scriptEnemy>(out scriptEnemy enemy))
        {
            if (enemy)
            {
                var particules = Instantiate(attackParticules, unit.transform.position, unit.transform.rotation, unit.transform);
                particules.SetActive(true);
                //Debug.Log("enemy damaged");
                degats = 4 + speed;
                enemy.TakeDamage(degats);

                //this dont work because of the kinematic 
                pushForce = speed / 2 + 200f;
                collider.attachedRigidbody.AddForce(pushForce * -collider.transform.forward, ForceMode.Impulse) ;
            }
            

        }
    }
    /*
    private void OnTriggerExit(Collider other)
    {
        gameObject.SetActive(false);
    }*/
}
