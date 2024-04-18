using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int spawnInterval;
    public GameObject enemy;
    public GameObject enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    IEnumerator SpawnerLoop()
    {
        yield return new WaitForSeconds(spawnInterval);
        Spawn();
    }
    void Spawn()
    {
        Instantiate(enemy, enemySpawner.transform);
        StartCoroutine(SpawnerLoop());
    }
}
