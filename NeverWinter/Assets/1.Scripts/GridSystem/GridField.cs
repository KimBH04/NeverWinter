using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridField : MonoBehaviour
{
    public GridTower havingTower;       //현재 위치해 있는 타워 정보
    public Transform havingTowerParent; //현재 위치해 있는 타워 오브젝트

    [Header("Visual")]
    [SerializeField] private Material green;
    [SerializeField] private Material blue;
    [SerializeField] private Material red;

    /// <summary>
    /// 클릭 후 타워 이동 가능 확인 상자 표시
    /// </summary>
    /// <param name="tower">타워</param>
    /// <param name="visual">비주얼 박스</param>
    public void VisualizeMovable(GridTower tower)
    {
        if (havingTower == null)
        {
            VisualTowerManager.visualBox.material = green;
        }
        else if (havingTower != tower && havingTower.HighRankTower != null && tower.ID == havingTower.ID)
        {
            VisualTowerManager.visualBox.material = blue;
        }
        else
        {
            VisualTowerManager.visualBox.material = red;
        }
    }

    /// <summary>
    /// 클릭 해제 시 타워 이동 시도
    /// </summary>
    /// <param name="tower">타워 정보</param>
    /// <param name="parent">타워 오브젝트</param>
    /// <returns>이동 및 병합 성공 여부</returns>
    public bool MovingTower(GridTower tower, Transform parent)
    {
        if (havingTower == null)
        {
            parent.position = transform.position;

            havingTower = tower;
            havingTowerParent = parent;

            return true;
        }
        else if (havingTower != tower && havingTower.HighRankTower != null && tower.ID == havingTower.ID)
        {
            GameObject high = Instantiate(havingTower.HighRankTower, transform.position, Quaternion.identity);

            Destroy(parent.gameObject);
            Destroy(havingTowerParent.gameObject);

            havingTower = high.GetComponentInChildren<GridTower>();
            havingTowerParent = high.transform;

            havingTower.field = this;

            return true;
        }
        else
        {
            return false;
        }
    }
}
