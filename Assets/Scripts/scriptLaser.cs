using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptLaser : MonoBehaviour
{
    ParticleSystem particles;
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    
    void Update()
    {
        if (particles.isStopped)
        {
            Destroy(gameObject);
        }
    }
}
