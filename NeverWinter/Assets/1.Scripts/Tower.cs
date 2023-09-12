using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    float downPos;

    void OnMouseDrag()
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 direction = mousePos.direction;
        float multi = -downPos / direction.y;

        transform.position = Camera.main.transform.position + (direction * multi);
    }

    void OnMouseUp()
    {
        if (Physics.Raycast(transform.position, new Vector3(0, -1, 0), out RaycastHit hit, 5))
        {

        }
    }
}
