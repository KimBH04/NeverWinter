using UnityEngine;

public class WaveContainer : MonoBehaviour
{
    public GameObject[] enemies;

    int enemyCount = 0;

    
    public GameObject UI;



    public GameObject GetEnemy()
    {

        if (enemyCount >= enemies.Length)
        {
            //UI.SetActive(true);
             return null;
        }


        return enemies[enemyCount++];

    }
}
