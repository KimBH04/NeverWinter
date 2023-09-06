using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    public float wSpeed, hSpeed, zoomInOutSpeed;
    private float X, Z, Zoom;

    private float w = Screen.width / 25f;
    private float h = Screen.height / 10f;

    void Update()
    {
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

        /*float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;*/

        float zoom = Input.GetAxis("Mouse ScrollWheel");

        /*X = Mathf.Clamp(X + x, -10, 10);
        Z = Mathf.Clamp(Z + z, -10, -2);*/

        Zoom = Mathf.Clamp(Zoom + zoom, -1, 0);

        transform.position = new Vector3(X, 10, Z) + (Zoom * zoomInOutSpeed * transform.forward);
    }
}
