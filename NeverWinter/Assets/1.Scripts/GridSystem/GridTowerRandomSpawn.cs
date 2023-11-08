using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTowerRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject noCoinMessage;              //���� ���� �޽���
    [SerializeField] private Transform[] towers;                    //������ Ÿ�� �����յ�
    public static List<GridField> grids = new List<GridField>();    //��ü �׸���

    private int towersCount;    //Ÿ�� ������ �ִ� ����

    //[SerializeField] private int spawnMaxCount;

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
            noCoinMessage.SetActive(true);
            StartCoroutine(CloseNoCoinMessage());
            return;
        }

        if (grids.Count == 0)
        {
            //��ȯ�� �� �ִ� �׸��尡 �� �������� ��
            //���̻� Ÿ���� ��ȯ�� �� �����ϴ� �޽��� ����
            Debug.Log("���̻� Ÿ���� ��ȯ�� �� �����ϴ�");
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
}
