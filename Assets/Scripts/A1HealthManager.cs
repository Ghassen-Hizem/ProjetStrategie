using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A1HealthManager : MonoBehaviour
{
    public Image healthBar;
    public int healthAmount = 100;

    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

 
    void Update()
    {
       /*
        if(healthAmount <=0) {
            anim.SetBool("IsDead",true);
            Destroy(gameObject,5);

       }*/
    }

    public void TakeDamage(int damage) {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100;

        if (healthAmount <= 0)
        {
            anim.SetBool("IsDead", true);
            Destroy(gameObject, 5);

        }
    }

    
}
