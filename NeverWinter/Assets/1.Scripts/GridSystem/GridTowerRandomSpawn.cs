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
        GridTower tower = towers[towerIdx].GetComponentInChildren<GridTower>();

        grids[gridIdx].havingTower = tower;
        grids[gridIdx].havingTowerParent = Instantiate(towers[towerIdx], gridTr.position, Quaternion.identity);
    }
}
