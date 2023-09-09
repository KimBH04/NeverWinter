using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject[] enemy;
    public Transform spawnPoint;

    IEnumerator Start()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, enemy.Length);
            GameObject selectEnemy = enemy[randomIndex];
            
            
            Instantiate(selectEnemy, spawnPoint.position, Quaternion.Euler(Vector3.forward));

            yield return new WaitForSeconds(3);
        }
    }
}
