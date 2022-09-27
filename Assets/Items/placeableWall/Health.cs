using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{

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
            PlayPartical();
        }
    }

    void PlayPartical()
    {

    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
