using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject AllTutorial;
    [SerializeField] private GameObject[] tutorial;
    [SerializeField] private GameObject SkipButton;
    

    public void Nexttutorial(int index)
    {
        tutorial[index].SetActive(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Ck);

        if (index < 7)
        {
            if (index == 6)
            {
                SkipButton.SetActive(false);
                return;
            }
            tutorial[index + 1].SetActive(true);
        }
        
    }
    
    public void AllTutorialClose()
    {
        AllTutorial.SetActive(false);
        SkipButton.SetActive(false);
    }
    

 
}
