using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public FixedJoystick Aim;
    Vector3 moveVelocity;
    Vector3 aimVelocity;
    public Rigidbody rb;
    public GameObject bullet;
    public Transform firePoint;
   public float fireSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        aimVelocity = new Vector3(Aim.Horizontal, 0f, Aim.Vertical);
        Vector3 AimInput = new Vector3(aimVelocity.x, 0f, aimVelocity.z);
        Vector3 lookAtPoint = transform.position + AimInput;
        transform.LookAt(lookAtPoint);
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }
    void Shoot()
    {

        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
        bulletRb.AddForce(transform.forward * fireSpeed);
    }
}
