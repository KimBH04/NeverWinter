using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PoisonTower : GridTower
{
    [Header("Poison")]
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private Vector3 offset;

    private const float DIAGONAL = 0.7071067811865475f;
    private readonly Vector3[] directions = {
        new Vector3(0f, 0f, 1f),
        new Vector3(DIAGONAL, 0f, DIAGONAL),
        new Vector3(1f, 0f, 0f),
        new Vector3(DIAGONAL, 0f, -DIAGONAL),
        new Vector3(0f, 0f, -1f),
        new Vector3(-DIAGONAL, 0f, -DIAGONAL),
        new Vector3(-1f, 0f, 0f),
        new Vector3(-DIAGONAL, 0f, DIAGONAL)
    };

    private new void Start()
    {
        base.Start();
        WatchNearestRoad();
    }

    private new void OnMouseUp()
    {
        base.OnMouseUp();
        WatchNearestRoad();
    }

    private void Update()
    {
        foreach (var item in directions)
            Debug.DrawRay(transform.position + offset, item * maxDistance, Color.red);
    }

    private void WatchNearestRoad()
    {
        Vector3 v = new Vector3(0f, 0f, 0f);
        float min = float.MaxValue;

        foreach (Vector3 dir in directions)
        {
            if (Physics.Raycast(transform.position + offset, dir, out RaycastHit hit, maxDistance, 1 << 10))
            {
                Debug.Log(hit.transform.name);
                float distance = hit.distance;

                if (distance < min)
                {
                    v = hit.point;
                    min = distance;
                }
            }
        }

        transform.parent.LookAt(v, transform.up);
    }
}
