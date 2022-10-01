using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public ParticleSystem deathEffect;
    public float health = 100;
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

   
    // testing
  /*  void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Damaged(50);
        }
    }*/
}
