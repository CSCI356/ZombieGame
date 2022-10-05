using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]

public class ZombieMovement : MonoBehaviour
{
    private Transform playerTransf;
    // private Animator botAnimator;
    private bool stop = false;
    public bool inRadius = false;
    private Vector3 direction;

    public NavMeshAgent agent;

    //[Range(0, 10)] public float speed;
    public float wanderSpeed = 2;
    public float followSpeed = 5;
    [Range(1, 200)] public float walkRadius;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransf = player.GetComponent<Transform>();

        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.speed = wanderSpeed;
            agent.SetDestination(RandomNavMeshLocation());
        }
        // botAnimator = GetComponent<Animator>();
    }

    public void setSpeed()
    {
        if (inRadius)
        {
            agent.speed = followSpeed;
        }
        else
        {
            agent.speed = wanderSpeed;
        }
    }

    private void Update()
    {

        if (agent != null && !stop)
        {
            if (inRadius)
            {
                agent.SetDestination(playerTransf.position);

                LookAtPlayer();
            }
            else if (agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.SetDestination(RandomNavMeshLocation());
            }
        }

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
        agent.speed = 0;
    }

    public Vector3 RandomNavMeshLocation()
    {
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * walkRadius;
        if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}