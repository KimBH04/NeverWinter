using System;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public float  wSpeed, hSpeed, zoomInOutSpeed;
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
            X = Mathf.Clamp(X - deltaMouse.x /50, -10, 10);
            Z = Mathf.Clamp(Z - deltaMouse.y /50, -10, -2);
           
          
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
        float x = Input.GetAxisRaw("Horizontal")/5;
        float z = Input.GetAxisRaw("Vertical")/5;

        float zoom = Input.GetAxis("Mouse ScrollWheel");

        X = Mathf.Clamp(X + x, -10, 10);
        Z = Mathf.Clamp(Z + z, -10, -2);

        Zoom = Mathf.Clamp(Zoom + zoom, -1, 0);

        transform.position = new Vector3(X, 10, Z) + (Zoom * zoomInOutSpeed * transform.forward);
    }
}
