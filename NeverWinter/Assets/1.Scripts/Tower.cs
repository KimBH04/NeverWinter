using UnityEngine;

public class Tower : MonoBehaviour
{
    [field: SerializeField]
    public int ID { get; private set; }

    [Header("Visualizing")]
    [SerializeField]
    private GameObject towerVisualize;
    private MeshRenderer visualMesh;

    [SerializeField]
    private Material red;
    [SerializeField]
    private Material green;
    [SerializeField]
    private Material blue;

    private Vector3 beforePos;
    private bool successMove = true;
    private bool isClick;

    private void Start()
    {
        visualMesh = towerVisualize.GetComponent<MeshRenderer>();
    }

    private void OnMouseDown()
    {
        isClick = true;
        beforePos = transform.position;
        towerVisualize.SetActive(true);
    }

    private void OnMouseDrag()
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 direction = mousePos.direction;
        float multi = -CameraCtrl.floorPos / direction.y;

        transform.position = Camera.main.transform.position + (direction * multi);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isClick)
            return;

        if (successMove = other.TryGetComponent(out Tower target) && target.ID == ID)
        {
            visualMesh.material = blue;
        }
        else
        {
            visualMesh.material = red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isClick)
            return;

        visualMesh.material = green;
    }

    private void OnMouseUp()
    {
        if (successMove)
        {

        }
        else
        {
            transform.position = beforePos;
        }

        isClick = false;
        successMove = true;
        towerVisualize.SetActive(false);
    }
}
