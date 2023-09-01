using UnityEngine;
using DG.Tweening;

public class VisualizePosition : MonoBehaviour
{
    [SerializeField]
    private GameObject visualCube;

    void Update()
    {
        if (DraggingImage.isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 6) && hit.transform.CompareTag("PLANE"))
            {
                visualCube.SetActive(true);
                float PointX = System.MathF.Floor(hit.point.x) + 0.5f;
                float PointZ = System.MathF.Floor(hit.point.z) + 0.5f;

                visualCube.transform.DOLocalMove(new Vector3(PointX, -0.3f, PointZ), 0.1f).SetEase(Ease.OutSine);
            }
            else
            {
                visualCube.SetActive(false);
            }
        }
        else
        {
            visualCube.transform.position = new Vector3(5, -0.3f);
            visualCube.SetActive(false);
        }
    }
}
