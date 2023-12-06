using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [SerializeField]
    private float floorIns;
    public float speed;
    public float zoomInOutSpeed;
    private float X, Z, Zoom;

    [SerializeField] private float cameraMaxView;
    
    private float w = Screen.width / 25f;
    private float h = Screen.height / 10f;

    private bool isPanning = false;
    private Vector3 lastMousePosition;

    private static float floorPos;

    public static Vector3 FloorPos
    {
        get
        {
            Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 dir = mousePos.direction;
            float multi = -floorPos / dir.y;
            return dir * multi;
        }
    }


    private void Start()
    {
        X = transform.position.x;
        Z = transform.position.z;
    }

    private void Update()
    {
        CamreaMove();
    }

    private void CamreaMove()
    {
        if (Input.GetMouseButtonDown(2))
        {
            isPanning = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(2))
        {
            isPanning = false;
        }

        if (isPanning)
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            X -= deltaMouse.x * speed * 0.01f;
            Z -= deltaMouse.y * speed * 0.01f;
            lastMousePosition = Input.mousePosition;
        }

        float x = Input.GetAxisRaw("Horizontal") * 0.5f;
        float z = Input.GetAxisRaw("Vertical") * 0.5f;

        float zoom = Input.GetAxis("Mouse ScrollWheel");

        Zoom = Mathf.Clamp(Zoom + zoom, -1, 0.5f);

        X = Mathf.Clamp(X + x, -(10 * Zoom + 10), 10 * Zoom + 10);
        Z = Mathf.Clamp(Z + z, -(4 * Zoom + 20), 4 * Zoom + 10);

        transform.position = new Vector3(X, 10, Z) + (Zoom * zoomInOutSpeed * transform.forward);
        floorPos = transform.position.y - floorIns;
    }
}
