using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [field: SerializeField]
    public int ID { get; private set; }

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

    private void Start()
    {
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
    }

    private bool readiedMerging;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        if (!isClick)
            return;

        if (readiedMerging = other.TryGetComponent(out Tower target) && target.ID == ID)
        {
            visualMesh.material = blue;
        }
        else
        {
            visualMesh.material = red;
        }
        
        move = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isClick)
            return;

        readiedMerging = false;
        visualMesh.material = green;

        move = true;
    }

    private void OnMouseUp()
    {
        if (move)
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
    }
}
