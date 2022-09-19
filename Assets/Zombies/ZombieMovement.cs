using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Transform playerTransf;
    // private Animator botAnimator;
    private bool stop = false;
    public bool inRadius = false;
    private Vector3 direction;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransf = player.GetComponent<Transform>();
        // botAnimator = GetComponent<Animator>();
        GameEvents.current.OnMovementTrigger += StartMovement;
    }

    private void Update()
    {
        LookAtPlayer();
    }

    public void StartMovement()
    {
        if (inRadius)
        {
            if (direction.magnitude < 1)
            {
                // botAnimator.SetTrigger("attack");
                this.transform.Translate(0, 0, 0.04F);
            }
            else
            {
                // botAnimator.SetTrigger("walk");
                this.transform.Translate(0, 0, 0.02F);
            }
        }
        //print(hit);
    }

    private void LookAtPlayer()
    {
        direction = playerTransf.position - this.transform.position;
        direction.y = 0;
        this.transform.rotation = Quaternion.LookRotation(direction);
    }

    public void StopMovement()
    {
        stop = true;
    }
}