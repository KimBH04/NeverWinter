using System;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public float zoomInOutSpeed;
    private float X, Z, Zoom;
    
    private float w = Screen.width / 25f;
    private float h = Screen.height / 10f;

    private bool isPanning = false;
    private Vector3 lastMousePosition;

    void Update()
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
            //X = Mathf.Clamp(X - deltaMouse.x / 50, -10, 10);
            //Z = Mathf.Clamp(Z - deltaMouse.y / 50, -10, -2);
            //밑에서 최대최소 조정되니 굳이 여기서 할 필요 없습니다
            X -= deltaMouse.x * 0.05f;
            Z -= deltaMouse.y * 0.05f;
            lastMousePosition = Input.mousePosition;
        }
        
        /*
        if (Input.mousePosition.x > Screen.width - w)
        {
            X = Mathf.Clamp(X + wSpeed, -10, 10);
        }
        else if (Input.mousePosition.x < w)
        {
            X = Mathf.Clamp(X - wSpeed, -10, 10);
        }

        if (Input.mousePosition.y > Screen.height - h)
        {
            Z = Mathf.Clamp(Z + hSpeed, -10, -2);
        }
        else if (Input.mousePosition.y < h)
        {
            Z = Mathf.Clamp(Z - hSpeed, -10, -2);
        }
*/
        float x = Input.GetAxisRaw("Horizontal") * 0.5f;
        float z = Input.GetAxisRaw("Vertical") * 0.5f;

        float zoom = Input.GetAxis("Mouse ScrollWheel");

        Zoom = Mathf.Clamp(Zoom + zoom, -1, 0);

        X = Mathf.Clamp(X + x, -(10 * Zoom + 10), 10 * Zoom + 10);
        Z = Mathf.Clamp(Z + z, -(4 * Zoom + 10), 4 * Zoom - 2);

        transform.position = new Vector3(X, 10, Z) + (Zoom * zoomInOutSpeed * transform.forward);
    }
}
