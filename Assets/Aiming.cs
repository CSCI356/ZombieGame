using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    public FixedJoystick Aim;
    Vector3 moveVelocity;
    Vector3 aimVelocity;
    public Rigidbody rb;

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


    }
} 
