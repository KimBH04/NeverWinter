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

        
        grids[gridIdx].havingTowerParent = Instantiate(towers[towerIdx], grids[gridIdx].transform.position, Quaternion.identity);

        
        grids[gridIdx].havingTower = grids[gridIdx].havingTowerParent.GetComponentInChildren<GridTower>();

        
        grids[gridIdx].havingTower.field = grids[gridIdx];

        grids.RemoveAt(gridIdx);
    }

    private IEnumerator CloseMessage(GameObject obj)
    {
        yield return new WaitForSeconds(1);
        obj.SetActive(false);
    }
}
