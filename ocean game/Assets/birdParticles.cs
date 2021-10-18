using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdParticles : MonoBehaviour
{
    ParticleSystem particles;
    public GameObject Player;

    Vector2 playerPos;
    Vector2 birdPos;
    public float checkRange = 30;
    public float flySpeed = 5;
    float flyStore;
    bool flying = false;
    int numberOfBirds;
    public bool sound = true;
    ParticleSystem.Particle[] m_Particles;
    float[] xpos;
    public AudioClip clip;
    public int birdmin =1, birdmax=4;
    // Start is called before the first frame update
    void Start()
    {
        flyStore = flySpeed;
        particles = GetComponent<ParticleSystem>();
        numberOfBirds = Random.Range(birdmin, birdmax);
        emitBirds();
        birdPos = new Vector2(transform.position.x, transform.position.y);
        
    }
    /*
    void InitializeIfNeeded()
    {
        if (particles == null)
            particles = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < particles.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[particles.main.maxParticles];

        if (xpos == null)
        {
            xpos = new float[m_Particles.Length];
            for (int i = 0; i < m_Particles.Length; i++)
            {
                xpos[i] = 0;
            }
        }
            

    }
    private void LateUpdate()
    {
        InitializeIfNeeded();
        int numParticlesAlive = particles.GetParticles(m_Particles);

        // Change only the particles that are alive
        for (int i = 0; i < numParticlesAlive; i++)
        {
           

            if(xpos[i] > m_Particles[i].position.x)
            {
                
                m_Particles[i].rotation = .1f;
                Debug.Log(m_Particles[i].rotation);
            }
            else
            {
                m_Particles[i].rotation = 1;
            }

            xpos[i] = m_Particles[i].position.x;
        }
    }
    
    */
    // Update is called once per frame
    void Update()
     {
        playerPos = new Vector2(Player.transform.position.x, Player.transform.position.y);

     
        if (flying)
        {
            flySpeed += 10f*Time.deltaTime;
            var vel = particles.velocityOverLifetime;
            vel.radial = flySpeed;

            if (Mathf.Abs(playerPos.x - birdPos.x) > checkRange || Mathf.Abs(playerPos.y - birdPos.y) > checkRange-3)
            {
                
                emitBirds();
                flying = false;


            }
        }
        

    }


    void emitBirds()
    {
        particles.Clear();
        
        var ts = particles.textureSheetAnimation;
        ts.rowIndex = 1;
        var vel = particles.velocityOverLifetime;
        vel.enabled = false;
        vel.radial =0;
        flySpeed = flyStore;
        var emitParams = new ParticleSystem.EmitParams();
        particles.Emit(emitParams, numberOfBirds);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !flying)
        {
            flying = true;
            var ts = particles.textureSheetAnimation;
            ts.rowIndex = 0;

            var vel = particles.velocityOverLifetime;
            vel.enabled = true;
            
            vel.radial = flySpeed;

            if (sound)
            {
                GetComponent<AudioSource>().PlayOneShot(clip);
            }
            
        }
    }



}
