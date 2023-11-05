using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridField : MonoBehaviour
{
    public GridTower havingTower;
    public Transform havingTowerParent;

    [SerializeField] private Material green;
    [SerializeField] private Material blue;
    [SerializeField] private Material red;

    public void VisualizeMovable(GridTower tower, MeshRenderer visual)
    {
        if (havingTower == null)
        {
            visual.material = green;
        }
        else if (havingTower.ID == tower.ID && havingTower.HighRankTower != null && havingTower != tower)
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
        else if (tower.ID == havingTower.ID && havingTower.HighRankTower != null && havingTower != tower)
        {
            GameObject having = havingTower.HighRankTower;
            Instantiate(having, transform.position, Quaternion.identity);

            Destroy(parent);
            Destroy(havingTower.transform.parent);

            havingTower = tower;
            havingTowerParent = parent;

            return true;
        }

        return false;
    }
}
