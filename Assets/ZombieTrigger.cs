using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieTrigger : MonoBehaviour
{
    public float radius;
    private void Start()
    {
        transform.localScale = new Vector3(radius, 0.001f, radius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zombies")
        {
            other.gameObject.GetComponent<ZombieMovement>().inRadius = true;
            other.gameObject.GetComponent<ZombieMovement>().setSpeed();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Zombies")
        {
            other.gameObject.GetComponent<ZombieMovement>().inRadius = false;
            other.gameObject.GetComponent<ZombieMovement>().setSpeed();
        }
    }

}
