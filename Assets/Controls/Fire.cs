using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject projectile;
    void Update(){
        if (Input.GetButtonDown("Fire1")){
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}