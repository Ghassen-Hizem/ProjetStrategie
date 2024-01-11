using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    private Animator anim;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

 
    void Update()
    {
       if(healthAmount <=0) {
            anim.SetBool("IsDead",true);
            Destroy(gameObject,5);

       }
    }

    public void TakeDamage(float damage) {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;     
    }

    
}
