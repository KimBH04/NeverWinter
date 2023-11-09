using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject AllTutorial;
    [SerializeField] private GameObject[] tutorial;
    [SerializeField] private GameObject SkipButton;
    
    
   
    void Start()
    {
        AllTutorial.SetActive(true);
    }

    public void Nexttutorial(int index)
    {
        tutorial[index].SetActive(false);
        if (index < 7)
        {
            if (index == 6)
            {
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
