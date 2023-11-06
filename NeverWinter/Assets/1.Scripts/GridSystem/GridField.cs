using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridField : MonoBehaviour
{
    public GridTower havingTower;
    public Transform havingTowerParent;

    [Header("Visual")]
    [SerializeField] private Material green;
    [SerializeField] private Material blue;
    [SerializeField] private Material red;

    public void VisualizeMovable(GridTower tower, MeshRenderer visual)
    {
        if (havingTower == null)
        {
            visual.material = green;
        }
        else if (havingTower != tower && havingTower.HighRankTower != null && tower.ID == havingTower.ID)
        {
            visual.material = blue;
        }
        else
        {
            visual.material = red;
        }
    }

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

        return false;
    }
}
