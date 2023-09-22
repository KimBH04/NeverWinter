using UnityEngine;

public class WaveContainer : MonoBehaviour
{
    public GameObject[] enemies;
    int enemyCount = 0;

    public GameObject GetEnemy()
    {
        if (enemyCount >= enemies.Length)
            return null;

        return enemies[enemyCount++];
    }
}
