using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour

{
    public ZombieAi zombie;
    private List<ZombieAi> zombies;

    [Range(0, 100)]
    public int numberOfZombies = 25;
    private float range = 70.0f;

    [Range(0, 5)]
    public float delayBetweenSpawns = 0.5f;


    public void Start()
    {
        zombies = new List<ZombieAi>();
        for (int i = 0; i < numberOfZombies; i++)
        {
            Invoke("SpawnZombie", i * delayBetweenSpawns);

        }

    }

    public void SpawnZombie()
    {
        ZombieAi spawnedZombie = Instantiate(zombie, RandomLocation(range), Quaternion.identity) as ZombieAi;
        zombies.Add(spawnedZombie);
    }

    public Vector3 RandomLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;

        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;

    }

}