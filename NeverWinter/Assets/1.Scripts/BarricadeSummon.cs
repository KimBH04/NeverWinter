using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarricadeSummon : MonoBehaviour
{
    [SerializeField] private Transform barricadeVisual;

    [SerializeField]
    private GameObject barricadePrefab;

    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject textObj;

    private bool summon;
    
    // 바리게이트 갯수 제한 변수
    public static int BarricadeCnt=0;


    private void Start()
    {
        textObj.SetActive(false);
        BarricadeCnt = 0;
    }

    private void Update()
    {
        if (summon)
        {
           
            if (Cost.Coin >= 40)
            {
                barricadeVisual.position = CameraCtrl.FloorPos + Camera.main.transform.position;
                if (Input.GetMouseButtonDown(1))
                {
                    summon=false;
                    barricadeVisual.position = new Vector3(0, -2, 0);
                }
                if (Input.GetMouseButtonDown(0))
                {
                    SummonBarricade();

                    
                }
            }
        }
    }

    private Vector3 spawnPos;

    private void SummonBarricade()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, 1 << 10))
        {
            barricadeVisual.position = new Vector3(0, -2, 0);
            summon = false;
            SkillControl.instance.HideSkiiSetting(1);
            spawnPos = hit.point;
            int random = Random.Range(0, 2);
            float y = (random == 0) ? 45f : 135f;
            GameObject barricade = Instantiate(barricadePrefab, spawnPos, Quaternion.Euler(0, y, 0));
            Cost.Coin -= 40;
            BarricadeCnt++;
            print(BarricadeCnt);
        }

    }

    public void click()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);
        if (Cost.Coin >= 40 && BarricadeCnt <= 4)
        {
            summon = true;
        }
        
        else if (Cost.Coin < 40)
        { 
            textObj.SetActive(true);
            text.text = "코인이 부족합니다.";
          print("코인이 부족합니다.");
          Invoke("Hide", 1.0f);
        }
        
        else if(BarricadeCnt > 4)
        {
            textObj.SetActive(true);
            text.text = "바리케이트는 5개까지만 설치할 수 있습니다.";
            print("바리케이트는 5개까지만 설치할 수 있습니다.");
            Invoke(nameof(Hide), 1.0f);
        }
    }

    private void Hide()
    {
        textObj.SetActive(false);
    }
    

}
