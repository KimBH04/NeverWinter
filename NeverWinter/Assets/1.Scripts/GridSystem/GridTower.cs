using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridTower : MonoBehaviour
{
    public delegate bool GridTowerDelegate();
    public static GridTowerDelegate MovedEvent;

    static bool isClick;

    [field: SerializeField] public int ID { get; private set; } //타워 고유 아이디

    [SerializeField] private GameObject highRankTower;          //상위 타워
    public GameObject HighRankTower => highRankTower;
    public Tower2 tower;

    private Vector3 boxPosition;        //설치 가능 시각화 상자의 이동 전 위치

    private GridField targetField;      //새로 이동하려는 위치의 그리드
    public GridField field;             //현재 있는 위치의 그리드

    protected void Start()
    {
        boxPosition = transform.localPosition;
    }

    private void Update()
    {
        if (!isClick)
        {
            if (Input.GetMouseButtonDown(1))
            {
                //타워 정보
            }
        }
    }

    private void OnMouseDown()
    {
        isClick = true;
    }

    private void OnMouseDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 12))
        {
            targetField = hit.transform.GetComponent<GridField>();

            transform.DOMove(hit.transform.position, 0.1f).SetEase(Ease.OutSine);
            targetField.VisualizeMovable(this);
        }
        VisualTowerManager.VisualizingTower(ID, transform.position);
    }

    protected void OnMouseUp()
    {
        if (targetField != null && field != null)
        {
            bool t = targetField.MovingTower(this, transform.parent);

            //목표 그리드에 이동 및 병합 성공시 현재 위치의 그리드의 타워 정보를 지우고 
            //현재 인스턴스의 그리드 정보를 목표 그리드로 변경

            //타워를 옮겨서 비게 된 그리드를 랜덤 스폰 그리드로 추가하고 옮겨진 곳은 삭제
            if (t)
            {
                field.havingTower = null;
                field.havingTowerParent = null;
                GridTowerRandomSpawn.grids.Add(field);

                field = targetField;
                GridTowerRandomSpawn.grids.Remove(field);

                MovedEvent?.Invoke();
            }
            targetField = null;
        }
        else
        {
            Debug.LogWarning("targetField or field is null. Make sure they are properly assigned.");
        }

        transform.DOKill();
        transform.localPosition = boxPosition;

        VisualTowerManager.EndVisualizing(ID);

        isClick = false;
    }

    public void TowerADUP(float plus)
    {
        tower.AD += plus;
    }

    public void TowerADminus(float minus)
    {
        tower.AD -= minus;
    }
}
