using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class GridTower : MonoBehaviour
{
    [field: SerializeField] public int ID { get; private set; }

    [SerializeField] private GameObject highRankTower;
    public GameObject HighRankTower => highRankTower;

    [Header("Visualizing")]
    [SerializeField] private GameObject visualBox;
    private MeshRenderer visualMesh;
    private Vector3 boxPosition;

    private GridField targetField;
    public GridField field;

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

            transform.DOMove(hit.transform.position, 0.2f).SetEase(Ease.OutSine);
            targetField.VisualizeMovable(this, visualMesh);
        }
    }

    private void OnMouseUp()
    {
        bool t = targetField.MovingTower(this, transform.parent);

        //��ǥ �׸��忡 �̵� �� ���� ������ ���� ��ġ�� �׸����� Ÿ�� ������ ����� 
        //���� �ν��Ͻ��� �׸��� ������ ��ǥ �׸���� ����
        if (t)
        {
            field.havingTower = null;
            field.havingTowerParent = null;
            field = targetField;
        }
        targetField = null;

        transform.DOKill();
        transform.localPosition = boxPosition;

        visualBox.SetActive(false);
    }
}
