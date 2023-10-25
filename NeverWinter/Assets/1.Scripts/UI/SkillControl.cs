using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillControl : MonoBehaviour
{
    public GameObject pen;

    public GameObject hideSkillButton;
    public GameObject textPro;
    public TextMeshProUGUI hideSkillTimeText;
    public Image hideSkillmage;
    private bool isHideSkill= false;
    private float skiiTimes = 10;
    private float getSkillTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        hideSkillTimeText = textPro.GetComponent<TextMeshProUGUI>();
        hideSkillButton.SetActive(false);
        pen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HideSkillChk();
    }


    public void HideSkiiSetting(int skillNum)
    {
        hideSkillButton.SetActive(true);
        pen.SetActive(true);
        getSkillTime = skiiTimes;
        isHideSkill = true;
    }

    private void HideSkillChk()
    {
        if(isHideSkill)
        {
            StartCoroutine(SkillTimeChk());
        }
    }

    IEnumerator SkillTimeChk()
    {
        yield return null;

        if(getSkillTime>0)
        {
            getSkillTime -= Time.deltaTime;

            if(getSkillTime<0)
            {
                getSkillTime = 0;
                isHideSkill = false;
                hideSkillButton.SetActive(false);
                pen.SetActive(false);

            }
            hideSkillTimeText.text = getSkillTime.ToString("00");


            float time = getSkillTime / skiiTimes;
            hideSkillmage.fillAmount = time;
            

        }
    }
}
