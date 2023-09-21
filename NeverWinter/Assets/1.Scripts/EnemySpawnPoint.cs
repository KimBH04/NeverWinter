using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public List<GameObject> enemyWave1 = new List<GameObject>();
    public List<GameObject> enemyWave2 = new List<GameObject>();
    public List<GameObject> enemyWave3 = new List<GameObject>();
    public List<GameObject> enemyWave4 = new List<GameObject>();
    public List<GameObject> enemyWave5 = new List<GameObject>();
    
    
    public Transform spawnPoint;

    
    

    private bool hasSpawnedWave = true;


    

    IEnumerator Start()
    {
        
        
        
        if (enemyWave1.Count > 0 && !hasSpawnedWave)
        {
            foreach (GameObject enemy in enemyWave1)
            {
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(1);
            }
            hasSpawnedWave = true;
            
        }
        hasSpawnedWave = false;
        yield return new WaitForSeconds(5);
        
        
        
        if (enemyWave2.Count > 0 && !hasSpawnedWave)
        {
            foreach (GameObject enemy in enemyWave2)
            {
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(1);
            }
            hasSpawnedWave = true;
            
        }
        
        hasSpawnedWave = false;
        yield return new WaitForSeconds(5);
        
        
        if (enemyWave3.Count > 0 && !hasSpawnedWave)
        {
            foreach (GameObject enemy in enemyWave3)
            {
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(1);
            }
            hasSpawnedWave = true;
        }
        hasSpawnedWave = false;
        yield return new WaitForSeconds(5);
        
        
        if (enemyWave4.Count > 0 && !hasSpawnedWave)
        {
            foreach (GameObject enemy in enemyWave4)
            {
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(1);
            }
            hasSpawnedWave = true;
        }
        
        hasSpawnedWave = false;
        yield return new WaitForSeconds(5);
        
        if (enemyWave5.Count > 0 && !hasSpawnedWave)
        {
            foreach (GameObject enemy in enemyWave5)
            {
                SpawnEnemy(enemy);
                yield return new WaitForSeconds(1);
            }
            hasSpawnedWave = true;
        }
        
        
    }

   

    
    void SpawnEnemy(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
    }
}