using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

[Serializable]
public class Enemys 
{
    [SerializeField] private string enemyName;

    [SerializeField] internal int poolCount = 3;
    [SerializeField] internal bool autoExpand = true;
    [SerializeField] internal BaseEnemy enemyPrefab;

    internal PoolMono<BaseEnemy> pool;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Enemys> enemysList;
    [Space]
    [SerializeField] private float xSpawnBorder = 3f;

    private bool spawnEnemy = true;

    private void Start()
    {
        InitializePools();
    }

    private void Update()
    {
        if (spawnEnemy)
        {
            StartCoroutine(SpawnNextEnemy());
            spawnEnemy = false;
        }
    }

    IEnumerator SpawnNextEnemy()
    {
        SpawEnemy();
        yield return new WaitForSeconds(Random.Range(1.5f, 3.5f));
        spawnEnemy = true;
    }

    private void SpawEnemy()
    {
        var rX = Random.Range(-xSpawnBorder, xSpawnBorder);
        var rZ = Random.Range(transform.position.z - 1f, transform.position.z + 1f);
        var y = transform.position.y;

        var rPosition = new Vector3(rX, y, rZ);

        int rIndex = Random.Range(0, enemysList.Count);
        var enemy = enemysList[rIndex].pool.GetFreeElement();
        enemy.transform.position = rPosition;
    }

    private void InitializePools()
    {
        for (int i = 0; i < enemysList.Count; i++)
        {
            enemysList[i].pool = new PoolMono<BaseEnemy>(enemysList[i].enemyPrefab, enemysList[i].poolCount, transform);
            enemysList[i].pool.autoExpand = enemysList[i].autoExpand;
        }
    }
}
