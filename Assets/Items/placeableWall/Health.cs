using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    public ParticleSystem deathEffect;
    public float health = 100;
    public float delay = 0.2f;

    private float coolDown = 0;

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

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Zombies")
        {
            if(coolDown <= 0){
                coolDown = delay;
                Damaged(10);
            }else{
                coolDown -= Time.deltaTime;
            }
        }
    }
}
