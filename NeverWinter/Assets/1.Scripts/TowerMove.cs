using System.Collections.Generic;
using UnityEngine;

public class TowerMove : MonoBehaviour
{

    [field: SerializeField]
    public int ID { get; private set; }


    [SerializeField] private GameObject highRankTower;

    [Header("Visualizing")]
    [SerializeField] private GameObject visualBox;
    private MeshRenderer visualMesh;

    [SerializeField] private Material red;
    [SerializeField] private Material green;
    [SerializeField] private Material blue;

    private bool isClick;


    public Tower2 tower2;
    private Vector3 beginPos;
    private bool readiedMerging;
    private bool move = true;
    private int triggeredCount;

    private Collider target;

    private TowerExplanationPopup popup;

    private void Start()
    {
        popup = GameObject.Find("TowerPopup").transform.Find("Popup").GetComponent<TowerExplanationPopup>();
        visualMesh = visualBox.GetComponent<MeshRenderer>();
    }

    public void TowerADUP(float plus)
    {
        tower2.AD += plus;
    }

    public void TowerADminus(float minus)
    {
        tower2.AD -= minus;
    }

    private void OnMouseDown()
    {
        isClick = true;
        beginPos = transform.position;
        visualBox.SetActive(true);
       
    }

    private void OnMouseDrag()
    {
        transform.position = CameraCtrl.FloorPos + Camera.main.transform.position;
        

        if (target != null)
        {
            visualMesh.material = blue;
        }
    }

    private void OnMouseUp()
    {
        if (readiedMerging)
        {
            Instantiate(highRankTower, target.transform.position, Quaternion.identity);

            Destroy(transform.parent.gameObject);
            Destroy(target.transform.parent.gameObject);
        }
        else if (move)
        {
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }
        else
        {
            transform.position = beginPos;
        }

        isClick = false;
        

        readiedMerging = false;
        move = true;

        visualMesh.material = green;
        visualBox.SetActive(false);
       

        triggeredCount = 0;

        target = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isClick)
            return;

        triggeredCount++;

        if (readiedMerging = other.TryGetComponent(out TowerMove tower) && tower.ID == ID && highRankTower != null)
        {
            target = other;
        }
        else if (target == null)
        {
            visualMesh.material = red;
        }
        
        move = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isClick)
            return;

        triggeredCount--;

        if (target == other)
        {
            target = null;
            readiedMerging = false;
        }
        else if (triggeredCount < 1)
        {
            visualMesh.material = green;
            move = true;
        }
    }
}
