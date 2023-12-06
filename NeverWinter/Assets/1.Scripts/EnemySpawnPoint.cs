using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject TextObj;
    [SerializeField] private TextMeshProUGUI text;

    public float spawnDelay = 1f;
    public GameManager manager;

    public Transform[] containers;

    private int containerIndex = 0;
    
    
    private static int isFinishedCoroutine = 2;



    public void WaveStart()
    {
        if (GridTowerRandomSpawn.grids.Count == GridTowerRandomSpawn.gridsMaxCount)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
            TextObj.SetActive(true);
            text.text = "타워를 소환하여 주세요.";
            Invoke(nameof(Hide1), 1f);
            return;
        }
        else
        {
            GridTower.PlayClick = false;
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Wave);
            manager.Sumonbutton.transform.DOLocalMoveY(-675, 1f);
            manager.Wavebutton.transform.DOLocalMoveY(-675, 1f);
        }
        if (isFinishedCoroutine >= 2)
        {
            isFinishedCoroutine = 0;

            if (containerIndex < containers.Length)
            {
                int enemyCount = 0;
                WaveContainer[] waves = containers[containerIndex].GetComponentsInChildren<WaveContainer>();
                foreach (WaveContainer container in waves)
                {
                    StartCoroutine(EnemySpawn(container));
                    enemyCount += container.enemies.Length;
                }
                manager.enemyCount = enemyCount;
                containerIndex++;
            }
        }
    }

    private void Hide1()
    {
        TextObj.SetActive(false);
    }

    private IEnumerator EnemySpawn(WaveContainer container)
    {
        for (; ; )
        {
            GameObject enemy = container.GetEnemy();
            if (enemy == null)
            {
                break;
            }

            Instantiate(enemy, container.way.WayPoints[0].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnDelay);
        }

        isFinishedCoroutine++;
    }
}