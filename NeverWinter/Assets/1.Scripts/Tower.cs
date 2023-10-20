using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [field: SerializeField]
    public int ID { get; private set; }

    [SerializeField] private GameObject highRankTower;

    [Header("Visualizing")]
    [SerializeField] private GameObject visualBox;
    private MeshRenderer visualMesh;

    [SerializeField] private Material red;
    [SerializeField] private Material green;
    [SerializeField] private Material blue;

    private bool isClick;

    private Vector3 beforePos;
    private bool readiedMerging;
    private bool move = true;
    private int triggeredCount;

    private Collider target;

    private TowerExplanationPopup popup;

    private void Start()
    {
        popup = GameObject.Find("TowerPopup").transform.Find("Popup").GetComponent<TowerExplanationPopup>();
        visualMesh = visualBox.GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        isClick = true;
        beforePos = transform.position;
        visualBox.SetActive(true);
    }

    private void OnMouseDrag()
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 direction = mousePos.direction;
        float multi = -CameraCtrl.floorPos / direction.y;

        transform.position = direction * multi + Camera.main.transform.position;

        if (target != null)
        {
            visualMesh.material = blue;
        }
    }

    private void OnMouseUp()
    {
        if (readiedMerging)
        {
            Instantiate(highRankTower, target.transform.position, Quaternion.identity);

            Destroy(transform.parent.gameObject);
            Destroy(target.transform.parent.gameObject);
        }
        else if (move)
        {
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.position = beforePos;
        }

        isClick = false;

        readiedMerging = false;
        move = true;

        visualMesh.material = green;
        visualBox.SetActive(false);

        triggeredCount = 0;

        target = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isClick)
            return;

        triggeredCount++;

        if (readiedMerging = other.TryGetComponent(out Tower tower) && tower.ID == ID && highRankTower != null)
        {
            target = other;
        }
        else if (target == null)
        {
            visualMesh.material = red;
        }
        
        move = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isClick)
            return;

        triggeredCount--;

        if (target == other)
        {
            target = null;
            readiedMerging = false;
        }
        else if (triggeredCount < 1)
        {
            visualMesh.material = green;
            move = true;
        }
    }
}
