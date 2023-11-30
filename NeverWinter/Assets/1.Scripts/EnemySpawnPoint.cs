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
        if (Cost.Coin >= 450)
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
            manager.Sumonbutton.transform.DOLocalMoveY(-658, 1f);
            manager.Wavebutton.transform.DOLocalMoveY(-658, 1f);
        }
        if (isFinishedCoroutine >= 2)
        {
            isFinishedCoroutine = 0;

            if (containerIndex < containers.Length)
            {
                WaveContainer[] waves = containers[containerIndex].GetComponentsInChildren<WaveContainer>();
                StartCoroutine(EnemySpawn(waves[0], true));
                StartCoroutine(EnemySpawn(waves[1], false));
                containerIndex++;
            }
        }
    }

    private void Hide1()
    {
        TextObj.SetActive(false);
    }

    private IEnumerator EnemySpawn(WaveContainer container, bool isLeft)
    {
        for (; ; )
        {
            GameObject enemy = container.GetEnemy();
            if (enemy == null)
            {
                break;
            }

            Instantiate(enemy, container.way.WayPoints[0].position, Quaternion.Euler(0f, isLeft ? 90f : -90f, 0f));
            yield return new WaitForSeconds(spawnDelay);
        }

        isFinishedCoroutine++;
    }
}