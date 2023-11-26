using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTowerRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject TextObj;
    [SerializeField] private Text text;
    // [SerializeField] private GameObject maxTowerMessage;    
    [SerializeField] private Transform[] towers;                    
    public static List<GridField> grids = new List<GridField>();    

    private int towersCount;    

    // [SerializeField] private int spawnMaxCount;


    private void Awake()
    {
        TextObj.SetActive(false);
    }

    private void Start()
    {
        GetComponentsInChildren(grids);
        towersCount = towers.Length;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Pause))
        {
            UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused;
        }
#endif
    }

    public void SpawningTowerToRandomPosition()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Sum);
        if (Cost.Coin < 100)
        {
            TextObj.SetActive(true);
            text.text = "소지금이 부족합니다.";
            Invoke(nameof(Hide),1f);
            return;
        }

        if (grids.Count == 0)
        {
            TextObj.SetActive(true);
            text.text = "더이상 소환할 수 없습니다.";
            Invoke(nameof(Hide),1f);
            return;
        }

        Cost.Coin -= 100;

        int gridIdx = Random.Range(0, grids.Count);
        int towerIdx = Random.Range(0, towersCount);

        //그리드에 타워 소환
        grids[gridIdx].havingTowerParent = Instantiate(towers[towerIdx], grids[gridIdx].transform.position, Quaternion.identity);

        //그리드에 타워 정보 전달
        grids[gridIdx].havingTower = grids[gridIdx].havingTowerParent.GetComponentInChildren<GridTower>();

        //타워에 그리드 정보 전달
        grids[gridIdx].havingTower.field = grids[gridIdx];

        grids.RemoveAt(gridIdx);
    }

    // Hide 함수 두개 째  다음 프로젝트에는 자주 쓰는 함수를 게임매니저스트립트에 넣고 싱글톤으로 만들어서 쓸 것  그럼 편함
    private void Hide()
    {
        TextObj.SetActive(false);
    }
    

   
}
