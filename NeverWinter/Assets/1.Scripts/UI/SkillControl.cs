using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillControl : MonoBehaviour
{
    
   
     public GameObject[] pen;

    public GameObject[] hideSkillButtons;
    public GameObject[] textPros;
    public TextMeshProUGUI[] hideSkillTimeTexts;
    public Image[] hideSkillmages;
    private bool[] isHideSkills= {false,false,false,false,false,false,false,false,false,false};
    private float[] skiiTimes = {10,20};
    private float[] getSkillTimes = {10,20,0,0,0,0,0,0,0,0};
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < textPros.Length; i++)
        {
          

            hideSkillTimeTexts[i] = textPros[i].GetComponent<TextMeshProUGUI>();
            hideSkillButtons[i].SetActive(false);
             pen[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
            HideSkillChk();
    }


    public void HideSkiiSetting(int skillNum)
    {
        hideSkillButtons[skillNum].SetActive(true);
        pen[skillNum].SetActive(true);
        getSkillTimes[skillNum] = skiiTimes[skillNum];
        isHideSkills[skillNum] = true;
    }

    private void HideSkillChk()
    {
        if(isHideSkills[0])
        {
            
            StartCoroutine(SkillTimeChk(0));
        }
        if(isHideSkills[1])
        {
            
            StartCoroutine(SkillTimeChk(1));
        }
        if(isHideSkills[2])
        {
            
            StartCoroutine(SkillTimeChk(2));
        }
        if(isHideSkills[3])
        {
            
            StartCoroutine(SkillTimeChk(3));
        }
        if(isHideSkills[4])
        {
            
            StartCoroutine(SkillTimeChk(4));
        }
    }

    IEnumerator SkillTimeChk(int skillNum)
    {
        yield return null;

        if(getSkillTimes[skillNum]>0)
        {
            getSkillTimes[skillNum] -= Time.deltaTime;

            if(getSkillTimes[skillNum]<0)
            {
                getSkillTimes[skillNum] = 0;
                isHideSkills[skillNum] = false;
                hideSkillButtons[skillNum].SetActive(false);
                pen[skillNum].SetActive(false);

            }
            hideSkillTimeTexts[skillNum].text = getSkillTimes[skillNum].ToString("00");


            float time = getSkillTimes[skillNum] / skiiTimes[skillNum];
            hideSkillmages[skillNum].fillAmount = time;
            

        }
    }
}
