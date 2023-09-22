using System.Collections;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public float spawnDelay = 1f;

    public WaveContainer[] containers;

    int containerIndex = 0;
    bool isFinishedCoroutine = true;

    public void WaveStart()
    {
        if (isFinishedCoroutine)
        {
            isFinishedCoroutine = false;
            StartCoroutine(EnemySpawn());
        }
        else
        {
            Debug.Log("Unfinished wave!");
        }
    }

    IEnumerator EnemySpawn()
    {
        if (containerIndex >= containers.Length)
        {
            isFinishedCoroutine = true;
            yield break;
        }

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
        isFinishedCoroutine = true;
    }
}