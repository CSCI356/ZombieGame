using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Shoot
{   
    void Update()
    {
        if(Input.GetKeyDown("space")){
            this.Fire();
        }
    }

    public void Fire(){
        // create a bullet object
        GameObject firedProj1;
        firedProj1 = Instantiate(bullet, barrel.transform.position+barrel.transform.up*2, transform.rotation) as GameObject;
        
        GameObject firedProj2;
        firedProj2 = Instantiate(bullet, barrel.transform.position+barrel.transform.up*2+new Vector3(0.1f, 0, 0), transform.rotation) as GameObject;
        
        GameObject firedProj3;
        firedProj3 = Instantiate(bullet, barrel.transform.position+barrel.transform.up*2+new Vector3(-0.1f, 0, 0), transform.rotation) as GameObject;

        // add force and direction
        Vector3 projectileForce1 = (barrel.transform.up.normalized) * bulletPower;
        firedProj1.GetComponent<Rigidbody>().AddForce(projectileForce1, ForceMode.Impulse);
        Vector3 projectileForce2 = ((barrel.transform.up+new Vector3(5, 0, 0)).normalized) * bulletPower;
        firedProj2.GetComponent<Rigidbody>().AddForce(projectileForce2, ForceMode.Impulse);
        Vector3 projectileForce3 = ((barrel.transform.up+new Vector3(-5, 0, 0)).normalized) * bulletPower;
        firedProj3.GetComponent<Rigidbody>().AddForce(projectileForce3, ForceMode.Impulse);
    }
}
