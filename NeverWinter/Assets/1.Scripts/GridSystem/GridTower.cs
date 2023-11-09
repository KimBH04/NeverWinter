using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class GridTower : MonoBehaviour
{
    [field: SerializeField] public int ID { get; private set; } //타워 고유 아이디

    [SerializeField] private GameObject highRankTower;          //상위 타워
    public GameObject HighRankTower => highRankTower;

    [Header("Visualizing")]
    [SerializeField] private GameObject visualBox;
    private MeshRenderer visualMesh;
    private Vector3 boxPosition;        //설치 가능 시각화 상자의 이동 전 위치

    private GridField targetField;      //새로 이동하려는 위치의 그리드
    public GridField field;             //현재 있는 위치의 그리드

    private void Start()
    {
        visualMesh = visualBox.GetComponent<MeshRenderer>();
        boxPosition = transform.localPosition;
    }

    private void OnMouseDown()
    {
        visualBox.SetActive(true);
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 12))
        {
            targetField = hit.transform.GetComponent<GridField>();

            transform.DOMove(hit.transform.position, 0.1f).SetEase(Ease.OutSine);
            targetField.VisualizeMovable(this, visualMesh);
        }
    }

    private void OnMouseUp()
    {
        bool t = targetField.MovingTower(this, transform.parent);

        //목표 그리드에 이동 및 병합 성공시 현재 위치의 그리드의 타워 정보를 지우고 
        //현재 인스턴스의 그리드 정보를 목표 그리드로 변경
        if (t)
        {
            field.havingTower = null;
            field.havingTowerParent = null;
            GridTowerRandomSpawn.grids.Add(field);

            field = targetField;
        }
        targetField = null;

        transform.DOKill();
        transform.localPosition = boxPosition;

        visualBox.SetActive(false);
    }
}
