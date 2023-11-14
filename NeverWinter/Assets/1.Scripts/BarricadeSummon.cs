using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeSummon : MonoBehaviour
{
    [SerializeField] private Transform barricadeVisual;

    [SerializeField]
    private GameObject barricadePrefab;



    private Vector3 spawnPos;

    public bool summon = false;

    private void Update()
    {
        if (summon)
        {
            barricadeVisual.position = CameraCtrl.FloorPos + Camera.main.transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                SummonBarricade();
            }
        }
    }

    private void SummonBarricade()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 10))
        {
            barricadeVisual.position = new Vector3(0f, -2f, 0);
            summon = false;
            SkillControl.instance.HideSkiiSetting(1);
            spawnPos = hit.point;
            int random = Random.Range(0, 2);
            float y = (random == 0) ? 45f : 135f;
            GameObject barricade = Instantiate(barricadePrefab, spawnPos, Quaternion.Euler(0, y, 0));
        }
    }

    public void click()
    {
        summon = true;
    }
}
