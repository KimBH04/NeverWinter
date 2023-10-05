using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverWiter
{
    public class GameManager : MonoBehaviour
    {
        public Attack Bullet = null;
        public GameObject levelUpPanel = null;
        public UiUpgrade[] upgradeItems = new UiUpgrade[3];
        public int[] upgradeItemLevel = new int[(int)UpgradeItemType.max+1];
        public GameObject tower = null;
        

        void Awake()
        {

        }

        void Start()
        {
            Application.targetFrameRate = 60;
            Bullet.AD = 10f;
        }

        
       public void WAVEEvent()
        {
            Time.timeScale = 0.0f;
            if (levelUpPanel)
                levelUpPanel.SetActive(true);


            List<UpgradeItemType> UpType = new List<UpgradeItemType>();

            while (UpType.Count < 3)
            {
                UpgradeItemType addType = (UpgradeItemType)Random.Range(0, (int)UpgradeItemType.max);
                if (UpType.Contains(addType) == false)
                    UpType.Add(addType);
            }

            for (int i = 0; i < upgradeItems.Length; i++)
            {
                upgradeItems[i].thisButton.interactable = false;
                int index = (int)UpType[i];
                upgradeItems[i].SetUpgradeInfo(UpType[i], upgradeItemLevel[index] + 1);
            }

            StartCoroutine(ActiveButtons());
        }

        IEnumerator ActiveButtons()
        {
            yield return new WaitForSecondsRealtime(1.0f);
            for (int i = 0; i < upgradeItems.Length; i++)
            {
                upgradeItems[i].thisButton.interactable = true;
            }
        }

        

        public void SelectUpgrade(UpgradeItemType uType)
        {
            Time.timeScale = 1.0f;
            if (levelUpPanel)
                levelUpPanel.SetActive(false);

            upgradeItemLevel[(int)uType]++;

            int currentLevel = upgradeItemLevel[(int)uType];

            switch (uType)
            {
                case UpgradeItemType.Potion:
                    
                        if (Bullet)
                        {
                            Bullet.AD += 5.0f; 
                        }

                    
                    break;
                case UpgradeItemType.Axe:
                    
                        Debug.Log("코인이 증가합니다");
                    
                    break;

                case UpgradeItemType.Book:
                    
                        Debug.Log("스펠 쿨타임 감소");
                    
                    break;
                case UpgradeItemType.Xbow:
                    
                        Debug.Log("넥서스 Hp 증가");
                    
                    break;
                case UpgradeItemType.Pub:

                    //Canon.shootDelay -= 0.2f;
                    
                    break;
                case UpgradeItemType.scout:
                    
                        Debug.Log("정찰대 ");                      
                    
                    break;

                case UpgradeItemType.Knight:
                    
                        Debug.Log("기마병");
                    
                    break;

                case UpgradeItemType.Gold:
                    
                        Debug.Log("코인이 증가합니다");
                    
                    break;

                case UpgradeItemType.Clover:
                    
                        Debug.Log("클로버");
                    Vector3 a = new Vector3(0.05f, 0.2f, -1.75f);
                    Instantiate(tower, a, Quaternion.identity);

                    break;

                case UpgradeItemType.Shield:
                    
                        Debug.Log("방패");
                    
                    break;

                case UpgradeItemType.Crown:
                    
                        Debug.Log("방패");
                    
                    break;
            } }

            
    }
}