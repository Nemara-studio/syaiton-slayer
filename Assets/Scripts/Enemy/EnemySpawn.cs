using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    [SerializeField] private List<Transform> spawnLocations;
    [SerializeField] private float minTimeToSpawn;
    [SerializeField] private float maxTimeToSpawn;
    private int levelToSpawn;

    private Coroutine spawnCoroutine;

    public void StartSpawn(int level)
    {
        Debug.Log($"Spawn Started..");

        levelToSpawn = level;

        spawnCoroutine = StartCoroutine(Spawning());
    }

    public IEnumerator Spawning()
    {

        float spawnTime = RandomTime();

        yield return new WaitForSeconds(spawnTime);

        Spawn();

        spawnCoroutine = StartCoroutine(Spawning());
    }

    public void StopSpawn()
    {
        if (spawnCoroutine == null) return;

        Debug.Log($"Spawn Stopped..");

        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }

    private void Spawn()
    {
        Vector3 spawnPos = RandomSpawnPosition();
        GameObject spawnObject = (GameObject)Instantiate(prefab, spawnPos, Quaternion.identity);
        spawnObject.GetComponent<Enemy>().SetStat(levelToSpawn);

        if (GameLogic.singleton != null)
        {
            GameLogic.singleton.enemySpawned++;
        }
    }

    private float RandomTime()
    {
         return UnityEngine.Random.Range(minTimeToSpawn, maxTimeToSpawn);
    }

    private Vector3 RandomSpawnPosition()
    {
        int rand = UnityEngine.Random.Range(0, spawnLocations.Count);
        return spawnLocations[rand].position;
    }
}
