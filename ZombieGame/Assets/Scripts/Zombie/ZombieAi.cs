using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAi : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    public void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
    }

    public void Update()
    {
        agent.SetDestination(target.position);
    }

}