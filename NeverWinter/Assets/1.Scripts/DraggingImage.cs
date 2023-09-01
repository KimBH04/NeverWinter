using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class DraggingImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isDragging;

    bool isDraggingMe;
    Vector3 defaultPos;

    Image image;

    void Start()
    {
        defaultPos = transform.position;

        image = GetComponent<Image>();
    }

    void Update()
    {
        if (!isDraggingMe)
        {
            return;
        }

        float x = Input.mousePosition.x;
        float y = Input.mousePosition.y;
        transform.position = new Vector3(x, y, 0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = new Color(0, 0, 0, 0.5f);

        isDraggingMe = true;
        isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOMove(defaultPos, 0.2f).SetEase(Ease.OutExpo);

        image.color = new Color(0, 0, 0, 1);

        isDraggingMe = false;
        isDragging = false;
    }
}
