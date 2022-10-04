using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public ParticleSystem deathEffect;
    public float health = 100;
    private float timeToDamage = 1;
    public float delay = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Damaged(int damTaken)
    {
        health -= damTaken;
      
        if(health <= 0)
        {
            Destroy(gameObject);
            // spawn death effect (ParticleSystem)
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        timeToDamage = Time.time + 1;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(timeToDamage <= Time.time && collision.gameObject.tag == "Zombies")
        {
            timeToDamage = Time.time + 1;
            Damaged(10);
        }
    }

    // testing
    void Update()
    {
        
    }
}
