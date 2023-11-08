using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTowerRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject noCoinMessage;              //코인 부족 메시지
    [SerializeField] private Transform[] towers;                    //생성할 타워 프리팹들
    public static List<GridField> grids = new List<GridField>();    //전체 그리드

    private int towersCount;    //타워 프리팹 최대 개수

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
            //소환할 수 있는 그리드가 다 떨어졌을 때
            //더이상 타워를 소환할 수 없습니다 메시지 띄우기
            Debug.Log("더이상 타워를 소환할 수 없습니다");
            return;
        }

        Cost.Coin -= 100;

        int gridIdx = Random.Range(0, grids.Count);
        int towerIdx = Random.Range(0, towersCount);

        //랜덤한 그리드 위치에 타워 생성
        grids[gridIdx].havingTowerParent = Instantiate(towers[towerIdx], grids[gridIdx].transform.position, Quaternion.identity);

        //생성된 위치의 그리드에 타워 정보 대입
        grids[gridIdx].havingTower = grids[gridIdx].havingTowerParent.GetComponentInChildren<GridTower>();

        //타워에 그리드 정보 대입
        grids[gridIdx].havingTower.field = grids[gridIdx];

        grids.RemoveAt(gridIdx);
    }

    private IEnumerator CloseNoCoinMessage()
    {
        yield return new WaitForSeconds(1);
        noCoinMessage.SetActive(false);
    }
}
