using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(healthAmount <=0) {
            Debug.Log("player is dead");
       }
    }

    public void TakeDamage(float damage) {
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f; 
    }

    
}
