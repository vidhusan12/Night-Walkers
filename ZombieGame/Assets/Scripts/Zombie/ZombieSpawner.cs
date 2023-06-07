using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [SerializeField] private Waves[] waves;
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float waveCountdown = 0;
    private int currentWave;

    private SpawnState state = SpawnState.COUNTING;

    [SerializeField] private Transform[] spawners;
    [SerializeField] private List<PlayerStatus> zombieList;
    


    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        currentWave = 0;
    }

    private void Update()
    {
        if(state == SpawnState.WAITING)
        {
            if (!zombiesAreDead())
                return;
            else
                CompleteWave();
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[currentWave]));
            }

        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
        
    }

    private IEnumerator SpawnWave(Waves waves)
    {
        state = SpawnState.SPAWNING;

        for(int i = 0; i < waves.enemiesAmount; i++)
        {
            SpawnZombie(waves.zombie);
            yield return new WaitForSeconds(waves.delay);

        }

       

        state = SpawnState.WAITING;

        yield break;
    }

    private void SpawnZombie(GameObject zombie)
    {
        int randomInt = Random.RandomRange(1, spawners.Length);
        Transform randomSpawner = spawners[randomInt];
        GameObject newZombie = Instantiate(zombie, randomSpawner.position, randomSpawner.rotation);
        PlayerStatus newZombieStatus = newZombie.GetComponent<PlayerStatus>();

        zombieList.Add(newZombieStatus);
    }

    private bool zombiesAreDead()
    {
        int i = 0;
        foreach(PlayerStatus zombie in zombieList)
        {
            if (zombie.IsDead())
                i++;
            else
                return false;
              
        }
        return true;
    }

    private void CompleteWave()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(currentWave + 1 > waves.Length - 1)
        {
            currentWave = 0;
            Debug.Log("Completed all the waves");
        }
        else
        {
            currentWave++;
        }

        
    }


}
