using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager singleton;

    [Serializable]
    public struct EnemySpawnData
    {
        public EnemySpawn spawner;
        public int spawnEveryLevel;
    }

    public List<EnemySpawnData> spawnData;
    public bool isSpawning;
    public int enemyLevel;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartSpawn(int currentLevel)
    {
        isSpawning = true;
        enemyLevel = Mathf.CeilToInt(currentLevel);

        foreach (EnemySpawnData data in spawnData)
        {
            if (currentLevel % data.spawnEveryLevel == 0)
            {
                data.spawner.StartSpawn(enemyLevel);
            }
        }
    }

    public void StopSpawn()
    {
        isSpawning = false;
        foreach (EnemySpawnData data in spawnData)
        {
            data.spawner.StopSpawn();
        }
    }
}
