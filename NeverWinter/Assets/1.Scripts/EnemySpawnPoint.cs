using System.Collections;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public float spawnDelay = 1f;

    public WaveContainer[] containers;
    //public Transform spawnPoint;
    int containerIndex = 0;

    public void WaveStart()
    {
        StartCoroutine(EnemySpawn());
        Debug.Log("STart");
    }

    IEnumerator EnemySpawn()
    {
        if (containerIndex >= containers.Length)
            yield break;

        for (; ; )
        {
            GameObject enemy = containers[containerIndex].GetEnemy();
            if (enemy == null)
            {
                break;
            }

            Instantiate(enemy, transform);
            yield return new WaitForSeconds(spawnDelay);
        }
        containerIndex++;
    }
}