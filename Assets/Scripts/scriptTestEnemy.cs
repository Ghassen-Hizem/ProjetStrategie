using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class scriptTestEnemy : MonoBehaviour
{

    public Image healthBar;
    public int healthAmount = 100;

    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    /*
    private int lifePoints = 10;
    public void TakeDamage(int degats)
    {
        lifePoints = lifePoints - degats ;

        if (lifePoints <= 0)
        {
            
            Destroy(gameObject);
            //or just deactivate the object

            Debug.Log("enemy damaged");

        }
    }*/

    public void TakeDamage(int damage)
    {
        //print("damage = " + damage);
        healthAmount -= damage;
        //print("health amount = " + healthAmount);
        healthBar.fillAmount = healthAmount / 100;

        if (healthAmount <= 0)
        {
            anim.SetBool("IsDead", true);
            Destroy(gameObject, 5);

        }
    }


}
