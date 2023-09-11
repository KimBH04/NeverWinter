using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    float downPos;

    void Update()
    {
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 direction = mousePos.direction;
        float multi = -downPos / direction.y;

        transform.position = Camera.main.transform.position + (direction * multi);
    }
}
