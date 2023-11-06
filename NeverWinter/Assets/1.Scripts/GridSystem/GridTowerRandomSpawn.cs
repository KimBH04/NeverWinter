using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTowerRandomSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] towers;
    [SerializeField] private GridField[] grids;

    private int towersCount;
    private int gridsCount;
    [SerializeField] private int spawnMaxCount;

    private void Start()
    {
        towersCount = towers.Length;
        gridsCount = grids.Length;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused;
        }
#endif
    }

    public void SpawningTowerToRandomPosition()
    {
        if (spawnMaxCount-- <= 0)
        {
            return;
        }

        int gridIdx;
        do
        {
            gridIdx = Random.Range(0, gridsCount);
        }
        while (grids[gridIdx].havingTower != null);
        Transform gridTr = grids[gridIdx].transform;

        int towerIdx = Random.Range(0, towersCount);

        grids[gridIdx].havingTowerParent = Instantiate(towers[towerIdx], gridTr.position, Quaternion.identity);
        grids[gridIdx].havingTower = grids[gridIdx].havingTowerParent.GetComponentInChildren<GridTower>();
        grids[gridIdx].havingTower.field = grids[gridIdx];
    }
}
