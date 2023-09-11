using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public List<GameObject> enemyWave = new List<GameObject>();
    public Transform spawnPoint;

    
    

    private bool hasSpawnedWave = false;

    IEnumerator Start()
    {
        if (enemyWave.Count > 0 && !hasSpawnedWave)
        {
            foreach (GameObject enemy in enemyWave)
            {
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(1);
            }
            hasSpawnedWave = true;
        }

        yield return null;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}