using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockdebrisScript : MonoBehaviour
{
    private ParticleSystem debrisParticles;
    // Start is called before the first frame update
    void Start()
    {
        debrisParticles = GetComponent<ParticleSystem>();

        debrisParticles.Emit(15);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
