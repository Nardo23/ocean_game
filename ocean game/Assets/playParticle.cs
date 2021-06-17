using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playParticle : MonoBehaviour
{

    public ParticleSystem particle;

    public void ParticlesPlay()
    {
        particle.Play();
    }
}
