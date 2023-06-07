using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Transform target;

    private void Start()
    {
        GetReferences();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        agent.SetDestination(target.position);
        RotateToTarget();

        
    }

    private void RotateToTarget()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0; // Set the y component to 0 to avoid looking down

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }


    private void GetReferences()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerMovement.instance.transform;
    }

}
