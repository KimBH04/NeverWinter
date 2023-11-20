using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualTowerManager : MonoBehaviour
{
    private static Transform[] visualTowers;
    public static MeshRenderer visualBox;

    private void Start()
    {
        visualTowers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            visualTowers[i] = transform.GetChild(i);
        }
        visualBox = visualTowers[0].GetComponentInChildren<MeshRenderer>();
    }

    public static void VisualizingTower(int ID, Vector3 position)
    {
        visualTowers[0].position = position;
        visualTowers[ID].position = position;
    }

    public static void EndVisualizing(int ID)
    {
        visualTowers[0].localPosition = Vector3.zero;
        visualTowers[ID].localPosition = Vector3.zero;
    }
}
