using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTowerRandomSpawn : MonoBehaviour
{
    [SerializeField] private GameObject noCoinMessage;              
    [SerializeField] private GameObject maxTowerMessage;    
    [SerializeField] private Transform[] towers;                    
    public static List<GridField> grids = new List<GridField>();    

    private int towersCount;    

    // [SerializeField] private int spawnMaxCount;


    private void Awake()
    {
        noCoinMessage.SetActive(false);
        maxTowerMessage.SetActive(false);
    }

    private void Start()
    {
        GetComponentsInChildren(grids);
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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Sum);
        if (Cost.Coin < 100)
        {
            noCoinMessage.SetActive(true);
            StartCoroutine(CloseMessage(noCoinMessage));
            return;
        }

        if (grids.Count == 0)
        {
            maxTowerMessage.SetActive(true);
            StartCoroutine(CloseMessage(maxTowerMessage));
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

    private IEnumerator CloseMessage(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }
}
