using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridTowerRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject noCoinMessage;              //���� ���� �޽���
    [SerializeField] private GameObject MaxTowerMessage;    //  그리드 타워로 꽉 차서 더이상 타워를 생성할 수 없을 때 뜨는 메세지
    [SerializeField] private Transform[] towers;                    //������ Ÿ�� �����յ�
    public static List<GridField> grids = new List<GridField>();    //��ü �׸���

    private int towersCount;    //Ÿ�� ������ �ִ� ����

    //[SerializeField] private int spawnMaxCount;


    private void Awake()
    {
        noCoinMessage.SetActive(false);
        MaxTowerMessage.SetActive(false);
    }

    private void Start()
    {
        GetComponentsInChildren<GridField>(grids);
        towersCount = towers.Length;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused;
        }
#endif
    }

    public void SpawningTowerToRandomPosition()
    {
        if (Cost.Coin < 100)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Sum);
            noCoinMessage.SetActive(true);
            StartCoroutine(CloseNoCoinMessage());
            return;
        }

        if (grids.Count == 0)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Sum);
            MaxTowerMessage.SetActive(true);
            StartCoroutine(CloseMaxTowerMessage());
            return;
        }

        Cost.Coin -= 100;

        int gridIdx = Random.Range(0, grids.Count);
        int towerIdx = Random.Range(0, towersCount);

        //������ �׸��� ��ġ�� Ÿ�� ����
        grids[gridIdx].havingTowerParent = Instantiate(towers[towerIdx], grids[gridIdx].transform.position, Quaternion.identity);

        //������ ��ġ�� �׸��忡 Ÿ�� ���� ����
        grids[gridIdx].havingTower = grids[gridIdx].havingTowerParent.GetComponentInChildren<GridTower>();

        //Ÿ���� �׸��� ���� ����
        grids[gridIdx].havingTower.field = grids[gridIdx];

        grids.RemoveAt(gridIdx);
    }

    private IEnumerator CloseNoCoinMessage()
    {
        yield return new WaitForSeconds(1);
        noCoinMessage.SetActive(false);
    }
    private IEnumerator CloseMaxTowerMessage()
    {
        yield return new WaitForSeconds(1);
        MaxTowerMessage.SetActive(false);
    }
}
