using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] public GameObject bullet;
    [SerializeField] public float bulletPower = 5.0f;
    [SerializeField] public GameObject barrel;

    [SerializeField] private bool automatic = false;

    private float gunheat;
    [SerializeField] private float fireRate = 1;
    [SerializeField] public int bulletDamage = 1;

    // Update is called once per frame
    void Update()
    {   
        if (gunheat > 0) gunheat -=Time.deltaTime;

        if(automatic){
            if(Input.GetKey("space")){
                this.Fire();
            }
        }else{
            if(Input.GetKeyDown("space")){
                this.Fire();
            }
        }
    }

    public void Fire(){
        if (gunheat <= 0){
            // create a bullet object
            GameObject firedProj;
            firedProj = Instantiate(bullet, barrel.transform.position+barrel.transform.up*2, transform.rotation) as GameObject;

            // add force and direction
            Vector3 projectileForce = (barrel.transform.up.normalized) * bulletPower;
            firedProj.GetComponent<Rigidbody>().AddForce(projectileForce, ForceMode.Impulse);
            gunheat = fireRate;

            StartCoroutine(DelayedDelete(firedProj));
        }
   }

   IEnumerator DelayedDelete(GameObject bullet){
        yield return new WaitForSeconds(2);
        // deletes zombie
        Destroy(bullet);
    }
}