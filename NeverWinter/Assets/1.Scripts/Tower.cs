using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [field: SerializeField]
    public int ID { get; private set; }

    [SerializeField]
    private GameObject highRankTower;

    [Header("Visualizing")]
    [SerializeField]
    private GameObject visualTower;
    [SerializeField]
    private GameObject visualBox;
    private MeshRenderer visualMesh;

    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;
    [SerializeField]
    private Material blue;

    private Vector3 beforePos;
    private bool move = true;
    private bool isClick;

    private bool readiedMerging;
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
        visualTower.SetActive(true);
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
        move = true;
        readiedMerging = false;
        visualTower.SetActive(false);
        target = null;

        Debug.Log(target);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isClick)
            return;

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

        if (target == other)
        {
            target = null;
            readiedMerging = false;
        }

        visualMesh.material = green;

        move = true;
    }
}
