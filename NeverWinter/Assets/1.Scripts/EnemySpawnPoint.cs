using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemy;
    public Transform spawnPoint;

    IEnumerator Start()
    {
        while (true)
        {
            Instantiate(enemy, spawnPoint.position, Quaternion.Euler(Vector3.forward));

            yield return new WaitForSeconds(3);
        }
    }
}
