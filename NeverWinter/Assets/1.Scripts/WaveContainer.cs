using UnityEngine;

public class WaveContainer : MonoBehaviour
{
    public WayContainer way;
    public GameObject[] enemies;


    int enemyCount = 0;

    
    public GameObject GetEnemy()
    {

        if (enemyCount >= enemies.Length)
        {
           
             return null;
        }

        enemies[enemyCount].GetComponent<EnemyCtrl>().container = way;
        return enemies[enemyCount++];

    }
}
